namespace AjLisp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLisp.Language;

    using Microsoft.VisualBasic.CompilerServices;

    public static class Numbers
    {
        public static object Add(object obj1, object obj2)
        {
            if (obj1 is RationalNumber)
            {
                RationalNumber number1 = (RationalNumber)obj1;

                if (IsFixnum(obj2))
                    return number1.Add(Convert.ToInt64(obj2));

                if (obj2 is RationalNumber)
                    return number1.Add((RationalNumber)obj2);

                obj1 = number1.Value;
            }

            if (obj2 is RationalNumber)
            {
                RationalNumber number2 = (RationalNumber)obj2;

                if (IsFixnum(obj1))
                    return number2.Add(Convert.ToInt64(obj1));

                obj2 = number2.Value;
            }

            return Operators.AddObject(obj1, obj2);
        }

        public static object Subtract(object obj1, object obj2)
        {
            if (obj1 is RationalNumber)
            {
                RationalNumber number1 = (RationalNumber)obj1;

                if (IsFixnum(obj2))
                    return number1.Subtract(Convert.ToInt64(obj2));

                if (obj2 is RationalNumber)
                    return number1.Subtract((RationalNumber)obj2);

                obj1 = number1.Value;
            }

            if (obj2 is RationalNumber)
            {
                RationalNumber number2 = (RationalNumber)obj2;

                if (IsFixnum(obj1))
                    return number2.SubtractFrom(Convert.ToInt64(obj1));

                obj2 = number2.Value;
            }

            return Operators.SubtractObject(obj1, obj2);
        }

        public static object Multiply(object obj1, object obj2)
        {
            if (obj1 is RationalNumber)
            {
                RationalNumber number1 = (RationalNumber)obj1;

                if (IsFixnum(obj2))
                    return number1.MultiplyBy(Convert.ToInt64(obj2));

                if (obj2 is RationalNumber)
                    return number1.MultiplyBy((RationalNumber)obj2);

                obj1 = number1.Value;
            }

            if (obj2 is RationalNumber)
            {
                RationalNumber number2 = (RationalNumber)obj2;

                if (IsFixnum(obj1))
                    return number2.MultiplyBy(Convert.ToInt64(obj1));

                obj2 = number2.Value;
            }

            return Operators.MultiplyObject(obj1, obj2);
        }

        public static object Divide(object obj1, object obj2)
        {
            if (IsFixnum(obj1) && IsFixnum(obj2))
                return RationalNumber.Create(Convert.ToInt64(obj1), Convert.ToInt64(obj2));

            if (obj1 is RationalNumber)
            {
                RationalNumber number1 = (RationalNumber)obj1;

                if (IsFixnum(obj2))
                    return number1.DivideBy(Convert.ToInt64(obj2));

                if (obj2 is RationalNumber)
                    return number1.DivideBy((RationalNumber)obj2);

                obj1 = number1.Value;
            }

            if (obj2 is RationalNumber)
            {
                RationalNumber number2 = (RationalNumber)obj2;

                if (IsFixnum(obj1))
                    return number2.DivideTo(Convert.ToInt64(obj1));

                obj2 = number2.Value;
            }

            return Operators.DivideObject(obj1, obj2);
        }

        public static object Remainder(object obj1, object obj2)
        {
            if (!IsFixnum(obj1) || !IsFixnum(obj2))
                throw new InvalidOperationException("Remainder requires integer values");

            return Operators.ModObject(obj1, obj2);
        }

        public static long GreatestCommonDivisor(long n, long m)
        {
            long a = Math.Min(n, m);
            long b = Math.Max(n, m);

            long rest = b % a;

            while (rest != 0)
            {
                b = a;
                a = rest;

                rest = b % a;
            }

            return Math.Abs(a);
        }

        public static object Abs(object obj)
        {
            if ((bool)Operators.CompareObjectLess(obj, 0, false))
                return Operators.NegateObject(obj);

            return obj;
        }

        public static bool IsFixnum(object obj)
        {
            if (obj == null)
                return false;

            if (obj is short || obj is int || obj is long)
                return true;

            return false;
        }
    }
}
