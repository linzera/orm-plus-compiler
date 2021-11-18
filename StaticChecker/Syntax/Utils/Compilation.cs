using System;
using System.Linq;
using orm_plus_compiler.StaticChecker.Syntax.Structs;
using orm_plus_compiler.StaticChecker.Binding;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    public sealed class Compilation
    {
        public Compilation(SyntaxTree syntaxTree)
        {
            Syntax = syntaxTree;
        }

        public SyntaxTree Syntax { get; }

        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var boundExpression = binder.BindExpression(Syntax.Root);
            var evaluator = new Evaluator(boundExpression);

            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();

            if (diagnostics.Any())
                return new EvaluationResult(diagnostics, null);

            var value = evaluator.Evaluate();

            return new EvaluationResult(Array.Empty<Diagnostic>(), value);
        }

    }
}
