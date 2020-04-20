﻿// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2011 Irony - Roman Ivantsov
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Parsing {
  // Helper classes for information used by syntax highlighters and editors
  // TokenColor, TokenTriggers and TokenType are copied from the Visual Studio integration assemblies. 
  //  Each terminal/token would have its TokenEditorInfo that can be used either by VS integration package 
  //   or any editor for syntax highligting.

  public class TokenEditorInfo {
    public readonly TokenType Type;
    public readonly TokenColor Color;
    public readonly TokenTriggers Triggers;
    public string ToolTip;
    public int UnderlineType;
    public TokenEditorInfo(TokenType type, TokenColor color, TokenTriggers triggers) {
      Type = type;
      Color = color;
      Triggers = triggers;
    }

  }//class

  public enum TokenColor {
    Text = 0,
    Keyword = 1,
    Comment = 2,
    Identifier = 3,
    String = 4,
    Number = 5,
  }

  // (Comments are coming from Visual Studio integration package)
  //     Specifies a set of triggers that can be fired from an Microsoft.VisualStudio.Package.IScanner
  //     language parser.
  [Flags]
  public enum TokenTriggers {
    // Summary:
    //     Used when no triggers are set. This is the default.
    None = 0,
    //
    // Summary:
    //     A character that indicates that the start of a member selection has been
    //     parsed. In C#, this could be a period following a class name. In XML, this
    //     could be a < (the member select is a list of possible tags).
    MemberSelect = 1,
    //
    // Summary:
    //     The opening or closing part of a language pair has been parsed. For example,
    //     in C#, a { or } has been parsed. In XML, a < or > has been parsed.
    MatchBraces = 2,
    //
    // Summary:
    //     A character that marks the start of a parameter list has been parsed. For
    //     example, in C#, this could be an open parenthesis, "(".
    ParameterStart = 16,
    //
    // Summary:
    //     A character that separates parameters in a list has been parsed. For example,
    //     in C#, this could be a comma, ",".
    ParameterNext = 32,
    //
    // Summary:
    //     A character that marks the end of a parameter list has been parsed. For example,
    //     in C#, this could be a close parenthesis, ")".
    ParameterEnd = 64,
    //
    // Summary:
    //     A parameter in a method's parameter list has been parsed.
    Parameter = 128,
    //
    // Summary:
    //     This is a mask for the flags used to govern the IntelliSense Method Tip operation.
    //     This mask is used to isolate the values Microsoft.VisualStudio.Package.TokenTriggers.Parameter,
    //     Microsoft.VisualStudio.Package.TokenTriggers.ParameterStart, Microsoft.VisualStudio.Package.TokenTriggers.ParameterNext,
    //     and Microsoft.VisualStudio.Package.TokenTriggers.ParameterEnd.
    MethodTip = 240,
  }

  public enum TokenType {
    Unknown = 0,
    Text = 1,
    Keyword = 2,
    Identifier = 3,
    String = 4,
    Literal = 5,
    Operator = 6,
    Delimiter = 7,
    WhiteSpace = 8,
    LineComment = 9,
    Comment = 10,
  }

}
