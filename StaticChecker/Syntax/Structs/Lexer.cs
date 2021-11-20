using orm_plus_compiler.StaticChecker.Enum;
using orm_plus_compiler.StaticChecker.Syntax.Utils;

namespace orm_plus_compiler.StaticChecker.Syntax.Structs
{
    class Lexer
    {
        private readonly string _text;
        private int _position;
        private DiagnosticBag _diagnostics = new DiagnosticBag();

        public Lexer(string text)
        {
            this._text = text;
        }

        public DiagnosticBag Diagnostics => _diagnostics;

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

            var start = _position;

            if (char.IsDigit(Current))
            {
                while (char.IsDigit(Current))
                    Next();

                var digitLength = _position - start;
                var text = _text.Substring(start, digitLength);
                if (!int.TryParse(text, out int value))
                {
                    _diagnostics.ReportInvalidNumber(new TextSpan(start, digitLength), _text, typeof(int));
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                while (char.IsLetter(Current) || Current.Equals('-'))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = OrmLanguageFacts.GetKeywordKind(text);

                if (length >= 30)
                {
                    return new TruncatedSyntaxToken(kind, start, text, text.Substring(0, 30), null);
                }

                return new SyntaxToken(kind, start, text, null);
            }

            if (OrmLanguageFacts.isCurrentAndLookaheadDoubleOperator(Current, Lookahead))
            {
                var atom = OrmLanguageFacts.doubleOperatorMapping[OrmLanguageFacts.buildStringFromCurrentAndLookahead(Current, Lookahead)];
                _position += 2;
                return new SyntaxToken(atom.Kind, start, atom.TextRepresentation, null);
            }

            if (OrmLanguageFacts.singleOperatorMapping.ContainsKey(Current))
            {
                var atom = OrmLanguageFacts.singleOperatorMapping[Current];
                return new SyntaxToken(atom.Kind, _position++, atom.TextRepresentation, null);
            }



            _diagnostics.ReportBadCharacter(_position, Current);
            return new SyntaxToken(SyntaxKind.BadExpressionToken, _position++, _text.Substring(_position - 1, 1), null);
        }

    }
}
