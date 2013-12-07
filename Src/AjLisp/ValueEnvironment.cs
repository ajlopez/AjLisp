namespace AjLisp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class ValueEnvironment
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();
        private ValueEnvironment parent;

        public ValueEnvironment()
        {
        }

        public ValueEnvironment(ValueEnvironment parent)
        {
            this.parent = parent;
        }

        public object GetValue(string name)
        {
                if (!this.values.ContainsKey(name))
                {
                    if (!(this.parent == null))
                    {
                        return this.parent.GetValue(name);
                    }

                    return null;
                }

                return this.values[name];
        }

        public void SetValue(string name, object value)
        {
             this.values[name] = value;
        }

        public void SetGlobalValue(string name, object value)
        {
            if (this.parent == null)
            {
                this.values[name] = value;
                return;
            }

            this.parent.SetGlobalValue(name, value);
        }
    }
}
