namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrNumberP : SubrUnaryOperation
    {
        public override object Execute(object argument, ValueEnvironment environment)
        {
            return Predicates.IsNumber(argument);
        }
    }
}
