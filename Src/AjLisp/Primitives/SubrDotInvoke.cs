namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;
    using AjLisp.Utilities;

    public class SubrDotInvoke : Subr
    {
        private string name;

        public SubrDotInvoke(string name)
        {
            this.name = name;
        }

        public override object Execute(List arguments, ValueEnvironment environment)
        {
            var target = arguments.First;
            IList<object> args = null;
            
            if (arguments.Next != null)
                args = arguments.Next.ToObjectArray();

            return ObjectUtilities.GetNativeValue(target, this.name, args);
        }
    }
}
