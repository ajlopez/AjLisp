namespace AjLisp.Tests.Primitives
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjLisp.Primitives;
    using AjLisp.Language;

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
