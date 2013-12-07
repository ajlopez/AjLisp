namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrList : Subr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            return arguments;
        }
    }
}
