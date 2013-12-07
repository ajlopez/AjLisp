namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public abstract class Subr : IFunction
    {
        public abstract object Execute(List arguments, ValueEnvironment environment);

        public object Apply(List arguments, ValueEnvironment environment)
        {
            return this.Execute(EvaluateList(arguments, environment), environment);
        }

        private static List EvaluateList(List arguments, ValueEnvironment environment)
        {
            if (arguments == null)
                return null;

            return new List(Machine.Evaluate(arguments.First, environment), EvaluateList(arguments.Next, environment));
        }
    }
}
