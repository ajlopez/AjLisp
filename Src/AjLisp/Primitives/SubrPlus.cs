namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrPlus : Subr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            object result = 0;

            while (!Predicates.IsNil(arguments))
            {
                object argument = arguments.First;
                arguments = arguments.Next;
                result = Numbers.Add(result, argument);
            }

            return result;
        }
    }
}
