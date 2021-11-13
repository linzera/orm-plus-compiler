using OrmPlusCompiler.StaticChecker;
using OrmPlusCompiler.StaticChecker.Syntax;
using OrmPlusCompiler.StaticChecker.Binding;

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
