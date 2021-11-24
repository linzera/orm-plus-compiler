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

            while (true)
            {
                Code code = FileManager.FileReader();

                List<SymbolTableRow> symbolDatasList = SymbolTable.RowFormation(code.CodeLines);

                FileManager.FileWriter(symbolDatasList);

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
