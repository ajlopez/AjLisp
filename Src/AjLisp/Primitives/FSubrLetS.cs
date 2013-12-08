namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrLetS : FSubr
    {
        public override object Execute(List args, ValueEnvironment env)
        {
            ValueEnvironment newenv;
            newenv = MakeEnvironment((List)args.First, env);
            FSubrProgN progn = new FSubrProgN();
            return progn.Execute(args.Next, newenv);
        }

        private static ValueEnvironment MakeEnvironment(List arguments, ValueEnvironment parent)
        {
            ValueEnvironment newenv = new ValueEnvironment(parent);

            while (!Predicates.IsNil(arguments))
            {
                List argument = (List)arguments.First;
                Identifier id = (Identifier)argument.First;
                object expr = argument.Next.First;
                newenv.SetValue(id.ToString(), Machine.Evaluate(expr, newenv));
                arguments = arguments.Next;
            }

            return newenv;
        }
    }
}
