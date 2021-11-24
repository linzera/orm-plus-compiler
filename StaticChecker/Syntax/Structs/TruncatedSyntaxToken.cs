using orm_plus_compiler.StaticChecker.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    class TruncatedSyntaxToken : SyntaxToken
    {

        public string TruncatedText { get; }

        public TruncatedSyntaxToken(SyntaxKind kind, int position, string text, string truncatedText, object value, string atomCodeId) : base(kind, position, text, value, atomCodeId)
        {
            TruncatedText = truncatedText;
        }
    }
}
