using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System.Collections.Generic;
using System.Linq;
namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    sealed public class SyntaxTree
    {
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
        public IReadOnlyList<Diagnostic> Diagnostics { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
        public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, IEnumerable<Diagnostic> diagnostics)
        {
            Root = root;
            EndOfFileToken = endOfFileToken;
            Diagnostics = diagnostics.ToArray();
        }
    }
}
