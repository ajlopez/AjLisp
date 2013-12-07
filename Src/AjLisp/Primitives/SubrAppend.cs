namespace AjLisp.Primitives
{
    using System;
    using System.Text;

    using AjLisp.Language;

    public class SubrAppend : SubrBinaryOperation
    {
        public override object Execute(object argument1, object argument2, ValueEnvironment environment)
        {
            if (Predicates.IsNil(argument1))
                return argument2;

            if (Predicates.IsNil(argument2))
                return argument1;

            List list = (List)argument1;

            return Append((List) argument1, argument2);
        }

        private static object Append(List list1, object list2)
        {
            if (Predicates.IsNil(list1))
                return list2;

            if (Predicates.IsNil(list2))
                return list1;

            return new List(list1.First, Append(list1.Next, list2));
        }
    }
}
