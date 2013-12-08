namespace AjLisp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AjLisp.Language;

    public static class Predicates
    {
        public static bool IsNil(object obj)
        {
            return obj == null;
        }

        public static bool IsFalse(object obj)
        {
            return IsNil(obj) || (obj is bool && (bool)obj == false);
        }

        public static bool IsTrue(object obj)
        {
            return !IsFalse(obj);
        }

        public static bool IsBooleanFalse(object obj)
        {
            return obj is bool && ((bool)obj) == false;
        }

        public static bool IsBooleanTrue(object obj)
        {
            return obj is bool && ((bool)obj) == true;
        }

        public static bool IsIdentifier(object obj)
        {
            return obj is Identifier;
        }

        public static bool IsString(object obj)
        {
            return obj is string;
        }

        public static bool IsAtom(object obj)
        {
            return obj is Identifier || !(obj is IExpression);
        }

        public static bool IsList(object obj)
        {
            return obj is List;
        }

        public static bool IsFunction(object obj)
        {
            return obj is IFunction;
        }

        public static bool IsNumber(object obj)
        {
            return obj is int || obj is short || obj is long
                || obj is uint || obj is ushort || obj is ulong
                || obj is float || obj is double || obj is decimal;
        }
    }
}
