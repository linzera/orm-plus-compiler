using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    class LexRow
    {
        public LexRow(string text, string atomCodeId, int? symbolTableIndex)
        {
            Text = text;
            AtomCodeId = atomCodeId;
            SymbolTableIndex = symbolTableIndex;
        }

        public string Text { get; }
        public string AtomCodeId { get; }
        public int? SymbolTableIndex { get; }
    }
    class LineToken
    {
        public LineToken(CodeLine lineData)
        {
            LineData = lineData;
            Tokens = new List<SyntaxToken>();
        }

        public CodeLine LineData { get; }
        public List<SyntaxToken> Tokens { get; }
    }

    internal static class SymbolTable
    {

        public static List<SymbolTableRow> symbolDataList = new List<SymbolTableRow>();
        public static List<LexRow> lexDataList = new List<LexRow>();

        public static List<SymbolTableRow> RowFormation(List<CodeLine> codeLines)
        {
            var lineTokens = new List<LineToken>();

            foreach (CodeLine line in codeLines)
            {
                var newLineToken = new LineToken(line);
                var lexer = new Lexer(line.Line);

                SyntaxToken token;

                do
                {
                    token = lexer.Lex();

                    if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadExpressionToken)
                    {
                        newLineToken.Tokens.Add(token);
                    }

                } while (token.Kind != SyntaxKind.EndOfFileToken);

                lineTokens.Add(newLineToken);
            }


            lineTokens.ForEach((lineToken) =>
            {
                // Montar table de simbolos e lex
                lineToken.Tokens.ForEach((token) =>
                {
                    if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadExpressionToken && (token.Kind == SyntaxKind.IntegerToken
                                   || token.Kind == SyntaxKind.DoubleToken || token.Kind == SyntaxKind.NotReservedKeyword || token.Kind == SyntaxKind.ConstString ||
                                   token.Kind == SyntaxKind.ConstChar))
                    {
                        if (!symbolDataList.Any(s => s.SyntaxToken.Text == token.Text))
                        {
                            SymbolTableRow symbolData = new SymbolTableRow();
                            symbolData.Index = symbolDataList.Count() + 1;
                            symbolData.IndexLineList.Add(lineToken.LineData.LineId);

                            SyntaxToken currentSyntaxToken = token;

                            int tokenIndex = lineToken.Tokens.IndexOf(token);

                            // Se token nao tem antecedente adicionamos direto a tabela de simbolos
                            if (tokenIndex == 0)
                            {
                                symbolData.SyntaxToken = currentSyntaxToken;
                                symbolDataList.Add(symbolData);
                                return;
                            }

                            // precisamos descobrir se o token tem um antecedente que diz sua caracteristica
                            // ex: nome-programa

                            SyntaxToken previousToken = lineToken.Tokens[tokenIndex - 1];
                            string previousContextAtomId = OrmLanguageFacts.EvaluatePreviousTokenCodeId(previousToken, lineToken.Tokens, tokenIndex);

                            if (previousContextAtomId != null)
                            {
                                currentSyntaxToken.SyntaxAtomCodeId = previousContextAtomId;
                            }

                            symbolData.SyntaxToken = currentSyntaxToken;
                            symbolDataList.Add(symbolData);
                        }
                        else
                        {
                            SymbolTableRow symbolRow = symbolDataList.FirstOrDefault(s => s.SyntaxToken.Text == token.Text);
                            int lineIndex = symbolDataList.IndexOf(symbolRow);
                            symbolDataList[lineIndex].IndexLineList.Add(lineToken.LineData.LineId);
                        }
                    }
                });

                lineToken.Tokens.ForEach((token) =>
                {
                    if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadExpressionToken && token.Kind != SyntaxKind.EndOfFileToken)
                    {

                        SymbolTableRow symbolRow = symbolDataList.Find(symbol => symbol.SyntaxToken.Text == token.Text);
                        string lex = string.Empty;
                        int? symbolIndex;
                        if (symbolRow != null)
                        {
                            if (symbolRow.SyntaxToken.GetType() == typeof(TruncatedSyntaxToken))
                            {
                                TruncatedSyntaxToken truncatedText = symbolRow.SyntaxToken as TruncatedSyntaxToken;
                                lex = truncatedText.TruncatedText;
                            }
                            else
                            {
                                lex = symbolRow.SyntaxToken.Text;
                            }
                            symbolIndex = symbolRow.Index;

                        }
                        else
                        {
                            lex = token.Text;
                            symbolIndex = null;
                        }

                        while (lex.Length <= 30)
                            lex = lex + " ";
                        lex = lex + " |";

                        LexRow lexRow = new LexRow(lex, token.SyntaxAtomCodeId, symbolIndex);

                        lexDataList.Add(lexRow);
                    }
                });



            });



            return symbolDataList;
        }
    }
}
