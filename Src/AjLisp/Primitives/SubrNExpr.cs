namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrNExpr : Subr
    {
        private Identifier argumentName;
        private List body;

        public SubrNExpr(Identifier arg, List body)
        {
            this.argumentName = arg;
            this.body = body;
        }

        public override object Execute(List arguments, ValueEnvironment env)
        {
            ValueEnvironment nenv;
            nenv = this.MakeEnvironment(arguments, env);
            return this.ExecuteBody(nenv);
        }

        internal ValueEnvironment MakeEnvironment(List args, ValueEnvironment parent)
        {
            ValueEnvironment nenv = new ValueEnvironment(parent);
            nenv.SetValue(this.argumentName.Name, args);
            return nenv;
        }

        internal object ExecuteBody(ValueEnvironment env)
        {
            FSubrProgN progn = new FSubrProgN();
            return progn.Execute(this.body, env);
        }
    }
}
