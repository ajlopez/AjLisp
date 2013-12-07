namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrFExpr : FSubr
    {
        private Identifier argumentName;
        private List body;

        public FSubrFExpr(Identifier arg, List body)
        {
            this.argumentName = arg;
            this.body = body;
        }

        public override object Execute(List arguments, ValueEnvironment environment)
        {
            ValueEnvironment nenv;
            nenv = this.MakeEnvironment(arguments, environment);
            return this.ExecuteBody(nenv);
        }

        internal ValueEnvironment MakeEnvironment(List arguments, ValueEnvironment parent)
        {
            ValueEnvironment nenv = new ValueEnvironment(parent);
            nenv.SetValue(this.argumentName.Name, arguments);
            return nenv;
        }

        internal object ExecuteBody(ValueEnvironment environment)
        {
            FSubrProgN progn = new FSubrProgN();
            return progn.Execute(this.body, environment);
        }
    }
}
