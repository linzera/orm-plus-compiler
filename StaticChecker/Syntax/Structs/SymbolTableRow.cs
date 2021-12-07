using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    internal class SymbolTableRow
    {
        public SyntaxToken SyntaxToken { get; set; }

        public List<int> IndexLineList { get; set; }

        public int Index { get; set; }

        public SymbolTableRow() 
        {
            IndexLineList = new List<int>();
        }

        public SymbolTableRow(SyntaxToken syntaxToken, List<int> indexLineList, int index)
        {
            SyntaxToken = syntaxToken;
            Index = index;
            IndexLineList = indexLineList;
        }

    }
}
