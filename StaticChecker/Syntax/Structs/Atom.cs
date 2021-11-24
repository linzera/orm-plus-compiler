using orm_plus_compiler.StaticChecker.Enum;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    abstract class Atom
    {
        public abstract SyntaxKind Kind { get; }
        public abstract string CodeId { get; }
        public abstract string TextRepresentation { get; }
    }
}
