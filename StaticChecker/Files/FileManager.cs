﻿using orm_plus_compiler.StaticChecker.Syntax.Structs;
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
                string path = Console.ReadLine().Replace("\"", "");

                if (!Path.GetExtension(path).Equals(".202"))
                    Console.WriteLine(" \nERRO: Invalid file format, please select a .202 extation file\n");
                else
                {
                    try
                    {
                        StreamReader rd = new StreamReader(path);
                        List<CodeLine> codeLineList = new List<CodeLine>();
                        int index = 1;
                        bool blockComment = false;
                        bool blockCommentAnotherLine = false;
                        while (!rd.EndOfStream)
                        {
                            string line = rd.ReadLine();

                            if (!blockComment && line.Contains("/*") && !line.Contains("*/"))
                                blockComment = true;

                            if (blockComment && line.Contains("*/") && line.Contains("/*"))
                                blockComment = false;

                            if (blockComment && line.Contains("*/"))
                                blockCommentAnotherLine = true;

                            if ((!blockComment || blockCommentAnotherLine) && (!string.IsNullOrWhiteSpace(line) || !string.IsNullOrEmpty(line)))
                            {
                                string filteredLine = lineFilter(line, blockComment);
                                if(!string.IsNullOrEmpty(filteredLine) || !string.IsNullOrWhiteSpace(filteredLine))
                                {
                                    CodeLine codeLine = new CodeLine(index, filteredLine);
                                    codeLineList.Add(codeLine);
                                }
                            }

                            index++;
                        }

                        Code code = new Code(Path.GetFileNameWithoutExtension(path), Path.GetExtension(path), codeLineList, Path.GetFullPath(path));

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

        private static string lineFilter(string line, bool blockCommentAnotherLine)
        {
            string filterWord = string.Empty;
            bool blockComment = false;
            bool lineComment = false;

            int barIndex = line.IndexOf('/');
            int charIndex = 0;

            foreach (char c in line)
            {
                if (barIndex + 1 <= line.Length && barIndex > -1 && charIndex < line.Length - 1)
                {
                    if (line[charIndex].Equals('/') && line[charIndex + 1].Equals('/'))
                        lineComment = true;

                    if (line[charIndex].Equals('/') && line[charIndex + 1].Equals('*'))
                        blockComment = true;
                }
                    
                if (!lineComment)
                    if ((c.Equals(' ') || c.Equals('-') || OrmLanguageFacts.ValidEspecialCharList.Contains(c) || char.IsLetterOrDigit(c) || 
                        OrmLanguageFacts.singleOperatorMapping.Any(a => a.Key.Equals(c)) || c.Equals('\"') || c.Equals('\'')) && !blockComment && !blockCommentAnotherLine)
                    {
                            filterWord += c;
                    }

                if (charIndex - 1 >= 0 && blockComment)
                    if (c.Equals('/') && line[charIndex - 1].Equals('*'))
                        blockComment = false;

                charIndex++;
            }

            return filterWord;
        }

        public static void FileWriter(List<SymbolTableRow> symbolTableRow, List<LexRow> lexTableRow, string path)
        {
            string symbolTablePath = path.Replace(".202", ".TAB");
            string lexTablePath = path.Replace(".202", ".LEX");

            // If directory does not exist, create it.  
            if (File.Exists(symbolTablePath))
                File.Delete(symbolTablePath);

            if (File.Exists(lexTablePath))
                File.Delete(lexTablePath);

            WriteTABFile(symbolTablePath, symbolTableRow);

            Console.WriteLine("Done!\n");

            WriteLEXFile(lexTablePath, lexTableRow);

            Console.WriteLine("Done!\n");

            Console.WriteLine(">> The reports were creat on the same directory entered previously <<\n");
        }

        private static void WriteTABFile(string symbolTablePath, List<SymbolTableRow> symbolTableRow)
        {
            Console.WriteLine("Creating the file.TAB...");
            StreamWriter symbolTableFile = new StreamWriter(symbolTablePath, true);
            Console.WriteLine("File Create!");
            Console.WriteLine("Writing...\n");

            //WriteHeader(symbolTableFile);
            symbolTableFile.WriteLine(">> Tabela de simbolos <<\n");
            symbolTableFile.WriteLine("  Index  |  Cod  |              Lexeme            |  QtdA  |  QtdD  |  Tipo  |  Linhas");
            List<string> stringSymbolTableRow = FormateSymbolTableReport(symbolTableRow);

            foreach (string s in stringSymbolTableRow)
                symbolTableFile.WriteLine(s);

            symbolTableFile.Close();
        }
        private static void WriteLEXFile(string lexTablePath, List<LexRow> lexTableRow)
        {
            Console.WriteLine("Creating the file.LEX...");
            StreamWriter lexTableFile = new StreamWriter(lexTablePath, true);
            Console.WriteLine("File Create!");
            Console.WriteLine("Writing...\n");

            WriteHeader(lexTableFile);
            lexTableFile.WriteLine("\n>> Relatório da análise léxica <<\n");
            lexTableFile.WriteLine("Lexeme                          |  Átomo  | Índice na Tabela de Símbolos");
            List<string> stringLexTableRow = FormateLexTableReport(lexTableRow);

            foreach (string s in stringLexTableRow)
                lexTableFile.WriteLine(s);

            lexTableFile.Close();
        }

        private static void WriteHeader(StreamWriter file)
        {
            file.WriteLine("- Codigo da equipe: E04");
            file.WriteLine("- Componentes:");
            file.WriteLine("Ana Paula Tartari Seidenstucker       ana.seidenstucker@ucsal.edu.br   71 9 9397-0202");
            file.WriteLine("Gean Carlos de Souza Almeida          gean.almeida@ucsal.edu.br        71 9 9673-2001");
            file.WriteLine("Italo Gabriel Rocha Lino              italo.lino@ucsal.edu.br          71 9 8742-1452");
            file.WriteLine("Felipe Augusto da E. Moreira          felipea.moreira@ucsal.edu.br     71 9 8775-6078");
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
                    index = "   0" + row.Index + "   |";
                else
                    index = " " + row.Index + "   |";

                code = $"  {row.SyntaxToken.SyntaxAtomCodeId}  |";


                if (row.SyntaxToken.GetType() == typeof(TruncatedSyntaxToken))
                {
                    TruncatedSyntaxToken truncatedText = row.SyntaxToken as TruncatedSyntaxToken;
                    lex = truncatedText.TruncatedText + "  |";

                    if (truncatedText.TruncatedText.Length < 10)
                    {
                        qtdA = "   " + truncatedText.Text.Length + "    |";
                        qtdD = "   " + truncatedText.TruncatedText.Length + "    |";
                    }
                    else if (truncatedText.TruncatedText.Length < 100)
                    {
                        qtdA = "   " + truncatedText.Text.Length + "   |";
                        qtdD = "   " + truncatedText.TruncatedText.Length + "   |";
                    }
                    else
                    {
                        qtdA = "   " + truncatedText.Text.Length + "  |";
                        qtdD = "   " + truncatedText.TruncatedText.Length + "  |";
                    }

                }
                else
                {
                    lex = row.SyntaxToken.Text;

                    while (lex.Length <= 30)
                        lex = lex + " ";
                    lex = lex + " |";

                    if (row.SyntaxToken.Text.Length < 10)
                    {
                        qtdA = "   " + row.SyntaxToken.Text.Length + "    |";
                    }
                    else if (row.SyntaxToken.Text.Length < 100)
                    {
                        qtdA = "   " + row.SyntaxToken.Text.Length + "   |";
                    }
                    else
                    {
                        qtdA = "   " + row.SyntaxToken.Text.Length + "  |";
                    }

                    qtdD = qtdA;

                }

                if (row.SyntaxToken.Kind == Enum.SyntaxKind.IntegerToken)
                    tokenType = "  INT   |";
                else if (row.SyntaxToken.Kind == Enum.SyntaxKind.DoubleToken)
                    tokenType = "  REAL  |";
                else if (row.SyntaxToken.Kind == Enum.SyntaxKind.ConstChar)
                    tokenType = "  CHAR  |";
                else if (row.SyntaxToken.Kind == Enum.SyntaxKind.ConstString)
                    tokenType = " STRING |";
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
            string symbolTableIndex = string.Empty;

            foreach (LexRow row in lexTableRow)
            {
                symbolTableIndex = " ---";

                if (row.SymbolTableIndex != null)
                    symbolTableIndex = " " + row.SymbolTableIndex.ToString();

                string rowLex = row.Text + "   " + row.AtomCodeId + "   " + "|" + symbolTableIndex;

                if (!tableRows.Contains(rowLex))
                    tableRows.Add(rowLex);
            }

            return tableRows;

        }
    }
}
