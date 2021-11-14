using orm_plus_compiler.StaticChecker.Binding.Abstraction;
using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Binding.Expression
{
    internal sealed class BoundLiteralExpression : BoundExpression
    {

        public override Type Type => Value.GetType();
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
        public object Value { get; }
 
        public BoundLiteralExpression(object value)
        {
            Value = value;
        }
    }
}
