namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrBackquote : FSubrUnaryOperation
    {
        public override object Execute(object argument, ValueEnvironment environment)
        {
            return MacroUtilities.Expand(argument, environment);
        }
    }
}
