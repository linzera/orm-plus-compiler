using System;
using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Binding.Abstraction;
using orm_plus_compiler.StaticChecker.Syntax.Utils;

namespace orm_plus_compiler.StaticChecker.Binding.Expression
{
    internal sealed class BoundVariableExpression : BoundExpression
    {
        public BoundVariableExpression(VariableSymbol variable)
        {
            Variable = variable;
        }

        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
        public override Type Type => Variable.Type;
        public VariableSymbol Variable { get; }
    }
}
