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

            if (index >= _text.Length)
            {
                return '\0';
            }

            return _text[index];
        }

        private void Next()
        {
            _position++;
        }

        private SyntaxKind DefineNumberType(string value)
        {
            if (Int64.TryParse(value, out Int64 intType) || !value.Contains('.'))
                return SyntaxKind.IntegerToken;
            else if (Decimal.TryParse(value, out decimal decimalType) || float.TryParse(value, out float floatType))
                return SyntaxKind.DoubleToken;
            else
                return SyntaxKind.NotReservedKeyword;
        }

        public SyntaxToken Lex()
        {

            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null, null);

            if (char.IsLetterOrDigit(Current))
            {
                var start = _position;

                while (char.IsLetterOrDigit(Current) || Current.Equals('.') || Current.Equals('-') || OrmLanguageFacts.ValidEspecialCharList.Contains(Current))
                    Next();

                int length = _position - start;
                string text = _text.Substring(start, length);
                SyntaxKind kind;
                string atomCodeId = string.Empty;

                if (!float.TryParse(text, out float value))
                {
                    kind = OrmLanguageFacts.GetKeywordKind(text);
                    atomCodeId = OrmLanguageFacts.GetAtomCodeId(kind);
                }
                else
                {
                    kind = DefineNumberType(text);
                    atomCodeId = OrmLanguageFacts.GetAtomCodeId(kind);

                }

                if (length >= 30)
                {
                    if(kind == SyntaxKind.IntegerToken || kind == SyntaxKind.DoubleToken)
                    {

                        kind = DefineNumberType(text.Substring(0, 30));
                        atomCodeId = OrmLanguageFacts.GetAtomCodeId(kind);
                    }
                    return new TruncatedSyntaxToken(kind, start, text, text.Substring(0, 30), null, atomCodeId);
                }

                return new SyntaxToken(kind, start, text, null, atomCodeId);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null, null);
            }


            if (Current.Equals('\''))
            {
                var start = _position;

                while (char.IsLetter(Current) || Current.Equals('\''))
                    Next();

                int length = _position - start;
                string text = _text.Substring(start, length);
                text = text.Replace("\'", "");
                SyntaxKind kind = text.Length == 1 ? SyntaxKind.ConstChar : SyntaxKind.NotReservedKeyword;
                string atomCodeId = OrmLanguageFacts.GetAtomCodeId(kind);

                if (length >= 30)
                {
                    return new TruncatedSyntaxToken(kind, start, text, text.Substring(0, 30), null, atomCodeId);
                }

                return new SyntaxToken(kind, start, text, null, atomCodeId);
            }

            if (Current.Equals('\"'))
            {
                var start = _position;

                while (char.IsLetterOrDigit(Current) || Current.Equals('\"') || Current.Equals(' ') 
                    || Current.Equals('-') || OrmLanguageFacts.ValidEspecialCharList.Contains(Current))
                    Next();

                int length = _position - start;
                string text = _text.Substring(start, length);
                text = text.Replace("\"", "");
                SyntaxKind kind = SyntaxKind.ConstString;
                string atomCodeId = OrmLanguageFacts.GetAtomCodeId(kind);

                if (length >= 30)
                {
                    return new TruncatedSyntaxToken(kind, start, text, text.Substring(0, 30), null, atomCodeId);
                }

                return new SyntaxToken(kind, start, text, null, atomCodeId);
            }

            if (OrmLanguageFacts.isCurrentAndLookaheadDoubleOperator(Current, Lookahead))
            {
                var atom = OrmLanguageFacts.doubleOperatorMapping[OrmLanguageFacts.buildStringFromCurrentAndLookahead(Current, Lookahead)];
                return new SyntaxToken(atom.Kind, _position += 2, atom.TextRepresentation, null, atom.CodeId);
            }

            if (OrmLanguageFacts.singleOperatorMapping.ContainsKey(Current))
            {
                var atom = OrmLanguageFacts.singleOperatorMapping[Current];
                return new SyntaxToken(atom.Kind, _position++, atom.TextRepresentation, null, atom.CodeId);
            }

            _diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadExpressionToken, _position++, _text.Substring(_position - 1, 1), null, null);
        }

    }
}
