using orm_plus_compiler.StaticChecker.Syntax.Structs;
using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Files
{
    static class FileManager
    {
        public static Code FileReader()
        {
            while (true)
            {
                Console.WriteLine(" Enter the path to te .202 format file: ");
                Console.Write("> ");
                string path = Console.ReadLine();

                if (!Path.GetExtension(path).Equals(".202"))
                    Console.WriteLine(" \nERRO: Invalid file format, please select a .202 extation file\n");
                else
                {
                    try
                    {
                        StreamReader rd = new StreamReader(path);
                        List<CodeLine> codeLineList = new List<CodeLine>();
                        int index = 1;

                        while (!rd.EndOfStream)
                        {
                            string line = rd.ReadLine();

                            if (!string.IsNullOrWhiteSpace(line) || !string.IsNullOrEmpty(line))
                            {
                                string filteredLine = lineFilter(line.Split(' '));
                                CodeLine codeLine = new CodeLine(index, filteredLine);
                                codeLineList.Add(codeLine);
                            }
                            index++;
                        }

                        Code code = new Code(Path.GetFileNameWithoutExtension(path), Path.GetExtension(path), codeLineList);

                        return code;
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("\nERRO: The file or directory cannot be found.\n");
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.WriteLine("\nERRO: The file or directory cannot be found.\n");
                    }
                    catch (PathTooLongException)
                    {
                        Console.WriteLine("\nERRO: 'path' exceeds the maxium supported path length.\n");
                    }

                }
            }

        }

        private static List<string> FormateSymbolTableReport(List<SymbolTableRow> symbolTableRow)
        {

            List<string> tableRows = new List<string>();
            string index = string.Empty,
                code = string.Empty,
                lex = string.Empty,
                tokenType = string.Empty,
                lines = string.Empty,
                qtdA = string.Empty,
                qtdD = string.Empty;


            foreach (SymbolTableRow row in symbolTableRow)
            {

                if (row.Index < 10)
                    index = "   00" + row.Index + "   |";
                else if (row.Index < 100)
                    index = " 0" + row.Index + "   |";
                else
                    index = " " + row.Index + "   |";

                code = $"  {row.SyntaxToken.SyntaxAtomCodeId}  |";


                if (row.SyntaxToken.GetType() == typeof(TruncatedSyntaxToken))
                {
                    TruncatedSyntaxToken truncatedText = row.SyntaxToken as TruncatedSyntaxToken;
                    lex = truncatedText.TruncatedText + "  |";
                    qtdA = "   " + truncatedText.Text.Length + "    |";
                    qtdD = "   " + truncatedText.TruncatedText.Length + "    |";
                }
                else
                {
                    lex = row.SyntaxToken.Text;

                    while (lex.Length <= 30)
                        lex = lex + " ";
                    lex = lex + " |";

                    qtdA = "   " + row.SyntaxToken.Text.Length + "    |";
                    qtdD = qtdA;
                }

                if (row.SyntaxToken.Kind == Enum.SyntaxKind.IntegerToken)
                    tokenType = "  INT   |";
                else if (row.SyntaxToken.Kind == Enum.SyntaxKind.DoubleToken)
                    tokenType = "  REAL  |";
                else if (row.SyntaxToken.Kind == Enum.SyntaxKind.NotReservedKeyword)
                    tokenType = "  VOID  |";

                lines = "  " + string.Join(", ", row.IndexLineList);


                tableRows.Add(index + code + lex + qtdA + qtdD + tokenType + lines);
            }


            return tableRows;

        }

        private static List<string> FormateLexTableReport(List<LexRow> lexTableRow)
        {

            List<string> tableRows = new List<string>();
            string symbolTableIndex = "---";


            foreach (LexRow row in lexTableRow)
            {

                if (row.SymbolTableIndex != null)
                {
                    symbolTableIndex = row.SymbolTableIndex.ToString();
                }
                else
                {
                    symbolTableIndex = "---";
                }


                tableRows.Add(row.Text + " |" + row.AtomCodeId + " |" + symbolTableIndex);
            }


            return tableRows;

        }

        public static void FileWriter(List<SymbolTableRow> symbolTableRow, List<LexRow> lexTableRow)
        {
            // If directory does not exist, create it.  
            if (File.Exists(@"C:\Users\Lin\work\orm-plus-compiler\Test.TAB"))
                File.Delete(@"C:\Users\Lin\work\orm-plus-compiler\Test.TAB");

            if (File.Exists(@"C:\Users\Lin\work\orm-plus-compiler\Test.LEX"))
                File.Delete(@"C:\Users\Lin\work\orm-plus-compiler\Test.LEX");

            Console.WriteLine("Creating the file.TAB...");
            StreamWriter symbolTableFile = new StreamWriter(@"C:\Users\Lin\work\orm-plus-compiler\Test.TAB", true);
            Console.WriteLine("File Create!");
            Console.WriteLine("Writing...\n \n");

            symbolTableFile.WriteLine("Tabela de simbolos\n");
            symbolTableFile.WriteLine("  Index  |  Cod  |              Lexeme            |  QtdA  |  QtdD  |  Tipo  |  Linhas");
            List<string> stringSymbolTableRow = FormateSymbolTableReport(symbolTableRow);

            foreach (string s in stringSymbolTableRow)
                symbolTableFile.WriteLine(s);

            symbolTableFile.Close();


            Console.WriteLine("Creating the file.LEX...");
            StreamWriter lexTableFile = new StreamWriter(@"C:\Users\Lin\work\orm-plus-compiler\Test.LEX", true);
            Console.WriteLine("File Create!");
            Console.WriteLine("Writing...\n \n");

            lexTableFile.WriteLine("Tabela Lex\n");
            lexTableFile.WriteLine("Lexeme  |  Átomo  | Índice na Tabela de Símbolos");
            List<string> stringLexTableRow = FormateLexTableReport(lexTableRow);

            foreach (string s in stringLexTableRow)
                lexTableFile.WriteLine(s);

            lexTableFile.Close();
        }

        private static string lineFilter(string[] line)
        {
            List<string> filteredLine = new List<string>();

            foreach (string l in line)
            {
                if (!string.IsNullOrWhiteSpace(l) || !string.IsNullOrEmpty(l))
                    filteredLine.Add(l);
            }

            return string.Join(" ", filteredLine);
        }
    }
}
