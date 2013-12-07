namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrAtom : SubrUnaryOperation
    {
        public override object Execute(object argument, ValueEnvironment environment)
        {
            return Predicates.IsAtom(argument);
        }
    }
}
