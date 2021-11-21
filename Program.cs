using orm_plus_compiler.StaticChecker.Binding;
using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Files;
using orm_plus_compiler.StaticChecker.Syntax.Structs;
using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace orm_plus_compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var showTree = false;

            //Code code = FileManager.FileReader();

            while (true)
            {
                Code code = FileManager.FileReader();
              /*  Console.Write("> ");

                var line = Console.ReadLine();*/

                List<SymbolTableRow> symbolDatasList = new List<SymbolTableRow>();

                foreach(CodeLine line in code.CodeLines)
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
                            if(!symbolDatasList.Any(s => s.SyntaxToken.Text == token.Text))
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


             /*   if (string.IsNullOrWhiteSpace(line))
                {
                    return;
                }

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees");
                    continue;
                }
                else if (line == "clear")
                {
                    Console.Clear();
                    continue;
                } */



              /*  var syntaxTree = SyntaxTree.Parse(line);
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);

                var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();

                if (showTree)
                    TreePrint(syntaxTree.Root);

                if (diagnostics.Any())
                {
                    foreach (var diagnostic in diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }
                }
                else
                {
                    var e = new Evaluator(boundExpression);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }*/
            }
        }

        static void TreePrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                TreePrint(child, indent, child == lastChild);
        }
    }
}
