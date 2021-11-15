using orm_plus_compiler.StaticChecker.Syntax.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    internal class SymbolRow
    {
        public int RowId { get; }

        public Atom Atom { get; }

        public SyntaxToken Lexeme { get; }

        public int CharAfterTruncateCount { get; }

        public int CharBeforeTruncateCount { get; }
    }
}
