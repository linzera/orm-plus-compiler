namespace OrmPlusCompiler.StaticChecker;

sealed class SyntaxTree
{
    public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, IEnumerable<string> diagnostics)
    {
        Root = root;
        EndOfFileToken = endOfFileToken;
        Diagnostics = diagnostics.ToList();
    }

    public ExpressionSyntax Root { get; }
    public SyntaxToken EndOfFileToken { get; }
    public IReadOnlyList<string> Diagnostics { get; }

    public static SyntaxTree Parse(string text)
    {
        var parser = new Parser(text);
        return parser.Parse();
    }
}