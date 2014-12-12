namespace AjLisp.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using AjLisp.Language;

    public enum TokenType
    {
        Name,
        Number,
        String,
        Separator
    }

    public class Lexer
    {
        private const string NameChars = "?_";
        private const string Separators = "()";

        private TextReader input;
        private char lastChar;
        private bool hasChar;
        private Token lastToken;
        private bool hasToken;

        public Lexer(TextReader input)
        {
            this.input = input;
        }

        public Lexer(string input)
            : this(new StringReader(input))
        {
        }

        public Token NextToken()
        {
            if (this.hasToken)
            {
                this.hasToken = false;
                return this.lastToken;
            }

            char? nch = this.NextCharSkipBlanks();

            if (!nch.HasValue)
                return null;

            char ch = nch.Value;

            if (char.IsLetter(ch) | ch == '_')
                return this.NextName(ch.ToString());

            if (Separators.IndexOf(ch) >= 0)
                return this.NextSeparator(ch);

            if (ch == '"')
                return this.NextString();

            if (ch == '\'')
                return new Token() { Type = TokenType.Name, Value = "'" };

            if (ch == '.')
            {
                char? ch2 = this.NextChar();

                if (ch2.HasValue)
                {
                    if (char.IsLetter(ch2.Value))
                        return this.NextName(ch.ToString() + ch2.Value.ToString());

                    this.PushChar(ch2.Value);
                }

                return new Token() { Type = TokenType.Name, Value = "." };
            }

            if (ch == '`')
                return new Token() { Type = TokenType.Name, Value = "`" };

            if (ch == '-')
            {
                char? ch2 = this.NextChar();

                if (ch2.HasValue)
                {
                    if (char.IsDigit(ch2.Value))
                    {
                        Token token = this.NextInteger(ch2.Value);

                        if (token.Value is int)
                            token.Value = -((int)token.Value);
                        if (token.Value is double)
                            token.Value = -((double)token.Value);

                        return token;
                    }

                    this.PushChar(ch2.Value);
                }

                return new Token() { Type = TokenType.Name, Value = "-" };
            }

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            return this.NextSpecialName(ch);
        }

        internal void PushToken(Token token)
        {
            this.lastToken = token;
            this.hasToken = true;
        }

        private void PushChar(char ch)
        {
            this.lastChar = ch;
            this.hasChar = true;
        }

        private char? NextChar()
        {
            if (this.hasChar)
            {
                this.hasChar = false;
                return this.lastChar;
            }

            int ch;

            ch = this.input.Read();

            if (ch < 0)
                return null;

            return (char)ch;
        }

        private void SkipToControl()
        {
            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && !char.IsControl(ch.Value))
                ch = this.NextChar();
        }

        private char? NextCharSkipBlanks()
        {
            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && char.IsWhiteSpace(ch.Value))
                ch = this.NextChar();

            return ch;
        }

        private Token NextName(string firsts)
        {
            string name;

            name = firsts;

            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && !char.IsWhiteSpace(ch.Value) && Separators.IndexOf(ch.Value) < 0)
            {
                name += ch;
                ch = this.NextChar();
            }

            if (ch.HasValue)
                this.PushChar(ch.Value);

            Token token = new Token();
            token.Type = TokenType.Name;
            token.Value = name;
            return token;
        }

        private Token NextSpecialName(char firstChar)
        {
            string name;

            name = firstChar.ToString();

            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && !char.IsWhiteSpace(ch.Value) && !char.IsLetterOrDigit(ch.Value) && Separators.IndexOf(ch.Value) < 0)
            {
                name += ch;
                ch = this.NextChar();
            }

            if (ch.HasValue)
                this.PushChar(ch.Value);

            Token token = new Token();
            token.Type = TokenType.Name;
            token.Value = name;
            return token;
        }

        private Token NextString()
        {
            string value = string.Empty;
            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && ch != '"')
            {
                value += ch.Value;
                ch = this.NextChar();
            }

            Token token = new Token();
            token.Type = TokenType.String;
            token.Value = value;

            return token;
        }

        private Token NextInteger(char firstDigit)
        {
            string value;

            value = new string(firstDigit, 1);

            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && char.IsDigit(ch.Value))
            {
                value += ch.Value;
                ch = this.NextChar();
            }

            if (ch.HasValue)
            {
                if (ch == '.')
                {
                    value += ch;
                    return this.NextReal(value);
                }

                if (ch == '/')
                    return this.NextRationalNumber(Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture));

                this.PushChar(ch.Value);
            }

            Token token = new Token();

            token.Type = TokenType.Number;

            long result = System.Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture);

            if (result >= Int32.MinValue && result <= Int32.MaxValue)
                token.Value = (int)result;
            else
                token.Value = result;

            return token;
        }

        private Token NextRationalNumber(long numerator)
        {
            string value = string.Empty;

            char? ch;

            ch = this.NextChar();

            while (ch.HasValue && char.IsDigit(ch.Value))
            {
                value += ch;
                ch = this.NextChar();
            }

            if (ch.HasValue)
            this.PushChar(ch.Value);

            Token token = new Token();

            token.Type = TokenType.Number;

            long denominator = System.Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture);

            token.Value = RationalNumber.Create(numerator, denominator);

            return token;
        }

        private Token NextReal(string value)
        {
            char? ch;

                ch = this.NextChar();

            while (ch.HasValue && char.IsDigit(ch.Value))
            {
                value += ch;
                ch = this.NextChar();
            }

            if (ch.HasValue)
                this.PushChar(ch.Value);

            Token token = new Token();

            token.Type = TokenType.Number;
            token.Value = System.Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);

            return token;
        }

        private Token NextSeparator(char ch)
        {
            Token token = new Token();
            token.Type = TokenType.Separator;
            token.Value = ch;
            return token;
        }
    }

    public class Token
    {
        public TokenType Type { get; set; }

        public object Value { get; set; }
    }

    public class TokenizerException : Exception
    {
        public TokenizerException(string msg) :
            base(msg)
        {
        }
    }
}
