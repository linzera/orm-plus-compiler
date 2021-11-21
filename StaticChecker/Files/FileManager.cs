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

        public static void FileWriter()
        {
           /* StreamWriter wr = new StreamWriter(@"C:\Users\anapa\OneDrive\Documents\GitHub\Faculdade\orm-plus-compiler\Test.LEX", true);
            wr.WriteLine("Este é o texto a escrever no arquivo");
            wr.Close();*/
        }

        private static string lineFilter(string[] line)
        {
            List<string> filteredLine = new List<string>();

            foreach(string l in line)
            {
                if (!string.IsNullOrWhiteSpace(l) || !string.IsNullOrEmpty(l))
                    filteredLine.Add(l);
            }

            return string.Join(" ", filteredLine);
        }
    }
}
