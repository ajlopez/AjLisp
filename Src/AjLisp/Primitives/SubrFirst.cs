namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrFirst : SubrUnaryOperation
    {
        public override object Execute(object argument, ValueEnvironment environment)
        {
            if (Predicates.IsNil(argument))
                return null;

            if (!Predicates.IsList(argument))
                throw new InvalidOperationException("First requires a list");

            return ((List)argument).First;
        }
    }
}
