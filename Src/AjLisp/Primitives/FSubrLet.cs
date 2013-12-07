namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrLet : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            ValueEnvironment newenv;
            newenv = MakeEnvironment((List) arguments.First, environment);
            FSubrProgN progn = new FSubrProgN();
            return progn.Execute(arguments.Next, newenv);
        }

        private static ValueEnvironment MakeEnvironment(List arguments, ValueEnvironment parent)
        {
            ValueEnvironment newenv = new ValueEnvironment(parent);

            while (!Predicates.IsNil(arguments))
            {
                List argument = (List)arguments.First;
                Identifier id = (Identifier) argument.First;
                object expr = argument.Next.First;
                newenv.SetValue(id.Name, Machine.Evaluate(expr, parent));
                arguments = arguments.Next;
            }

            return newenv;
        }
    }
}
