namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;
    using AjLisp.Utilities;

    public class SubrInvoke : Subr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            object obj = arguments.First;
            string methodName = (string)arguments.Next.First;

            object[] parameters = null;

            List arglist = arguments.Next.Next;

            if (arglist != null)
                parameters = arglist.ToObjectArray();

            return ObjectUtilities.GetNativeValue(obj, methodName, parameters);
        }
    }
}
