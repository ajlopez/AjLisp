namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrNClosure : SubrNExpr
    {
        private ValueEnvironment environment;

        public SubrNClosure(Identifier arg, ValueEnvironment env, List body)
            : base(arg, body)
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
