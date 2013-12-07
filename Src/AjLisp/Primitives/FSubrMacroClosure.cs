namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrMacroClosure : FSubrMExpr
    {
        private ValueEnvironment environment;

        public FSubrMacroClosure(object arguments, ValueEnvironment environment, List body)
            : base(arguments, body)
        {
            this.environment = environment;
        }

        public override object Execute(List arguments, ValueEnvironment environment)
        {
            return base.Execute(arguments, this.environment);
        }
    }
}
