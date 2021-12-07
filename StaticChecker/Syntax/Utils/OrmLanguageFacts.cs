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

        public static List<char> ValidEspecialCharList = new List<char>()
        {
           '$', '_', '.'
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
                case SyntaxKind.ConstChar:
                    return "I07";
                case SyntaxKind.ConstString:
                    return "I06";
                case SyntaxKind.InitialProgramKeyword:
                    return "P01";
                case SyntaxKind.InitialStatementsKeyword:
                    return "P02";
                case SyntaxKind.FinalStatementsKeyword:
                    return "P03";
                case SyntaxKind.InitialFunctionKeyword:
                    return "P04";
                case SyntaxKind.FinalFunctionKeyword:
                    return "P05";
                case SyntaxKind.FinalProgramKeyword:
                    return "P06";
                case SyntaxKind.VarTypeKeyword:
                    return "P07";
                case SyntaxKind.VoidTypeKeyword:
                    return "P08";
                case SyntaxKind.FloatTypeKeyword:
                    return "P09";
                case SyntaxKind.IntegerTypeKeyword:
                    return "P10";
                case SyntaxKind.StringKeyword:
                    return "P11";
                case SyntaxKind.LogicKeyword:
                    return "P12";
                case SyntaxKind.CharKeyword:
                    return "P13";
                case SyntaxKind.FunctionTypeKeyword:
                    return "P14";
                case SyntaxKind.FinalFuncKyword:
                    return "P15";
                case SyntaxKind.ParameterTypeKeyword:
                    return "P16";
                case SyntaxKind.IfKeyword:
                    return "P17";
                case SyntaxKind.EndIfKeyword:
                    return "P18";
                case SyntaxKind.ElseIfKeyword:
                    return "P19";
                case SyntaxKind.WhileKeyword:
                    return "P20";
                case SyntaxKind.EndWhileKeyword:
                    return "P21";
                case SyntaxKind.ReturnKeyword:
                    return "P22";
                case SyntaxKind.PauseKeyword:
                    return "P23";
                case SyntaxKind.PrintKeyword:
                    return "P24";
                case SyntaxKind.TrueKeyword:
                    return "P25";
                case SyntaxKind.FalseKeyword:
                    return "P26";
                default:
                    return "X00";
            }
        }

        public static string EvaluatePreviousTokenCodeId(SyntaxToken previousToken, List<SyntaxToken> tokens, int tokenIndex)
        {
            switch (previousToken.Kind)
            {
                case SyntaxKind.InitialProgramKeyword:
                    return "I01";
                case SyntaxKind.TwoPoints:
                    return EvaluateTwoPointsCodeId(previousToken, tokens, tokenIndex);
                default:
                    return null;
            }
        }

        public static string EvaluateTwoPointsCodeId(SyntaxToken previousToken, List<SyntaxToken> token, int tokenIndex)
        {

            SyntaxToken functionOrVarDelcaration = null;

            try
            {
                functionOrVarDelcaration = token[tokenIndex - 3];
            }
            catch
            {

            }

            if (functionOrVarDelcaration == null)
            {
                return null;
            }

            var typeDeclarationTokenKind = OrmLanguageFacts.GetKeywordKind(functionOrVarDelcaration.Text);

            switch (typeDeclarationTokenKind)
            {
                case SyntaxKind.FunctionTypeKeyword:
                    return "I03";
                case SyntaxKind.VarTypeKeyword:
                    return "I02";
                default:
                    return null;
            }
        }


        public static SyntaxKind GetKeywordKind(string text)
        {
            var normalizedText = text.ToLower();

            switch (normalizedText)
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
                case "fim-func":
                    return SyntaxKind.FinalFuncKyword;
                case "tipo-param":
                    return SyntaxKind.ParameterTypeKeyword;
                case "cadeia":
                    return SyntaxKind.StringKeyword;
                case "logico":
                    return SyntaxKind.LogicKeyword;
                case "retorna":
                    return SyntaxKind.ReturnKeyword;
                case "caracter":
                    return SyntaxKind.CharKeyword;
                case "se":
                    return SyntaxKind.IfKeyword;
                case "fim-se":
                    return SyntaxKind.EndIfKeyword;
                case "senao":
                    return SyntaxKind.ElseIfKeyword;
                case "enquanto":
                    return SyntaxKind.WhileKeyword;
                case "fim-enquanto":
                    return SyntaxKind.EndWhileKeyword;
                case "pausa":
                    return SyntaxKind.PauseKeyword;
                case "imprime":
                    return SyntaxKind.PrintKeyword;
                default:
                    return SyntaxKind.NotReservedKeyword;
            }
        }
    }
}
