namespace OrmPlusCompiler.StaticChecker;

class Atom
{
    public Atom(SyntaxKind kind, string code, string textRepresentation)
    {
        Kind = kind;
        Code = code;
        TextRepresentation = textRepresentation;
    }

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
}