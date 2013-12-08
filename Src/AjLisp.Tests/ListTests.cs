namespace AjLisp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void CreateListWithOneElement()
        {
            List list = new List(1);

            Assert.AreEqual(1, list.First);
            Assert.IsNull(list.Rest);
            Assert.IsNull(list.Next);
        }

        [TestMethod]
        public void CreateListWithTwoObjectElements()
        {
            List list = new List(1, 2);

            Assert.AreEqual(1, list.First);
            Assert.AreEqual(2, list.Rest);
        }

        [TestMethod]
        public void CreateListWithThreeNumbers()
        {
            List list = new List(1, new List(2, new List(3)));

            Assert.AreEqual(1, list.First);
            Assert.AreEqual(2, list.Next.First);
            Assert.AreEqual(3, list.Next.Next.First);
            Assert.IsNull(list.Next.Next.Rest);
        }

        [TestMethod]
        public void ToObjectArray()
        {
            List list = new List(1, new List(2, new List(3)));
            object[] array = list.ToObjectArray();

            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
        }

        [TestMethod]
        public void ToObjectList()
        {
            List list = new List(1, new List(2, new List(3)));
            IList<object> olist = list.ToObjectArray();

            Assert.IsNotNull(olist);
            Assert.AreEqual(3, olist.Count);
            Assert.AreEqual(1, olist[0]);
            Assert.AreEqual(2, olist[1]);
            Assert.AreEqual(3, olist[2]);
        }
    }
}
