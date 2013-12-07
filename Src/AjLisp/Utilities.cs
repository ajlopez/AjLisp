namespace AjLisp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Utilities
    {
        public static int Hash(object obj)
        {
            if (obj == null)
                return 0;

            return obj.GetHashCode();
        }

        public static int CombineHash(IEnumerable elements)
        {
            int value = 0;

            if (elements != null)
                foreach (object element in elements)
                    value = CombineHash(value, Hash(element));

            return value;
        }

        public static int CombineHash(int seed, int hash)
        {
            // a la boost
            unchecked
            {
                seed ^= hash + (int)0x9e3779b9 + (seed << 6) + (seed >> 2);
            }

            return seed;
        }
    }
}
