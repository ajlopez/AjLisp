namespace AjLisp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NumbersTests
    {
        [TestMethod]
        public void CalculateSimpleGCD()
        {
            Assert.AreEqual(1, Numbers.GreatestCommonDivisor(3, 1));
            Assert.AreEqual(2, Numbers.GreatestCommonDivisor(4, 2));
            Assert.AreEqual(3, Numbers.GreatestCommonDivisor(9, 6));
        }

        [TestMethod]
        public void CalculateAbs() 
        {
            Assert.AreEqual(0, Numbers.Abs(0));
            Assert.AreEqual(1, Numbers.Abs(1));
            Assert.AreEqual(2, Numbers.Abs(-2));
            Assert.AreEqual(1.23, Numbers.Abs(1.23));
            Assert.AreEqual(2.34, Numbers.Abs(-2.34));
        }

        [TestMethod]
        public void IsFixnum()
        {
            Assert.IsTrue(Numbers.IsFixnum(1));
            Assert.IsTrue(Numbers.IsFixnum(2L));
            Assert.IsTrue(Numbers.IsFixnum((short)3));

            Assert.IsFalse(Numbers.IsFixnum(null));
            Assert.IsFalse(Numbers.IsFixnum("foo"));
        }

        [TestMethod]
        public void Add()
        {
            Assert.AreEqual(7, Numbers.Add(5, 2));
            Assert.AreEqual(5.3 - 2.1, Numbers.Add(5.3, -2.1));
            Assert.AreEqual(RationalNumber.Create(1, 2), Numbers.Add(RationalNumber.Create(3, 2), -1));
            Assert.AreEqual(RationalNumber.Create(-1, 6), Numbers.Add(RationalNumber.Create(4, 3), RationalNumber.Create(-3, 2)));
            Assert.AreEqual(0.5, Numbers.Add(RationalNumber.Create(3, 2), -1.0));
            Assert.AreEqual(-0.5, Numbers.Add(1.0, RationalNumber.Create(-3, 2)));
        }

        [TestMethod]
        public void Subtract()
        {
            Assert.AreEqual(3, Numbers.Subtract(5, 2));
            Assert.AreEqual(5.3 - 2.1, Numbers.Subtract(5.3, 2.1));
            Assert.AreEqual(RationalNumber.Create(1, 2), Numbers.Subtract(RationalNumber.Create(3, 2), 1));
            Assert.AreEqual(RationalNumber.Create(-1, 6), Numbers.Subtract(RationalNumber.Create(4, 3), RationalNumber.Create(3, 2)));
            Assert.AreEqual(0.5, Numbers.Subtract(RationalNumber.Create(3, 2), 1.0));
            Assert.AreEqual(-0.5, Numbers.Subtract(1.0, RationalNumber.Create(3, 2)));
        }

        [TestMethod]
        public void Multiply()
        {
            Assert.AreEqual(2, Numbers.Multiply(1, 2));
            Assert.AreEqual(2.2, Numbers.Multiply(1.1, 2));
            Assert.AreEqual(3, Numbers.Multiply(RationalNumber.Create(3, 2), 2));
            Assert.AreEqual(3, Numbers.Multiply(2, RationalNumber.Create(3, 2)));
            Assert.AreEqual(3.0, Numbers.Multiply(RationalNumber.Create(3, 2), 2.0));
            Assert.AreEqual(3.0, Numbers.Multiply(2.0, RationalNumber.Create(3, 2)));
            Assert.AreEqual(RationalNumber.Create(15, 14), Numbers.Multiply(RationalNumber.Create(3, 2), RationalNumber.Create(5, 7)));
        }

        [TestMethod]
        public void Divide()
        {
            Assert.AreEqual(2, Numbers.Divide(4, 2));
            Assert.AreEqual(4 / 3.0, Numbers.Divide(4, 3.0));
            Assert.AreEqual(RationalNumber.Create(3, 2), Numbers.Divide(3, 2));
            Assert.AreEqual(RationalNumber.Create(3, 4), Numbers.Divide(RationalNumber.Create(3, 2), 2));
            Assert.AreEqual(RationalNumber.Create(4, 3), Numbers.Divide(2, RationalNumber.Create(3, 2)));
            Assert.AreEqual(2.0 / (3.0 / 2), Numbers.Divide(2.0, RationalNumber.Create(3, 2)));
            Assert.AreEqual((3.0 / 2) / 2.0, Numbers.Divide(RationalNumber.Create(3, 2), 2.0));
            Assert.AreEqual(RationalNumber.Create(21, 10), Numbers.Divide(RationalNumber.Create(3, 2), RationalNumber.Create(5, 7)));
        }
    }
}
