namespace AjLisp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AjLisp.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExamplesTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        [DeploymentItem("Examples\\Core.lsp")]
        public void MapFirst()
        {
            this.LoadFile("Core.lsp");
            this.Evaluate("(define inc (x) (plus x 1))");

            this.EvaluateAndCompare("(mapfirst inc '(1 2 3))", "(2 3 4)");
            this.EvaluateAndCompare("(mapfirst inc nil)", "nil");
        }

        [TestMethod]
        [DeploymentItem("Examples\\Core.lsp")]
        public void ReverseList()
        {
            this.LoadFile("Core.lsp");
            this.EvaluateAndCompare("(reverse nil)", "nil");
            this.EvaluateAndCompare("(reverse '(a))", "(a)");
            this.EvaluateAndCompare("(reverse '(a b))", "(b a)");
            this.EvaluateAndCompare("(reverse '(a b c))", "(c b a)");
            this.EvaluateAndCompare("(reverse '(a (b c) d))", "(d (b c) a)");
        }

        private object Evaluate(string text)
        {
            Parser parser = new Parser(text);

            return this.machine.Evaluate(parser.Compile());
        }

        private void LoadFile(string filename)
        {
            Parser parser = new Parser(new Lexer(new StreamReader(filename)));

            object expression;

            while ((expression = parser.Compile()) != null)
                this.machine.Evaluate(expression);
        }

        private void EvaluateAndCompare(string text, string expected)
        {
            object result = this.Evaluate(text);
            Assert.AreEqual(expected, Conversions.ToPrintString(result));
        }
    }
}
