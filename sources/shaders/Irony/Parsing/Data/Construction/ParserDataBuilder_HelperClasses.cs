// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2011 Irony - Roman Ivantsov
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

//Helper data classes for ParserDataBuilder
// Note about using LRItemSet vs LRItemList. 
// It appears that in many places the LRItemList would be a better (and faster) choice than LRItemSet. 
// Many of the sets are actually lists and don't require hashset's functionality. 
// But surprisingly, using LRItemSet proved to have much better performance (twice faster for lookbacks/lookaheads computation), so LRItemSet
// is used everywhere.
namespace Stride.Irony.Parsing.Construction { 

  internal class ParserStateData {
    public readonly ParserState State;
    public readonly LRItemSet AllItems = new();
    public readonly LRItemSet ShiftItems = new();
    public readonly LRItemSet ReduceItems = new();
    public readonly LRItemSet InitialItems = new();
    public readonly BnfTermSet ShiftTerms = new();
    public readonly TerminalSet ShiftTerminals = new();
    public readonly TerminalSet Conflicts = new();
    public readonly TerminalSet ResolvedConflicts = new();
    public readonly bool IsInadequate;
    public LR0ItemSet AllCores = new();

    //used for creating canonical states from core set
    public ParserStateData(ParserState state, LR0ItemSet kernelCores) {
      State = state;
      foreach (var core in kernelCores)
        AddItem(core);
      IsInadequate =  ReduceItems.Count > 1 || ReduceItems.Count == 1 && ShiftItems.Count > 0;
    }

    public void AddItem(LR0Item core) {
      //Check if a core had been already added. If yes, simply return
      if (!AllCores.Add(core))return ; 
      //Create new item, add it to AllItems, InitialItems, ReduceItems or ShiftItems
      var item = new LRItem(State, core);
      AllItems.Add(item); 
      if (item.Core.IsFinal)
        ReduceItems.Add(item);
      else
        ShiftItems.Add(item);
      if (item.Core.IsInitial)
        InitialItems.Add(item); 
      if (core.IsFinal) return; 
      //Add current term to ShiftTerms
      if (!ShiftTerms.Add(core.Current)) return; 
      if (core.Current is Terminal)
        ShiftTerminals.Add(core.Current as Terminal); 
      //If current term (core.Current) is a new non-terminal, expand it
      var currNt = core.Current as NonTerminal;
      if (currNt == null) return; 
      foreach(var prod in currNt.Productions) 
        AddItem(prod.LR0Items[0]);
    }//method

    public TransitionTable Transitions {
      get {
        if (_transitions == null)
          _transitions = new TransitionTable();
        return _transitions;
      }
    } TransitionTable _transitions; 

    //A set of states reachable through shifts over nullable non-terminals. Computed on demand
    public ParserStateSet ReadStateSet {
      get {
        if (_readStateSet == null) {
          _readStateSet = new ParserStateSet(); 
          foreach(var shiftTerm in State.BuilderData.ShiftTerms)
            if (shiftTerm.FlagIsSet(TermFlags.IsNullable)) {
              var targetState = State.Actions[shiftTerm].NewState;
              _readStateSet.Add(targetState);
              _readStateSet.UnionWith(targetState.BuilderData.ReadStateSet); //we shouldn't get into loop here, the chain of reads is finite
            }
        }//if 
        return _readStateSet;
      }
    } ParserStateSet _readStateSet; 


    public TerminalSet GetShiftReduceConflicts() {
      var result = new TerminalSet();
      result.UnionWith(Conflicts);
      result.IntersectWith(ShiftTerminals);
      return result;
    }
    public TerminalSet GetReduceReduceConflicts() {
      var result = new TerminalSet();
      result.UnionWith(Conflicts);
      result.ExceptWith(ShiftTerminals);
      return result;
    }

  }//class

  //An object representing inter-state transitions. Defines Includes, IncludedBy that are used for efficient lookahead computation 
  internal class Transition {
    public readonly ParserState FromState;
    public readonly ParserState ToState;
    public readonly NonTerminal OverNonTerminal;
    public readonly LRItemSet Items;
    public readonly TransitionSet Includes = new();
    public readonly TransitionSet IncludedBy = new();
    int _hashCode;

