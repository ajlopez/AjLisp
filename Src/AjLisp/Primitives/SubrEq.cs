namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrEq : SubrBinaryOperation
    {
        public override object Execute(object argument1, object argument2, ValueEnvironment environment)
        {
            return Operations.Equals(argument1, argument2);
        }
    }
}
