namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public abstract class SubrUnaryOperation : Subr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            return this.Execute(arguments.First, environment);
        }

        public abstract object Execute(object argument, ValueEnvironment environment);
    }
}
