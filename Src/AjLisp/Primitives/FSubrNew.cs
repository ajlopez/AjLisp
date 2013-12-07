namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class FSubrNew : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            Type type = TypeUtilities.GetType(environment, arguments.First);

            object[] parameters = null;

            List arglist = arguments.Next;

            if (arglist != null)
            {
                parameters = arglist.ToObjectArray();

                for (int k = 0; k < parameters.Length; k++)
                    parameters[k] = Machine.Evaluate(parameters[k], environment);
            }

            return Activator.CreateInstance(type, parameters);
        }
    }
}
