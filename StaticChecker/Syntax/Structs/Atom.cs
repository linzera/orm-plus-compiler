using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    internal class Atom
    {
        public SyntaxKind Kind { get; }
        public string Code { get; }
        public string TextRepresentation { get; }

        public char Operator
        {
            get
            {
                return this.TextRepresentation[0];
            }
        }

        public Atom(SyntaxKind kind, string code, string textRepresentation)
        {
            Kind = kind;
            Code = code;
            TextRepresentation = textRepresentation;
        }
    }
}
