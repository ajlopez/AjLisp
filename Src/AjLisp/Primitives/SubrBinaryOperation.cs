namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public abstract class SubrBinaryOperation : Subr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            return this.Execute(arguments.First, arguments.Next.First, environment);
        }

        public abstract object Execute(object argument1, object argument2, ValueEnvironment environment);
    }
}
