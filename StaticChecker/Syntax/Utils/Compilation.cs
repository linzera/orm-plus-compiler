using System;
using System.Linq;
using System.Collections.Generic;
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

        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            var binder = new Binder(variables);
            var boundExpression = binder.BindExpression(Syntax.Root);


            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();

            if (diagnostics.Any())
                return new EvaluationResult(diagnostics, null);

            var evaluator = new Evaluator(boundExpression, variables);
            var value = evaluator.Evaluate();

            return new EvaluationResult(Array.Empty<Diagnostic>(), value);
        }

    }
}
