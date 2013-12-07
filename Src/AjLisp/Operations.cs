namespace AjLisp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Operations
    {
        public static new bool Equals(object obj1, object obj2)
        {
            if (obj1 == null)
                return obj2 == null;

            return obj1.Equals(obj2);
        }
    }
}
