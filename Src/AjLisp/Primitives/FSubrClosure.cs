namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrClosure : FSubrFExpr
    {
        private ValueEnvironment environment;

        public FSubrClosure(Identifier arg, ValueEnvironment env, List body)
            : base(arg, body)
        {
            this.environment = env;
        }

        public override object Execute(List arguments, ValueEnvironment env)
        {
            ValueEnvironment newenv;
            newenv = MakeEnvironment(arguments, this.environment);
            return ExecuteBody(newenv);
        }
    }
}
