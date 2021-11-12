namespace OrmPlusCompiler.StaticChecker;

class OrmTokens
{

    public static Dictionary<Char, Atom> singleOperatorMapping = new Dictionary<Char, Atom>{
        {'(', new Atom(SyntaxKind.OpenParenthesisToken, "S06", "(")},
        {')', new Atom(SyntaxKind.CloseParenthesisToken, "S07", ")")},
        {'+', new Atom(SyntaxKind.PlusToken, "S16", "+")},
        {'-', new Atom(SyntaxKind.MinusToken, "S17", "-")},
        {'*', new Atom(SyntaxKind.StarToken, "S18", "*")},
        {'/', new Atom(SyntaxKind.SlashToken, "S19", "/")},
    };

}