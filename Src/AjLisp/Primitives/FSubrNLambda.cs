namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrNLambda : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            List arglist = (List) arguments.First;

            if (!Predicates.IsIdentifier(arglist.First) || !Predicates.IsNil(arglist.Rest))
            {
                throw new ArgumentException("nlambda needs a unique parameter");
            }

            return new SubrNClosure((Identifier)arglist.First, environment, arguments.Next);
        }
    }
}
