namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrGreaterEqual : SubrBinaryOperation
    {
        public override object Execute(object argument1, object argument2, ValueEnvironment environment)
        {
            return ((IComparable)argument1).CompareTo(argument2) >= 0;
        }
    }
}
