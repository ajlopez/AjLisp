namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrProgN : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            object result = null;

            while (!Predicates.IsNil(arguments))
            {
                result = Machine.Evaluate(arguments.First, environment);
                arguments = arguments.Next;
            }

            return result;
        }
    }
}
