using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Enum
{
    internal enum BoundNodeKind
    {
        UnaryExpression,

        LiteralExpression,

        BinaryExpression,
        VariableExpression,
        AssignmentExpression
    }
}
