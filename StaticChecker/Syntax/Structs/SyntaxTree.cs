using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    sealed class SyntaxTree
    {
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
        public IReadOnlyList<string> Diagnostics { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
        public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, IEnumerable<string> diagnostics)
        {
            Root = root;
            EndOfFileToken = endOfFileToken;
            Diagnostics = diagnostics.ToList();
        }
    }
}
