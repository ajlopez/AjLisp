namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrQuotient : SubrBinaryOperation
    {
        public override object Execute(object argument1, object argument2, ValueEnvironment environment)
        {
            return Numbers.Divide(argument1, argument2);
        }
    }
}
