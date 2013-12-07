namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrQuote : FSubrUnaryOperation
    {
        public override object Execute(object argument, ValueEnvironment environment)
        {
            return argument;
        }
    }
}
