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
            {'{', new OperatorAtom(SyntaxKind.RightBrace, "S09", "{")},
            {'}', new OperatorAtom(SyntaxKind.LeftBrace, "S10", "}")},
        };

        public static Dictionary<String, DoubleOperatorAtom> doubleOperatorMapping = new Dictionary<String, DoubleOperatorAtom>{
            {"==", new DoubleOperatorAtom(SyntaxKind.EqualsEqualsToken, "S14", "==")},
            {"!=", new DoubleOperatorAtom(SyntaxKind.BangEquals, "S15", "!=")},
            {"<=", new DoubleOperatorAtom(SyntaxKind.SmallerOrEqualsThan, "S10", "<=")},
            {">=", new DoubleOperatorAtom(SyntaxKind.BiggerOrEqualsThan, "S13", ">=")},
            {":=", new DoubleOperatorAtom(SyntaxKind.TwoPointsEquals, "S21", ":=")},
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

        public static string GetAtomCodeId(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.IntegerToken:
                    return "I04";
                case SyntaxKind.DoubleToken:
                    return "I05";
                case SyntaxKind.NotReservedKeyword:
                    return "I02";
                default:
                    return "X00";
            }
        }

        public static string EvaluatePreviousTokenCodeId(SyntaxToken previousToken)
        {
            switch (previousToken.Kind)
            {
                case SyntaxKind.InitialProgramKeyword:
                    return "I01";
                default:
                    return null;
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
                case "vazio":
                    return SyntaxKind.VoidTypeKeyword;
                case "real":
                    return SyntaxKind.FloatTypeKeyword;
                case "inteiro":
                    return SyntaxKind.IntegerTypeKeyword;
                case "tipo-func":
                    return SyntaxKind.FunctionTypeKeyword;
                case "tipo-param":
                    return SyntaxKind.ParameterTypeKeyword;
                case "retorna":
                    return SyntaxKind.ReturnKeyword;
                default:
                    return SyntaxKind.NotReservedKeyword;
            }
        }
    }
}
