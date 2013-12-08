namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrDefine : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            Identifier atom;
            object arglist;
            List body;

            atom = (Identifier)arguments.First;

            arglist = arguments.Next.First;
            body = arguments.Next.Next;

            if (body == null)
            {
                object result = Machine.Evaluate(arglist, environment);
                environment.SetGlobalValue(atom.Name, result);
                return result;
            }

            SubrClosure closure = new SubrClosure(arglist, environment, body);

            environment.SetGlobalValue(atom.Name, closure);

            return closure;
        }
    }
}
