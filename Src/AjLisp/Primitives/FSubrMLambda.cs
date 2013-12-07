namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrMLambda : FSubr
    {
        public override object Execute(List args, ValueEnvironment env)
        {
            return new FSubrMacroClosure(args.First, env, args.Next);
        }
    }
}
