namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrDefineN : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            Identifier atom;
            List arglist;
            List body;

            atom = (Identifier) arguments.First;

            arglist = (List) arguments.Next.First;
            body = arguments.Next.Next;

            if (!Predicates.IsIdentifier(arglist.First) || !Predicates.IsNil(arglist.Rest))
            {
                throw new ArgumentException("df needs a unique parameter");
            }

            SubrNClosure closure = new SubrNClosure((Identifier)arglist.First, environment, body);
            environment.SetGlobalValue(atom.ToString(), closure);

            return closure;
        }
    }
}
