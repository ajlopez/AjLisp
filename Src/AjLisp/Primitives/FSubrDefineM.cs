namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrDefineM : FSubr
    {
        public override object Execute(List args, ValueEnvironment env)
        {
            Identifier atom;
            object arglist;
            List body;

            atom = (Identifier) args.First;
            arglist = args.Next.First;
            body = args.Next.Next;

            FSubrMacroClosure closure = new FSubrMacroClosure((Identifier) arglist, env, body);

            env.SetGlobalValue(atom.Name, closure);

            return closure;
        }
    }
}
