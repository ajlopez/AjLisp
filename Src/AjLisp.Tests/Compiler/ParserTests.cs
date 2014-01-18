namespace AjLisp.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Compiler;
    using AjLisp.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseAtom()
        {
            this.ParseAndCompare("a", "a");
        }

        [TestMethod]
        public void ParseAtomWithSpace()
        {
            this.ParseAndCompare(" a", "a");
        }

        [TestMethod]
        public void ParseInteger()
        {
            this.ParseAndCompare("123", "123");
        }

        [TestMethod]
        public void ParseNegativeInteger()
        {
            this.ParseAndCompare("-123", "-123");
        }

        [TestMethod]
        public void ParseNegativeReal()
        {
            this.ParseAndCompare("-12.34", "-12.34");
        }

        [TestMethod]
        public void ParseAtomWithSpaces()
        {
            this.ParseAndCompare(" a ", "a");
        }

        [TestMethod]
        public void ParseNil()
        {
            this.ParseAndCompare("nil", "nil");
        }

        [TestMethod]
        public void ParseTrue()
        {
            this.ParseAndCompare("t", "t");
        }

        [TestMethod]
        public void ParseSimpleList()
        {
            this.ParseAndCompare("(a)", "(a)");
        }

        [TestMethod]
        public void ParseList()
        {
            this.ParseAndCompare("(a b)", "(a b)");
        }

        [TestMethod]
        public void ParseDottedList()
        {
            this.ParseAndCompare("(a . b)", "(a . b)");
        }

        [TestMethod]
        public void ParseListInList()
        {
            this.ParseAndCompare("(a (b c) d)", "(a (b c) d)");
        }

        [TestMethod]
        public void ParseQuote()
        {
            this.ParseAndCompare("'a", "(quote a)");
        }

        [TestMethod]
        public void ParseString()
        {
            this.ParseAndCompare("\"foo\"", "\"foo\"");
        }

        [TestMethod]
        public void ParseQualifiedIdentifier()
        {
            this.ParseAndCompare("foo.bar", "foo.bar");
        }

        [TestMethod]
        public void ParseCommaName()
        {
            this.ParseAndCompare(",foo", "(comma foo)");
        }

        [TestMethod]
        public void ParseCommaAtName()
        {
            this.ParseAndCompare(",@foo", "(comma-at foo)");
        }

        [TestMethod]
        public void ParseBackquote()
        {
            this.ParseAndCompare("`foo", "(backquote foo)");
        }

        private object ParseExpression(string text)
        {
            Parser parser = new Parser(text);
            return parser.Compile();
        }

        private void ParseAndCompare(string text, string expected)
        {
            Parser parser = new Parser(text);
            object expr = parser.Compile();
            Assert.AreEqual(expected, Conversions.ToPrintString(expr));
        }
    }
}
