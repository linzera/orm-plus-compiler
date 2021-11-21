using System;
using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Binding.Abstraction;
using orm_plus_compiler.StaticChecker.Syntax.Utils;

namespace orm_plus_compiler.StaticChecker.Binding.Expression
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
        public override Type Type => Expression.Type;
        public VariableSymbol Variable { get; }
        public BoundExpression Expression { get; }
    }
}
