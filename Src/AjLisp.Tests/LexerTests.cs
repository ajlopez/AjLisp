using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjLisp.Compiler;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLisp.Language;

namespace AjLisp.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ParseSimpleName()
        {
            this.Tokenize("a", TokenType.Name, "a");
        }

        [TestMethod]
        public void ParseSimpleNameWithLeftSpaces()
        {
            this.Tokenize(" a", TokenType.Name, "a");
        }

        [TestMethod]
        public void ParseSimpleNameWithSpaces()
        {
            this.Tokenize(" a ", TokenType.Name, "a");
        }

        [TestMethod]
        public void ParseLeftParenthesis()
        {
            this.Tokenize("(", TokenType.Separator, '(');
        }

        [TestMethod]
        public void ParseRightParenthesis()
        {
            this.Tokenize(")", TokenType.Separator, ')');
        }

        [TestMethod]
        public void ParseQuote()
        {
            this.Tokenize("'", TokenType.Name, "'");
        }

        [TestMethod]
        public void ParseOneDigitInteger()
        {
            this.Tokenize("1", TokenType.Number, 1);
        }

        [TestMethod]
        public void ParseInteger()
        {
            this.Tokenize("1234", TokenType.Number, 1234);
        }

        [TestMethod]
        public void ParseLongInteger()
        {
            this.Tokenize("1234567890123", TokenType.Number, 1234567890123);
        }

        [TestMethod]
        public void ParseRationalNumber()
        {
            this.Tokenize("3/2", TokenType.Number, RationalNumber.Create(3,2));
        }

        [TestMethod]
        public void ParseNegativeInteger()
        {
            this.Tokenize("-1234", TokenType.Number, -1234);
        }

        [TestMethod]
        public void ParseReal()
        {
            this.Tokenize("12.34", TokenType.Number, 12.34);
        }

        [TestMethod]
        public void ParseNegativeReal()
        {
            this.Tokenize("-12.34", TokenType.Number, -12.34);
        }

        [TestMethod]
        public void ParseMinus()
        {
            this.Tokenize("-", TokenType.Name, "-");
        }

        [TestMethod]
        public void ParseString()
        {
            this.Tokenize("\"foo\"", TokenType.String, "foo");
        }

        [TestMethod]
        public void ParseSimpleDot()
        {
            this.Tokenize(".", TokenType.Name, ".");
        }

        [TestMethod]
        public void ParseQualifiedName()
        {
            this.Tokenize("System.foo", TokenType.Name, "System.foo");
        }

        [TestMethod]
        public void ParseSpecialCharsName()
        {
            this.Tokenize("<", TokenType.Name, "<");
            this.Tokenize("<=", TokenType.Name, "<=");
            this.Tokenize(">=", TokenType.Name, ">=");
            this.Tokenize("+", TokenType.Name, "+");
            this.Tokenize("-", TokenType.Name, "-");
            this.Tokenize("*", TokenType.Name, "*");
            this.Tokenize("/", TokenType.Name, "/");
        }

        [TestMethod]
        public void ParseComma()
        {
            this.Tokenize(",", TokenType.Name, ",");
        }

        [TestMethod]
        public void ParseCommaAt()
        {
            this.Tokenize(",@", TokenType.Name, ",@");
        }

        [TestMethod]
        public void ParseCommaAndName()
        {
            this.Tokenize(",foo", TokenType.Name, ",", TokenType.Name, "foo");
        }

        [TestMethod]
        public void ParseCommaAtAndName()
        {
            this.Tokenize(",@foo", TokenType.Name, ",@", TokenType.Name, "foo");
        }

        [TestMethod]
        public void ParseBackquoteAndQuote()
        {
            this.Tokenize("`'", TokenType.Name, "`", TokenType.Name, "'");
        }

        private void Tokenize(string text, TokenType type, object value)
        {
            Token token = this.Tokenize(text);

            Assert.IsNotNull(token);
            Assert.AreEqual(type, token.Type);
            Assert.AreEqual(value, token.Value);
        }

        private void Tokenize(string text, TokenType type, object value, TokenType type2, object value2)
        {
            Lexer tokenizer = new Lexer(text);

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(type, token.Type);
            Assert.AreEqual(value, token.Value);

            token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(type2, token.Type);
            Assert.AreEqual(value2, token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        private Token Tokenize(string text)
        {
            Lexer tokenizer = new Lexer(text);

            Token token = tokenizer.NextToken();

            Assert.IsNull(tokenizer.NextToken());

            return token;
        }
    }
}
