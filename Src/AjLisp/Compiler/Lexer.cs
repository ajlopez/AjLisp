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

            char ch;

            try
            {
                ch = this.NextCharSkipBlanks();

                if (char.IsLetter(ch) | ch == '_')
                    return this.NextName(ch);

                if (Separators.IndexOf(ch) >= 0)
                    return this.NextSeparator(ch);

                if (ch == '"')
                    return this.NextString();

                if (ch == '\'')
                    return new Token() { Type = TokenType.Name, Value = "'" };

                if (ch == '.')
                    return new Token() { Type = TokenType.Name, Value = "." };

                if (ch == '`')
                    return new Token() { Type = TokenType.Name, Value = "`" };

                if (ch == '-')
                {
                    try
                    {
                        char ch2 = this.NextChar();

                        if (char.IsDigit(ch2))
                        {
                            Token token = this.NextInteger(ch2);

                            if (token.Value is int)
                                token.Value = -((int)token.Value);
                            if (token.Value is double)
                                token.Value = -((double)token.Value);

                            return token;
                        }

                        this.PushChar(ch2);
                    }
                    catch (EndOfInputException)
                    {
                    }

                    return new Token() { Type = TokenType.Name, Value = "-" };
                }

                if (char.IsDigit(ch))
                    return this.NextInteger(ch);

                return this.NextSpecialName(ch);
            }
            catch (EndOfInputException)
            {
                return null;
            }
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

        private char NextChar()
        {
            if (this.hasChar)
            {
                this.hasChar = false;
                return this.lastChar;
            }

            int ch;

            ch = this.input.Read();

            if (ch < 0)
                throw new EndOfInputException();

            return (char)ch;
        }

        private void SkipToControl()
        {
            char ch;

            ch = this.NextChar();

            while (!char.IsControl(ch))
                ch = this.NextChar();
        }

        private char NextCharSkipBlanks()
        {
            char ch;

            ch = this.NextChar();

            while (char.IsWhiteSpace(ch))
                ch = this.NextChar();

            return ch;
        }

        private Token NextName(char firstChar)
        {
            string name;

            name = firstChar.ToString();

            char ch;

            try
            {
                ch = this.NextChar();

                while (!char.IsWhiteSpace(ch) && Separators.IndexOf(ch) < 0)
                {
                    name += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Type = TokenType.Name;
            token.Value = name;
            return token;
        }

        private Token NextSpecialName(char firstChar)
        {
            string name;

            name = firstChar.ToString();

            char ch;

            try
            {
                ch = this.NextChar();

                while (!char.IsWhiteSpace(ch) && !char.IsLetterOrDigit(ch) && Separators.IndexOf(ch) < 0)
                {
                    name += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Type = TokenType.Name;
            token.Value = name;
            return token;
        }

        private Token NextString()
        {
            string value = string.Empty;
            char ch;

            ch = this.NextChar();

            while (ch != '"')
            {
                value += ch;
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

            char ch;

            try
            {
                ch = this.NextChar();

                while (char.IsDigit(ch))
                {
                    value += ch;
                    ch = this.NextChar();
                }

                if (ch == '.')
                {
                    value += ch;
                    return this.NextReal(value);
                }

                if (ch == '/')
                    return this.NextRationalNumber(Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture));

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
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

            char ch;

            try
            {
                ch = this.NextChar();

                while (char.IsDigit(ch))
                {
                    value += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();

            token.Type = TokenType.Number;

            long denominator = System.Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture);

            token.Value = RationalNumber.Create(numerator, denominator);

            return token;
        }

        private Token NextReal(string value)
        {
            char ch;

            try
            {
                ch = this.NextChar();

                while (char.IsDigit(ch))
                {
                    value += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

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

    public class EndOfInputException : Exception
    {
        public EndOfInputException()
            : base("End of Input")
        {
        }
    }

    public class TokenizerException : Exception
    {
        public TokenizerException(string msg) :
            base(msg)
        {
        }
    }
}
