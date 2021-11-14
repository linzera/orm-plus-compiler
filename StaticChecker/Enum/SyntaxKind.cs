using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Enum
{
    internal enum SyntaxKind
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
