// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2011 Irony - Roman Ivantsov
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Irony.Parsing {

  //Note: this class was not tested at all
  // Based on contributions by CodePlex user sakana280
  // 12.09.2008 - breaking change! added "name" parameter to the constructor
  public class RegexBasedTerminal : Terminal {
    public RegexBasedTerminal(string pattern, params string[] prefixes)
      : base("name") {
      Pattern = pattern;
      if (prefixes != null)
        Prefixes.AddRange(prefixes);
    }
    public RegexBasedTerminal(string name, string pattern, params string[] prefixes) : base(name) {
      Pattern = pattern;
      if (prefixes != null)
        Prefixes.AddRange(prefixes);
    }

    #region public properties
    public readonly string Pattern;
    public readonly StringList Prefixes = new StringList();

    public Regex Expression {
      get { return _expression; }
    } Regex  _expression;
    #endregion

    public override void Init(GrammarData grammarData) {
      base.Init(grammarData);
      string workPattern = @"\G(" + Pattern + ")";
      RegexOptions options = (Grammar.CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
      _expression = new Regex(workPattern, options);
      if (this.EditorInfo == null) 
        this.EditorInfo = new TokenEditorInfo(TokenType.Unknown, TokenColor.Text, TokenTriggers.None);
    }

    public override IList<string> GetFirsts() {
      return Prefixes;
    }

    public override Token TryMatch(ParsingContext context, ISourceStream source) {
      Match m = _expression.Match(source.Text, source.PreviewPosition);
      if (!m.Success || m.Index != source.PreviewPosition) 
        return null;
      source.PreviewPosition += m.Length;
      return source.CreateToken(this.OutputTerminal); 
    }

  }//class




}//namespace
