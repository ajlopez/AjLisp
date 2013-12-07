namespace AjLisp.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjLisp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeUtilitiesTests
    {
        [TestMethod]
        public void GetTypeByName()
        {
            Type type = TypeUtilities.GetType("System.Int32");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(int));
        }

        [TestMethod]
        public void GetTypeStoredInEnvironment()
        {
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("int", typeof(int));

            Type type = TypeUtilities.GetType(environment, "int");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(int));
        }

        [TestMethod]
        public void GetTypeInAnotherAssembly()
        {
            Type type = TypeUtilities.GetType(new ValueEnvironment(), "System.Data.DataSet");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(System.Data.DataSet));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Unknown Type 'Foo.Bar'")]
        public void RaiseIfUnknownType()
        {
            TypeUtilities.GetType(new ValueEnvironment(), "Foo.Bar");
        }

        [TestMethod]
        public void AsType()
        {
            Assert.IsNotNull(TypeUtilities.AsType("System.IO.File"));
            Assert.IsNull(TypeUtilities.AsType("Foo.Bar"));
        }

        [TestMethod]
        public void IsNamespace()
        {
            Assert.IsTrue(TypeUtilities.IsNamespace("System"));
            Assert.IsTrue(TypeUtilities.IsNamespace("AjLisp"));
            Assert.IsTrue(TypeUtilities.IsNamespace("AjLisp.Language"));
            Assert.IsTrue(TypeUtilities.IsNamespace("System.IO"));
            Assert.IsTrue(TypeUtilities.IsNamespace("System.Data"));

            Assert.IsFalse(TypeUtilities.IsNamespace("Foo.Bar"));
        }

        [TestMethod]
        public void GetValueFromType()
        {
            Assert.IsFalse((bool)TypeUtilities.InvokeTypeMember(typeof(System.IO.File), "Exists", new object[] { "unknown.txt" }));
        }
    }
}
