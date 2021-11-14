using orm_plus_compiler.StaticChecker.Binding.Abstraction;
using orm_plus_compiler.StaticChecker.Binding.Operator_Class;
using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Binding.Expression
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public override Type Type => Op.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;

        public BoundExpression Left { get; }
        public BoundBinaryOperator Op { get; }
        public BoundExpression Right { get; }
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

    }
}
