namespace AjLisp.Language
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public class List : IExpression
    {
        private object first;
        private object rest;

        public List()
        {
        }

        public List(object first)
        {
            this.first = first;
        }

        public List(object first, object rest)
        {
            this.first = first;
            this.rest = rest;
        }

        public object First { get { return this.first; } }

        public object Rest { get { return this.rest; } }

        public List Next { get { return (List)this.rest; } }

        public static List Create(IList elements)
        {
            if (elements == null || elements.Count == 0)
                return null;

            List list = null;

            for (int k = elements.Count; k > 0; k--)
                list = new List(elements[k - 1], list);

            return list;
        }

        public object Evaluate(ValueEnvironment environment)
        {
            IFunction function = (IFunction)Machine.Evaluate(this.first, environment);

            if (function == null)
                throw new InvalidOperationException(string.Format("Unknown form '{0}'", this.first.ToString()));

            return function.Apply(this.Next, environment);
        }

        // TODO use StringBuilder
        public override string ToString()
        {
            string text;

            text = "(" + Conversions.ToPrintString(this.first);

            object restObject = this.rest;

            while (Predicates.IsList(restObject))
            {
                List restList = (List)restObject;
                text = text + " " + Conversions.ToPrintString(restList.First);
                restObject = restList.Rest;
            }

            if (!Predicates.IsNil(restObject))
                text = text + " . " + Conversions.ToPrintString(restObject);

            text = text + ")";
            
            return text;
        }

        public IList<object> ToObjectList()
        {
            IList<object> list = new List<object>();

            for (Language.List l = this; l != null; l = l.Next)
                list.Add(l.First);

            return list;
        }

        public object[] ToObjectArray()
        {
            return ((List<object>)this.ToObjectList()).ToArray();
        }
    }
}
