namespace AjLisp.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using AjLisp;
    using AjLisp.Compiler;
    using AjLisp.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MacroUtilitiesTests
    {
        [TestMethod]
        public void ShouldExpandConstants()
        {
            Assert.AreEqual(1, MacroUtilities.Expand(1, null));
            Assert.AreEqual("foo", MacroUtilities.Expand("foo", null));
        }
        
        [TestMethod]
        public void ShouldExpandSymbol()
        {
            Identifier identifier = new Identifier("foo");
            Assert.AreEqual(identifier, MacroUtilities.Expand(identifier, null));
        }

        [TestMethod]
        public void ShouldExpandSimpleList()
        {
            Parser parser = new Parser("(1 2 3)");
            object array = parser.Compile();
            object result = MacroUtilities.Expand(array, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            List resultList = (List)result;

            Assert.AreEqual(1, resultList.First);
            Assert.AreEqual(2, resultList.Next.First);
            Assert.AreEqual(3, resultList.Next.Next.First);
            Assert.IsNull(resultList.Next.Next.Next);
        }

        [TestMethod]
        public void ShouldExpandUnquotedSymbol()
        {
            Parser parser = new Parser("(unquote x)");
            object list = parser.Compile();
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("x", "y");

            object result = MacroUtilities.Expand(list, environment);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual("y", result);
        }

        [TestMethod]
        public void ShouldExpandUnquotedSymbolInList()
        {
            Parser parser = new Parser("(1 (unquote x) 3)");
            object list = parser.Compile();
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("x", 2);

            object result = MacroUtilities.Expand(list, environment);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            List resultList = (List)result;

            Assert.AreEqual(1, resultList.First);
            Assert.AreEqual(2, resultList.Next.First);
            Assert.AreEqual(3, resultList.Next.Next.First);
            Assert.IsNull(resultList.Next.Next.Next);
        }

        [TestMethod]
        public void ShouldExpandImplicitUnquotedSymbolInList()
        {
            Parser parser = new Parser("(1 ~x 3)");
            object list = parser.Compile();
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("x", 2);

            object result = MacroUtilities.Expand(list, environment);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            List resultList = (List)result;

            Assert.AreEqual(1, resultList.First);
            Assert.AreEqual(2, resultList.Next.First);
            Assert.AreEqual(3, resultList.Next.Next.First);
            Assert.IsNull(resultList.Next.Next.Next);
        }

        [TestMethod]
        public void ShouldExpandUnquotedSplicingSymbolInList()
        {
            Parser parser = new Parser("(2 3) (1 (unquote-splice x) 4)");
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("x", parser.Compile());

            object list = parser.Compile();
            object result = MacroUtilities.Expand(list, environment);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            List resultList = (List)result;

            Assert.AreEqual(1, resultList.First);
            Assert.AreEqual(2, resultList.Next.First);
            Assert.AreEqual(3, resultList.Next.Next.First);
            Assert.AreEqual(4, resultList.Next.Next.Next.First);
            Assert.IsNull(resultList.Next.Next.Next.Next);
        }
    }
}
