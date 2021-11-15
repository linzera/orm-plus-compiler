using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    internal sealed class CodeLine
    {
        public int LineId { get; }

        public string Line { get; set; }

        public CodeLine(int lineId, string line)
        {
            LineId = lineId;
            Line = line;
        }
    }
}
