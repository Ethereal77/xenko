// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Irony.Parsing;

namespace Stride.Core.Shaders.Grammar
{
    /// <summary>
    /// Extensions to BnfTerm.
    /// </summary>
    public static class BnfTermExtensions
    {
        /// <summary>
        /// Makes a non terminal optional.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>An optional non terminal.</returns>
        public static NonTerminal Opt(this BnfTerm term)
        {
            var nonTerminal = term.Q();
            nonTerminal.SetFlag(TermFlags.NoAstNode);
            return nonTerminal;
        }

        /// <summary>
        /// Makes a list of non terminals.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>A list of non temrinal</returns>
        public static NonTerminal List(this BnfTerm term)
        {
            var nonTerminal = term.Plus();
            nonTerminal.SetFlag(TermFlags.NoAstNode);
            return nonTerminal;
        }

        /// <summary>
        /// Makes an optional list of non terminals.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>An optional list of non terminals.</returns>
        public static NonTerminal ListOpt(this BnfTerm term)
        {
            var nonTerminal = term.Star();
            nonTerminal.SetFlag(TermFlags.NoAstNode);
            return nonTerminal;
        }
    }
}
