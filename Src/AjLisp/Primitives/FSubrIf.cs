namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrIf : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            if (Predicates.IsFalse(Machine.Evaluate(arguments.First, environment)))
            {
                List elseexpr = arguments.Next.Next;
                FSubrProgN progn = new FSubrProgN();
                return progn.Execute(elseexpr, environment);
            }

            return Machine.Evaluate(arguments.Next.First, environment);
        }
    }
}
