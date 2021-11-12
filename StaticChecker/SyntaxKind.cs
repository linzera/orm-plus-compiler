namespace OrmPlusCompiler.StaticChecker
{
    enum SyntaxKind
    {
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

        // Expressions
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression,
    }
}

