using orm_plus_compiler.StaticChecker.Binding.Abstraction;
using orm_plus_compiler.StaticChecker.Binding.Expression;
using orm_plus_compiler.StaticChecker.Binding.Operator_Class;
using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Structs;
using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System;

namespace orm_plus_compiler.StaticChecker.Binding
{
    internal sealed class Binder
    {
        private DiagnosticBag _diagnostics = new DiagnosticBag();
        public DiagnosticBag Diagnostics => _diagnostics;

        public BoundExpression BindExpression(ExpressionSyntax syntax)
        {
            switch (syntax.Kind)
            {
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax);
                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);
                case SyntaxKind.ParenthesizedExpression:
                    var parenthesizedExpression = (ParenthesizedExpressionSyntax)syntax;
                    return BindExpression(parenthesizedExpression.Expression);
                default:
                    throw new Exception($"Unexpected syntax {syntax.Kind}");
            }
        }

        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
        {
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);


            if (boundOperator == null)
            {
                _diagnostics.ReportUndefinedUnaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundOperand.Type);
                return boundOperand;
            }

            return new BoundUnaryExpression(boundOperator, boundOperand);
        }

        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var left = BindExpression(syntax.Left);
            var right = BindExpression(syntax.Right);
            var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, left.Type, right.Type);

            if (boundOperator == null)
            {
                _diagnostics.ReportUndefinedBinaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, left.Type, right.Type);
                return left;
            }

            return new BoundBinaryExpression(left, boundOperator, right);
        }

        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
        {
            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }
    }
}
