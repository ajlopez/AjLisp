namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrFLambda : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            List arglist = (List)arguments.First;

            if (!Predicates.IsIdentifier(arglist.First) || !Predicates.IsNil(arglist.Rest))
            {
                throw new ArgumentException("flambda needs a unique parameter");
            }

            return new FSubrClosure(((Identifier)arglist.First), environment, arguments.Next);
        }
    }
}
