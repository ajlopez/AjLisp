namespace AjLisp.Language
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Identifier : IExpression, IComparable
    {
        private string value;

        public Identifier(string value)
        {
            this.value = value;
        }

        public string Name { get { return this.value; } }

        public override string ToString()
        {
            return this.value;
        }

        public int CompareTo(object obj)
        {
            return this.value.CompareTo(obj.ToString());
        }

        public object Evaluate(ValueEnvironment env)
        {
            return env.GetValue(this.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Identifier)
                return this.value.Equals(((Identifier)obj).value);

            return false;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
    }
}
