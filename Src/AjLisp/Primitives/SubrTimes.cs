namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrTimes : Subr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            object result = 1;

            while (!Predicates.IsNil(arguments))
            {
                object argument = arguments.First;
                arguments = arguments.Next;
                result = Numbers.Multiply(result, argument);
            }

            return result;
        }
    }
}

