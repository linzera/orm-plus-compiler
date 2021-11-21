using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    public class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
        public TextSpan Span => new TextSpan(Position, Text.Length);

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
    }
}
