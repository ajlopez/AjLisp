namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrCond : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment env)
        {
            while (!Predicates.IsNil(arguments))
            {
                List pair = (List)arguments.First;
                object condition;

                condition = pair.First;

                if (!Predicates.IsFalse(Machine.Evaluate(condition, env)))
                {
                    FSubrProgN progn = new FSubrProgN();

                    return progn.Execute(pair.Next, env);
                }

                arguments = arguments.Next;
            }

            return null;
        }
    }
}
