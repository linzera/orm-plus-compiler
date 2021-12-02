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
        IntegerToken,
        DoubleToken,
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
        SmallerOrEqualsThan,
        BiggerOrEqualsThan,
        SmallerThan,
        BiggerThan,
        TwoPoints,
        Semicolon,
        Comma,
        OpenSquareBracket,
        CloseSquareBracket,
        LeftBrace,
        RightBrace,
        QuestionMark,
        ConstChar,
        ConstString,

        // Palavras reservadas
        TrueKeyword,
        FalseKeyword,
        InitialProgramKeyword,
        FinalProgramKeyword,
        InitialStatementsKeyword,
        FinalStatementsKeyword,
        InitialFunctionKeyword,
        FinalFunctionKeyword,
        VarTypeKeyword,
        NotReservedKeyword,
        VoidTypeKeyword,
        FloatTypeKeyword,
        IntegerTypeKeyword,
        FunctionTypeKeyword,
        ParameterTypeKeyword,
        ReturnKeyword,
        TwoPointsEquals,
        StringKeyword,
        LogicKeyword,
        CharKeyword,
        IfKeyword,
        EndIfKeyword,
        ElseIfKeyword,
        WhileKeyword,
        EndWhileKeyword,
        PauseKeyword,
        PrintKeyword,
        FinalFuncKyword

    }
}
