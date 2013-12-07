namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrSet : SubrBinaryOperation
    {
        public override object Execute(object argument1, object argument2, ValueEnvironment environment)
        {
            Identifier atom = (Identifier)argument1;
            environment.SetValue(atom.Name, argument2);
            return argument2;
        }
    }
}
