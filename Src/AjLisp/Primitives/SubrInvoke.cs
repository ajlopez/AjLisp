namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

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

            Type type = obj.GetType();

            return type.InvokeMember(methodName, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance, null, obj, parameters);
        }
    }
}
