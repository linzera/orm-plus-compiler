namespace OrmPlusCompiler.StaticChecker.Syntax
{
    enum SyntaxKind
    {
        // Tokens
        NumberToken,
        SemiColonToken,
        OpenBracketToken,
        CloseBracketToken,
        ColonToken,
        CommaToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        QuestionMarkToken,
        OpenBraceToken,
        CloseBraceToken,
        LessOrEqualToken,
        LessToken,
        MoreToken,
        MoreOrEqualToken,
        EqualityToken,
        DifferentToken,
        PoundToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        PercentToken,
        WhiteSpaceToken,
        BadExpressionToken,
        EndOfFileToken,
        IdentifierToken,

        // Expressoes
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression,
        LiteralExpression,

        // Palavras reservadas
        TrueKeyword,
        FalseKeyword,
    }
}

