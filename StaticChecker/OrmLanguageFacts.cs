namespace OrmPlusCompiler.StaticChecker;

class OrmLanguageFacts
{

    public static Dictionary<Char, Atom> singleOperatorMapping = new Dictionary<Char, Atom>{
        {'(', new Atom(SyntaxKind.OpenParenthesisToken, "S06", "(")},
        {')', new Atom(SyntaxKind.CloseParenthesisToken, "S07", ")")},
        {'+', new Atom(SyntaxKind.PlusToken, "S16", "+")},
        {'-', new Atom(SyntaxKind.MinusToken, "S17", "-")},
        {'*', new Atom(SyntaxKind.StarToken, "S18", "*")},
        {'/', new Atom(SyntaxKind.SlashToken, "S19", "/")},
    };

    // Agora conseguimos adicionar prioridades ao tokens com o uso de precedentes.
    public static int GetBinaryOperatorPrecedence(SyntaxKind kind)
    {
        switch (kind)
        {
            case SyntaxKind.SlashToken:
            case SyntaxKind.StarToken:
                return 2;
            case SyntaxKind.PlusToken:
            case SyntaxKind.MinusToken:
                return 1;
            default:
                return 0;
        }
    }
}