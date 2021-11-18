using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    class OrmLanguageFacts
    {
        public static Dictionary<Char, OperatorAtom> singleOperatorMapping = new Dictionary<Char, OperatorAtom>{
            {'(', new OperatorAtom(SyntaxKind.OpenParenthesisToken, "S06", "(")},
            {')', new OperatorAtom(SyntaxKind.CloseParenthesisToken, "S07", ")")},
            {'+', new OperatorAtom(SyntaxKind.PlusToken, "S16", "+")},
            {'-', new OperatorAtom(SyntaxKind.MinusToken, "S17", "-")},
            {'*', new OperatorAtom(SyntaxKind.StarToken, "S18", "*")},
            {'/', new OperatorAtom(SyntaxKind.SlashToken, "S19", "/")},
            {';', new OperatorAtom(SyntaxKind.Semicolon, "S01", ";")},
            {'[', new OperatorAtom(SyntaxKind.OpenSquareBracket, "S02", "[")},
            {']', new OperatorAtom(SyntaxKind.CloseSquareBracket, "S03", "]")},
            {':', new OperatorAtom(SyntaxKind.TwoPoints, "S04", ":")},
            {',', new OperatorAtom(SyntaxKind.Comma, "S05", ",")},
            {'?', new OperatorAtom(SyntaxKind.QuestionMark, "S08", "?")},
            {'{', new OperatorAtom(SyntaxKind.LeftBrace, "S09", "{")},
            {'}', new OperatorAtom(SyntaxKind.RightBrace, "S10", "}")},
        };

        public static Dictionary<String, DoubleOperatorAtom> doubleOperatorMapping = new Dictionary<String, DoubleOperatorAtom>{
            {"==", new DoubleOperatorAtom(SyntaxKind.EqualsEqualsToken, "S14", "==")},
            {"!=", new DoubleOperatorAtom(SyntaxKind.BangEquals, "S15", "!=")},
            {"<=", new DoubleOperatorAtom(SyntaxKind.SmallerOrEqualsThan, "S10", "<=")},
            {">=", new DoubleOperatorAtom(SyntaxKind.BiggerOrEqualsThan, "S13", ">=")},
        };

        public static String buildStringFromCurrentAndLookahead(char current, char lookahead)
        {
            return string.Concat(current, lookahead);
        }
        public static bool isCurrentAndLookaheadDoubleOperator(char current, char lookahead)
        {
            String operatorText = buildStringFromCurrentAndLookahead(current, lookahead);
            return doubleOperatorMapping.ContainsKey(operatorText);
        }


        public static int GetUnaryOperatorPrecedence(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                default:
                    return 0;
            }
        }

        public static int GetBinaryOperatorPrecedence(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 3;
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 2;

                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.BangEquals:
                    return 1;
                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            var normalizedText = text.ToLower();

            Console.WriteLine(normalizedText);

            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "falso":
                    return SyntaxKind.FalseKeyword;
                case "programa":
                    return SyntaxKind.InitialProgramKeyword;
                case "fim-programa":
                    return SyntaxKind.FinalProgramKeyword;
                case "declaracoes":
                    return SyntaxKind.InitialStatementsKeyword;
                case "fim-declaracoes":
                    return SyntaxKind.FinalStatementsKeyword;
                case "funcoes":
                    return SyntaxKind.InitialFunctionKeyword;
                case "fim-funcoes":
                    return SyntaxKind.FinalFunctionKeyword;
                case "tipo-var":
                    return SyntaxKind.VarTypeKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}
