namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrDifference : SubrBinaryOperation
    {
        public override object Execute(object argument1, object argument2, ValueEnvironment environment)
        {
            return Numbers.Subtract(argument1, argument2);
        }
    }
}
