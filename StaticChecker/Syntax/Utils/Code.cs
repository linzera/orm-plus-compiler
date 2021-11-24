using System.Collections.Generic;

namespace orm_plus_compiler.StaticChecker.Syntax.Utils
{
    class Code
    {
        public string FileName { get; }

        public string FileExtention { get; set; }

        public List<CodeLine> CodeLines { get; }

        public Code(string fileName, string fileExtention, List<CodeLine> codeLines)
        {
            FileName = fileName;
            FileExtention = fileExtention;
            CodeLines = codeLines;
        }
    }
}
