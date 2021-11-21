using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
   class OperatorAtom : Atom
    {
        public override SyntaxKind Kind { get; }
        public override string CodeId { get; }
        public override string TextRepresentation { get; }
        public OperatorKind OperatorType { get; }

        public OperatorAtom(SyntaxKind kind, string codeId, string textRepresentation)
        {
            Kind = kind;
            CodeId = codeId;
            TextRepresentation = textRepresentation;
        }

        public string Operator { get => TextRepresentation[0]; }
    }
}


