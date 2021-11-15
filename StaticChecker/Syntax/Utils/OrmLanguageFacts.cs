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
        };

        public static Dictionary<String, DoubleOperatorAtom> doubleOperatorMapping = new Dictionary<String, DoubleOperatorAtom>{
            {"==", new DoubleOperatorAtom(SyntaxKind.EqualsEqualsToken, "S14", "==")},
            {"!=", new DoubleOperatorAtom(SyntaxKind.BangEquals, "S15", "!=")}
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

            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "falso":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}
