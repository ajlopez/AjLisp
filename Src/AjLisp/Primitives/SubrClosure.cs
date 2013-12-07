namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrClosure : SubrExpr
    {
        private ValueEnvironment environment;

        public SubrClosure(object args, ValueEnvironment env, List body)
            : base(args, body)
        {
            this.environment = env;
        }

        public override object Execute(List args, ValueEnvironment env)
        {
            ValueEnvironment nenv;
            nenv = this.MakeEnvironment(args, this.environment);
            return this.ExecuteBody(nenv);
        }
    }
}
