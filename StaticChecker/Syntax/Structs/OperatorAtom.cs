using orm_plus_compiler.StaticChecker.Enum;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    class OperatorAtom : Atom
    {
        public override SyntaxKind Kind { get; }
        public override string CodeId { get; }
        public override string TextRepresentation { get; }

        public OperatorAtom(SyntaxKind kind, string codeId, string textRepresentation)
        {
            Kind = kind;
            CodeId = codeId;
            TextRepresentation = textRepresentation;
        }
        public char Operator { get => TextRepresentation[0]; }
    }
}


