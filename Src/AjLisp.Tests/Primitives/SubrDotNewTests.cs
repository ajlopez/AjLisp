namespace AjLisp.Tests.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Language;
    using AjLisp.Primitives;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubrDotNewTests
    {
        [TestMethod]
        public void CreateDataset()
        {
            SubrDotNew snew = new SubrDotNew("System.Data.DataSet");

            var result = snew.Execute(null, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.Data.DataSet));
        }

        [TestMethod]
        public void CreateFileInfo()
        {
            SubrDotNew snew = new SubrDotNew("System.IO.FileInfo");
            var list = List.Create(new string[] { "foo.txt" });

            var result = snew.Execute(list, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.FileInfo));
        }
    }
}
