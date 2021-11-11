namespace OrmPlusCompiler.StaticChecker;

class Lexer
{
    private readonly string _text;
    private int _position;

    private Dictionary<Char, Atom> operatorMapping = new Dictionary<Char, Atom>{
        {'(', new Atom(SyntaxKind.OpenParenthesisToken, "S06", "(")},
        {')', new Atom(SyntaxKind.CloseParenthesisToken, "S07", ")")},
        {'+', new Atom(SyntaxKind.PlusToken, "S16", "+")},
        {'-', new Atom(SyntaxKind.MinusToken, "S17", "-")},
        {'*', new Atom(SyntaxKind.StarToken, "S18", "*")},
        {'/', new Atom(SyntaxKind.SlashToken, "S19", "/")},
    };

    public Lexer(string text)
    {
        this._text = text;
    }

    private char Current
    {
        get
        {

            if (_position >= _text.Length)
            {
                return '\0';
            }

            return _text[_position];

        }
    }

    private void Next()
    {
        _position++;
    }

    public SyntaxToken ReadToken()
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
            int.TryParse(text, out int value);
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


        if (operatorMapping.ContainsKey(Current))
        {
            var atom = operatorMapping[Current];
            return new SyntaxToken(atom.Kind, _position++, atom.TextRepresentation, null);
        }

        return new SyntaxToken(SyntaxKind.BadExpressionToken, _position++, _text.Substring(_position - 1, 1), null);
    }


}