namespace AjLisp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Language;
    using AjLisp.Primitives;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrimitivesTests
    {
        [TestMethod]
        public void EvaluateInvokeFunctionWithSimpleObjectAndMethod()
        {
            SubrInvoke invoke = new SubrInvoke();

            object result = invoke.Apply(new List(123, new List("ToString")), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual("123", result);
        }

        [TestMethod]
        public void EvaluateInvokeFunctionWithSimpleObjectAndProperty()
        {
            SubrInvoke invoke = new SubrInvoke();

            object result = invoke.Apply(new List("foo", new List("Length")), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateInvokeFunctionWithObjectMethodAndArguments()
        {
            SubrInvoke invoke = new SubrInvoke();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(".");

            object result = invoke.Apply(new List(di, new List("GetFiles", new List("*.exe"))), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.FileInfo[]));
        }

        [TestMethod]
        public void EvaluateNewFunctionWithNativeObject()
        {
            FSubrNew fnew = new FSubrNew();

            object result = fnew.Apply(new List(typeof(System.IO.DirectoryInfo), new List(".")), new ValueEnvironment());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.DirectoryInfo));

            System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)result;

            Assert.AreEqual((new System.IO.DirectoryInfo(".")).FullName, di.FullName);
        }

        [TestMethod]
        public void EvaluateNewFunctionWithNativeObjectWithoutArguments()
        {
            FSubrNew fnew = new FSubrNew();

            object result = fnew.Apply(new List(typeof(System.Data.DataSet)), new ValueEnvironment());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.Data.DataSet));
        }

        [TestMethod]
        public void EvaluateNewFunctionWithStringTypeName()
        {
            FSubrNew fnew = new FSubrNew();

            object result = fnew.Apply(new List("System.IO.DirectoryInfo", new List(".")), new ValueEnvironment());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.DirectoryInfo));

            System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)result;

            Assert.AreEqual((new System.IO.DirectoryInfo(".")).FullName, di.FullName);
        }

        [TestMethod]
        public void EvaluateNewFunctionWithIdentifierTypeName()
        {
            FSubrNew fnew = new FSubrNew();

            object result = fnew.Apply(new List(new Identifier("System.IO.DirectoryInfo"), new List(".")), new ValueEnvironment());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.DirectoryInfo));

            System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)result;

            Assert.AreEqual((new System.IO.DirectoryInfo(".")).FullName, di.FullName);
        }

        [TestMethod]
        public void EvaluateInvokeTypeFunctionWithObjectMethodAndArguments()
        {
            FSubrTypeInvoke invoke = new FSubrTypeInvoke();

            object result = invoke.Apply(new List(typeof(System.IO.File), new List("Exists", new List("nofile.txt"))), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse((bool)result);
        }
    }
}
