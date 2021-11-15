using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
   class KeywordAtom : Atom
    {
        public override SyntaxKind Kind { get; }
        public override string CodeId { get; }
        public override string TextRepresentation { get; }

        public KeywordAtom(SyntaxKind kind, string codeId, string textRepresentation)
        {
            Kind = kind;
            CodeId = codeId;
            TextRepresentation = textRepresentation;
        }
    }
}


