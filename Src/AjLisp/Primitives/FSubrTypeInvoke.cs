namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;
    using AjLisp.Utilities;

    public class FSubrTypeInvoke : FSubr
    {
        public override object Execute(List arguments, ValueEnvironment environment)
        {
            Type type = TypeUtilities.GetType(environment, arguments.First);

            string methodName = (string)Machine.Evaluate(arguments.Next.First, environment);

            object[] parameters = null;

            List arglist = arguments.Next.Next;

            if (arglist != null)
            {
                parameters = arglist.ToObjectArray();

                for (int k = 0; k < parameters.Length; k++)
                    parameters[k] = Machine.Evaluate(parameters[k], environment);
            }

            return type.InvokeMember(methodName, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static, null, null, parameters);
        }
    }
}
