using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    abstract class Atom
    {
        public abstract SyntaxKind Kind { get; }
        public abstract string CodeId { get; }
        public abstract string TextRepresentation { get; }
    }
}
