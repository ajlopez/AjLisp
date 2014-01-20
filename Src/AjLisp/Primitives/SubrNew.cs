namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;
    using AjLisp.Utilities;

    public class SubrNew : Subr
    {
        private string name;
        private Type type;

        public SubrNew(string name)
        {
            this.name = name;
            this.type = TypeUtilities.AsType(name);
        }

        public override object Execute(List arguments, ValueEnvironment environment)
        {
            if (arguments == null)
                return Activator.CreateInstance(this.type);

            return Activator.CreateInstance(this.type, arguments.ToObjectArray());
        }
    }
}
