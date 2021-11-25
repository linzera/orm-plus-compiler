using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public string SyntaxAtomCodeId { get; set; }
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
        public SyntaxToken(SyntaxKind kind, int position, string text, object value, string atomCodeId)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
            SyntaxAtomCodeId = atomCodeId;
        }
    }
}
