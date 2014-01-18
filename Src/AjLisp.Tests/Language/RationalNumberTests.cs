namespace AjLisp.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RationalNumberTests
    {
        [TestMethod]
        public void CreateRationalNumber()
        {
            object result = RationalNumber.Create(3, 2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RationalNumber));

            RationalNumber number = (RationalNumber)result;

            Assert.AreEqual(3, number.Numerator);
            Assert.AreEqual(2, number.Denominator);
        }

        [TestMethod]
        public void CreateLongRationalNumber()
        {
            object result = RationalNumber.Create(Int64.MaxValue, Int64.MaxValue - 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RationalNumber));

            RationalNumber number = (RationalNumber)result;

            Assert.AreEqual(Int64.MaxValue, number.Numerator);
            Assert.AreEqual(Int64.MaxValue - 1, number.Denominator);
        }

        public void CreateLong()
        {
            object result = RationalNumber.Create(Int64.MaxValue, 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(long));
            Assert.AreEqual(Int64.MaxValue, result);
        }

        [TestMethod]
        public void CreateRationalNumberAsInteger()
        {
            object result = RationalNumber.Create(4, 2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CreateUsingNegativeIntegers()
        {
            RationalNumber number = (RationalNumber)RationalNumber.Create(-1, 2);

            Assert.AreEqual(-1, number.Numerator);
            Assert.AreEqual(2, number.Denominator);

            number = (RationalNumber)RationalNumber.Create(1, -2);

            Assert.AreEqual(-1, number.Numerator);
            Assert.AreEqual(2, number.Denominator);

            number = (RationalNumber)RationalNumber.Create(-1, -2);

            Assert.AreEqual(1, number.Numerator);
            Assert.AreEqual(2, number.Denominator);
        }

        [TestMethod]
        public void EqualRationalNumbers()
        {
            Assert.IsTrue(RationalNumber.Create(4, 2).Equals(2));
            Assert.IsTrue(RationalNumber.Create(3, 2).Equals(RationalNumber.Create(3, 2)));

            Assert.IsFalse(RationalNumber.Create(4, 2).Equals(3));
            Assert.IsFalse(RationalNumber.Create(3, 2).Equals(RationalNumber.Create(5, 2)));
        }

        [TestMethod]
        public void GetHashCodes()
        {
            int hash1 = RationalNumber.Create(3, 2).GetHashCode();
            int hash2 = RationalNumber.Create(6, 4).GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void EvaluateToString()
        {
            Assert.AreEqual("3/2", RationalNumber.Create(3, 2).ToString());
        }

        [TestMethod]
        public void GetValue()
        {
            Assert.AreEqual(1.5, ((RationalNumber)RationalNumber.Create(3, 2)).Value);
            Assert.AreEqual(5.0 / 3.0, ((RationalNumber)RationalNumber.Create(5, 3)).Value);
        }

        [TestMethod]
        public void AddLong()
        {
            RationalNumber number = (RationalNumber)RationalNumber.Create(3, 2);

            Assert.AreEqual(RationalNumber.Create(5, 2), number.Add(1));
            Assert.AreEqual(RationalNumber.Create(-1, 2), number.Add(-2));
        }

        [TestMethod]
        public void AddRationalNumbers()
        {
            RationalNumber number = (RationalNumber)RationalNumber.Create(3, 2);

            Assert.AreEqual(2, number.Add((RationalNumber)RationalNumber.Create(1, 2)));
            Assert.AreEqual(-1, number.Add((RationalNumber)RationalNumber.Create(-5, 2)));
        }

        [TestMethod]
        public void SubtractLong()
        {
            RationalNumber number = (RationalNumber)RationalNumber.Create(3, 2);

            Assert.AreEqual(RationalNumber.Create(1, 2), number.Subtract(1));
            Assert.AreEqual(RationalNumber.Create(-1, 2), number.Subtract(2));
        }

        [TestMethod]
        public void MultiplyRationalNumberByLong()
        {
            RationalNumber number = (RationalNumber)RationalNumber.Create(3, 2);
            Assert.AreEqual(3, number.MultiplyBy(2));
            Assert.AreEqual(RationalNumber.Create(15, 2), number.MultiplyBy(5));
        }

        [TestMethod]
        public void MultiplyRationalNumbers()
        {
            RationalNumber number1 = (RationalNumber)RationalNumber.Create(3, 2);
            RationalNumber number2 = (RationalNumber)RationalNumber.Create(7, 5);
            Assert.AreEqual(RationalNumber.Create(21, 10), number1.MultiplyBy(number2));
        }

        [TestMethod]
        public void DivideRationalNumberByLong()
        {
            RationalNumber number = (RationalNumber)RationalNumber.Create(3, 2);
            Assert.AreEqual(RationalNumber.Create(3, 4), number.DivideBy(2));
            Assert.AreEqual(RationalNumber.Create(4, 3), number.DivideTo(2));
        }

        [TestMethod]
        public void DivideRationalNumbers()
        {
            RationalNumber number1 = (RationalNumber)RationalNumber.Create(3, 2);
            RationalNumber number2 = (RationalNumber)RationalNumber.Create(5, 7);

            Assert.AreEqual(RationalNumber.Create(21, 10), number1.DivideBy(number2));
            Assert.AreEqual(RationalNumber.Create(10, 21), number2.DivideBy(number1));
            Assert.AreEqual(1, number1.DivideBy(number1));
        }

        [TestMethod]
        public void CreateRationalNumberUsingGCD()
        {
            object result = RationalNumber.Create(9, 6);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RationalNumber));

            RationalNumber rational = (RationalNumber)result;

            Assert.AreEqual(3, rational.Numerator);
            Assert.AreEqual(2, rational.Denominator);
        }
    }
}
