namespace OrmPlusCompiler.StaticChecker.Syntax
{
    enum SyntaxKind
    {
        // Tokens
        NumberToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        EqualsEqualsToken,
        BangEquals,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        WhiteSpaceToken,
        BadExpressionToken,
        EndOfFileToken,
        IdentifierToken,

        // Expressoes
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression,
        LiteralExpression,

        // Palavras reservadas
        TrueKeyword,
        FalseKeyword,
    }
}

