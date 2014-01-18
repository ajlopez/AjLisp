namespace AjLisp.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Utilities;

    public class RationalNumber
    {
        private long numerator;
        private long denominator;

        private RationalNumber(long numerator, long denominator)
        {
            if (denominator < 0)
            {
                denominator = -denominator;
                numerator = -numerator;
            }
                
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public long Numerator { get { return this.numerator; } }

        public long Denominator { get { return this.denominator; } }

        public double Value { get { return (double)this.numerator / (double)this.denominator; } }

        public static object Create(long numerator, long denominator)
        {
            if (denominator == 1)
                return numerator;

            if ((numerator % denominator) == 0)
            {
                long result = (long)(numerator / denominator);

                if (result <= Int32.MaxValue && result >= Int32.MinValue)
                    return (int)result;

                return result;
            }

            long gcd = Numbers.GreatestCommonDivisor(numerator, denominator);

            return new RationalNumber(numerator / gcd, denominator / gcd);
        }

        public override string ToString()
        {
            return this.numerator + "/" + this.denominator;
        }

        public object Add(long number)
        {
            return RationalNumber.Create(this.numerator + (number * this.denominator), this.denominator);
        }

        public object Add(RationalNumber number)
        {
            return RationalNumber.Create((this.numerator * number.denominator) + (number.numerator * this.denominator), this.denominator * number.denominator);
        }

        public object Subtract(long number)
        {
            return RationalNumber.Create(this.numerator - (number * this.denominator), this.denominator);
        }

        public object SubtractFrom(long number)
        {
            return RationalNumber.Create((number * this.denominator) - this.numerator, this.denominator);
        }

        public object Subtract(RationalNumber number)
        {
            return RationalNumber.Create((this.numerator * number.denominator) - (number.numerator * this.denominator), this.denominator * number.denominator);
        }

        public object MultiplyBy(long factor)
        {
            return RationalNumber.Create(this.numerator * factor, this.denominator);
        }

        public object MultiplyBy(RationalNumber factor)
        {
            return RationalNumber.Create(this.numerator * factor.numerator, this.denominator * factor.denominator);
        }

        public object DivideBy(long divisor)
        {
            return RationalNumber.Create(this.numerator, this.denominator * divisor);
        }

        public object DivideTo(long divident)
        {
            return RationalNumber.Create(this.denominator * divident, this.numerator);
        }

        public object DivideBy(RationalNumber divisor)
        {
            return RationalNumber.Create(this.numerator * divisor.denominator, this.denominator * divisor.numerator);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is RationalNumber))
                return false;

            RationalNumber r = (RationalNumber)obj;

            if (r.numerator == this.numerator && r.denominator == this.denominator)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return HashUtilities.CombineHash(this.numerator.GetHashCode(), this.denominator.GetHashCode());
        }
    }
}
