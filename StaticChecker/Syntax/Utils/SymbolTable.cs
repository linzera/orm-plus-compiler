using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    internal static class SymbolTable
    {
        public static List<SymbolTableRow> RowFormation(List<CodeLine> codeLines)
        {
            List<SymbolTableRow> symbolDatasList = new List<SymbolTableRow>();


            foreach (CodeLine line in codeLines)
            {
                SymbolTableRow symbloData = new SymbolTableRow();
                symbloData.Index = symbolDatasList.Count() + 1;
                Lexer lexer = new Lexer(line.Line);

                SyntaxToken token;

                do
                {
                    token = lexer.Lex();

                    if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadExpressionToken && (token.Kind == SyntaxKind.IntegerToken
                        || token.Kind == SyntaxKind.DoubleToken || token.Kind == SyntaxKind.NotReservedKeyword))
                    {
                        if (!symbolDatasList.Any(s => s.SyntaxToken.Text == token.Text))
                        {
                            symbloData.IndexLineList.Add(line.LineId);
                            symbloData.SyntaxToken = token;
                            symbolDatasList.Add(symbloData);
                        }
                        else
                        {
                            SymbolTableRow symbloRow = symbolDatasList.FirstOrDefault(s => s.SyntaxToken.Text == token.Text);
                            int lineIndex = symbolDatasList.IndexOf(symbloRow);
                            symbolDatasList[lineIndex].IndexLineList.Add(line.LineId);
                        }
                    }

                } while (token.Kind != SyntaxKind.EndOfFileToken);

            }

            return symbolDatasList;
        }
    }
}
