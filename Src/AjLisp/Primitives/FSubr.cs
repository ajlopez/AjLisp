namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public abstract class FSubr : IFunction
    {
        public abstract object Execute(List arguments, ValueEnvironment environment);

        public object Apply(List arguments, ValueEnvironment environment)
        {
            return this.Execute(arguments, environment);
        }
    }
}
