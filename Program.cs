using OrmPlusCompiler.StaticChecker;
class Program
{
    static void Main(string[] args)
    {

        var showTree = false;

        while (true)
        {
            Console.Write("> ");

            var line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
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
            }

            var syntaxTree = SyntaxTree.Parse(line);

            if (showTree)
                TreePrint(syntaxTree.Root);


            if (syntaxTree.Diagnostics.Any())
            {
                foreach (var diagnostic in syntaxTree.Diagnostics)
                {
                    Console.WriteLine(diagnostic);
                }
            }
            else
            {
                var e = new Evaluator(syntaxTree.Root);
                var result = e.Evaluate();
                Console.WriteLine(result);
            }
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