    public Transition(ParserState fromState, NonTerminal overNonTerminal) {
      FromState = fromState;
      OverNonTerminal = overNonTerminal;
      ToState = FromState.Actions[overNonTerminal].NewState; 
      _hashCode = unchecked(FromState.GetHashCode() - overNonTerminal.GetHashCode());
      FromState.BuilderData.Transitions.Add(overNonTerminal, this);   
      Items = FromState.BuilderData.ShiftItems.SelectByCurrent(overNonTerminal);
      foreach(var item in Items) {
        item.Transition = this;
      }
      
    }//constructor

    public void Include(Transition other) {
      if (other == this)  return;
      if (!IncludeTransition(other)) return; 
      //include children
      foreach(var child in other.Includes) {
        IncludeTransition(child); 
      }
    }
    private bool IncludeTransition(Transition other) {
      if (!Includes.Add(other)) return false; 
      other.IncludedBy.Add(this);
      //propagate "up"
      foreach(var incBy in IncludedBy)
        incBy.IncludeTransition(other);
      return true; 
    }

    public override string ToString() {
      return FromState.Name + " -> (over " + OverNonTerminal.Name + ") -> " + ToState.Name;
    }
    public override int GetHashCode() {
      return _hashCode;
    }
  }//class

  internal class TransitionSet : HashSet<Transition> { }
  internal class TransitionList : List<Transition> { }
  internal class TransitionTable : Dictionary<NonTerminal, Transition> { }

  internal class LRItem {
    public readonly ParserState State;
    public readonly LR0Item Core;
    //these properties are used in lookahead computations
    public LRItem ShiftedItem;
    public Transition Transition; 
    int _hashCode;

    //Lookahead info for reduce items
    public TransitionSet Lookbacks = new(); 
    public TerminalSet Lookaheads = new();

    public LRItem(ParserState state, LR0Item core) {
      State = state;
      Core = core;
      _hashCode = unchecked(state.GetHashCode() + core.GetHashCode());
    }
    public override string ToString() {
      return Core.ToString();
    }
    public override int GetHashCode() {
      return _hashCode; 
    }
    
  }//LRItem class

  internal class LRItemList : List<LRItem> {}

  internal class LRItemSet : HashSet<LRItem> {

    public LRItem FindByCore(LR0Item core) {
      foreach (LRItem item in this)
        if (item.Core == core) return item;
      return null;
    }
    public LRItemSet SelectByCurrent(BnfTerm current) {
      var result = new LRItemSet();
      foreach (var item in this)
        if (item.Core.Current == current)
          result.Add(item);
      return result;
    }

    public LR0ItemSet GetShiftedCores() {
      var result = new LR0ItemSet();
      foreach (var item in this)
        if (item.Core.ShiftedItem != null) 
          result.Add(item.Core.ShiftedItem);
      return result;
    }
    public LRItemSet SelectByLookahead(Terminal lookahead) {
      var result = new LRItemSet();
      foreach (var item in this)
        if (item.Lookaheads.Contains(lookahead))
          result.Add(item);
      return result;
    }

  }//class

  public partial class LR0Item {
    public readonly Production Production;
    public readonly int Position;
    public readonly BnfTerm Current;
    public bool TailIsNullable;
    public GrammarHintList Hints = new GrammarHintList();

    //automatically generated IDs - used for building keys for lists of kernel LR0Items
    // which in turn are used to quickly lookup parser states in hash
    internal readonly int ID;

    public LR0Item(int id, Production production, int position, GrammarHintList hints) {
      ID = id;
      Production = production;
      Position = position;
      Current = (Position < Production.RValues.Count) ? Production.RValues[Position] : null;  
      if (hints != null)
        Hints.AddRange(hints); 
      _hashCode = ID.ToString().GetHashCode();
    }//method

    public LR0Item ShiftedItem {
      get {
        if (Position >= Production.LR0Items.Count - 1)
          return null;
        else
          return Production.LR0Items[Position + 1];
      }
    }
    public bool IsInitial {
      get { return Position == 0; }
    }
    public bool IsFinal {
      get { return Position == Production.RValues.Count; }
    }
    public override string ToString() {
      return Production.ProductionToString(Production, Position);
    }
    public override int GetHashCode() {
      return _hashCode;
    } int _hashCode;

  }//LR0Item

  internal class LR0ItemList : List<LR0Item> { }
  internal class LR0ItemSet : HashSet<LR0Item> { }



}//namespace
