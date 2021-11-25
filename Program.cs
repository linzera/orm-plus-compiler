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

                SymbolTable.RowFormation(code.CodeLines);

                FileManager.FileWriter(SymbolTable.symbolDataList, SymbolTable.lexDataList);
            }
        }
    }
}
