namespace AjLisp.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PredicateTests
    {
        [TestMethod]
        public void IsFalse()
        {
            Assert.IsTrue(Predicates.IsFalse(false));
            Assert.IsTrue(Predicates.IsFalse(null));
            Assert.IsFalse(Predicates.IsFalse(true));
            Assert.IsFalse(Predicates.IsFalse(1));
        }

        [TestMethod]
        public void IsTrue()
        {
            Assert.IsFalse(Predicates.IsTrue(false));
            Assert.IsFalse(Predicates.IsTrue(null));
            Assert.IsTrue(Predicates.IsTrue(true));
            Assert.IsTrue(Predicates.IsTrue(1));
        }

        [TestMethod]
        public void IsString()
        {
            Assert.IsFalse(Predicates.IsString(false));
            Assert.IsFalse(Predicates.IsString(null));
            Assert.IsFalse(Predicates.IsString(true));
            Assert.IsFalse(Predicates.IsString(1));
            Assert.IsTrue(Predicates.IsString(string.Empty));
            Assert.IsTrue(Predicates.IsString("foo"));
        }
    }
}
