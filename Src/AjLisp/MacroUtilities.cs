namespace AjLisp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLisp.Language;

    public class MacroUtilities
    {
        public static object Expand(object obj, ValueEnvironment environment)
        {
            if (obj == null)
                return null;

            if (obj is string)
                return obj;

            if (!(obj is List))
                return obj;

            List list = (List)obj;

            if (IsMacro(list))
                return Machine.Evaluate(list.Next.First, environment);

            List<object> newlist = new List<object>();

            foreach (object element in list.ToObjectList())
            {
                if (element is List)
                {
                    List elementList = (List)element;

                    if (IsListMacro(elementList))
                    {
                        newlist.AddRange(((List)Machine.Evaluate(elementList.Next.First, environment)).ToObjectList());
                        continue;
                    }
                }

                newlist.Add(Expand(element, environment));
            }

            return List.Create(newlist);
        }

        private static bool IsMacro(List list)
        {
            if (list == null)
                return false;

            if (list.Next == null || list.Next.Next != null)
                return false;

            if (list.First is Identifier)
            {
                Identifier identifier = (Identifier)list.First;

                if (identifier.Name.Equals("unquote"))
                    return true;
            }

            return false;
        }

        private static bool IsListMacro(List list)
        {
            if (list == null)
                return false;

            if (list.Next == null || list.Next.Next != null)
                return false;

            if (list.First is Identifier)
            {
                Identifier identifier = (Identifier)list.First;

                if (identifier.Name.Equals("unquote-splice"))
                    return true;
            }

            return false;
        }
    }
}
