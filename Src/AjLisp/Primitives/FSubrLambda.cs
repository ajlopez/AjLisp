namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrLambda : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            return new SubrClosure(arguments.First, environment, arguments.Next);
        }
    }
}
