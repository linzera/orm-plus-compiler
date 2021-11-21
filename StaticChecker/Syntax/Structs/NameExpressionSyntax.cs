using orm_plus_compiler.StaticChecker.Enum;
using System.Collections.Generic;
namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    public sealed class NameExpressionSyntax : ExpressionSyntax
    {
        public NameExpressionSyntax(SyntaxToken identifierToken)
        {
            IdentifierToken = identifierToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NameExpression;
        public SyntaxToken IdentifierToken { get; }
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return IdentifierToken;
        }
    }
}
