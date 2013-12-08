namespace AjLisp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Conversions
    {
        public static string ToPrintString(object obj)
        {
            if (obj == null)
                return "nil";

            if (obj is bool)
                if ((bool)obj)
                    return "t";
                else
                    return "nil";

            // TODO escape characters
            if (obj is string)
                return '"' + (string)obj + '"';

            return Convert.ToString(obj, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
