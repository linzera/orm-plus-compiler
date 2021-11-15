using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text)
        {
            this._text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current => Peek(0);
        private char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;

            if (_position >= _text.Length)
            {
                return '\0';
            }

            return _text[index];
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()
        {

            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Next();

                var digitLength = _position - start;
                var text = _text.Substring(start, digitLength);
                if (!int.TryParse(text, out int value))
                {
                    _diagnostics.Add($"The number {_text} can not be represented as an Int32");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                var start = _position;

                while (char.IsLetter(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = OrmLanguageFacts.GetKeywordKind(text);

                return new SyntaxToken(kind, start, text, null);
            }

            if (OrmLanguageFacts.singleOperatorMapping.ContainsKey(Current))
            {
                var atom = OrmLanguageFacts.singleOperatorMapping[Current];
                return new SyntaxToken(atom.Kind, _position++, atom.TextRepresentation, null);
            }

            if (OrmLanguageFacts.isCurrentAndLookaheadDoubleOperator(Current, Lookahead))
            {
                var atom = OrmLanguageFacts.doubleOperatorMapping[OrmLanguageFacts.buildStringFromCurrentAndLookahead(Current, Lookahead)];
                return new SyntaxToken(atom.Kind, _position += 2, atom.TextRepresentation, null);
            }

            _diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadExpressionToken, _position++, _text.Substring(_position - 1, 1), null);
        }

    }
}
