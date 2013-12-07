namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrNull : SubrUnaryOperation
    {
        public override object Execute(object argument, ValueEnvironment environment)
        {
            return Predicates.IsNil(argument);
        }
    }
}
