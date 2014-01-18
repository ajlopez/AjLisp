namespace AjLisp.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // Based on AjSharp AjLanguage.ObjectUtilities
    // Based on PythonSharp.Utilitites
    public class ObjectUtilities
    {
        public static void SetValue(object obj, string name, object value)
        {
            Type type = obj.GetType();

            type.InvokeMember(name, System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.SetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, obj, new object[] { value });
        }

        public static object GetValue(object obj, string name)
        {
            Type type = obj.GetType();

            try
            {
                return type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | /* System.Reflection.BindingFlags.InvokeMethod | */ System.Reflection.BindingFlags.Instance, null, obj, null);
            }
            catch
            {
                return type.GetMethod(name);
            }
        }

        public static object GetValue(object obj, string name, IList<object> arguments)
        {
            return GetNativeValue(obj, name, arguments);
        }

        public static IList<string> GetNames(object obj)
        {
            return TypeUtilities.GetNames(obj.GetType());
        }

        public static object GetNativeValue(object obj, string name, IList<object> arguments)
        {
            Type type = obj.GetType();

            return type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance, null, obj, arguments == null ? null : arguments.ToArray());
        }

        public static bool IsNumber(object obj)
        {
            return obj is int ||
                obj is short ||
                obj is long ||
                obj is decimal ||
                obj is double ||
                obj is float ||
                obj is byte;
        }

        // TODO implement a method with only one index
        public static object GetIndexedValue(object obj, object[] indexes)
        {
            if (obj is System.Array)
                return GetIndexedValue((System.Array)obj, indexes);

            if (obj is IList)
                return GetIndexedValue((IList)obj, indexes);

            if (obj is IDictionary)
                return GetIndexedValue((IDictionary)obj, indexes);

            return GetValue(obj, string.Empty, indexes); 
        }

        // TODO implement a method with only one index
        public static void SetIndexedValue(object obj, object[] indexes, object value)
        {
            if (obj is System.Array)
            {
                SetIndexedValue((System.Array)obj, indexes, value);
                return;
            }

            if (obj is IList)
            {
                if (indexes.Length != 1)
                    throw new InvalidOperationException("Invalid number of subindices");

                int index = (int)indexes[0];

                IList list = (IList)obj;

                if (list.Count == index)
                    list.Add(value);
                else
                    list[index] = value;

                return;
            }

            if (obj is IDictionary)
            {
                if (indexes.Length != 1)
                    throw new InvalidOperationException("Invalid number of subindices");

                ((IDictionary)obj)[indexes[0]] = value;

                return;
            }

            // TODO as in GetIndexedValue, consider Default member
            throw new InvalidOperationException(string.Format("Not indexed value of type {0}", obj.GetType().ToString()));
        }

        public static void SetIndexedValue(System.Array array, object[] indexes, object value)
        {
            switch (indexes.Length)
            {
                case 1:
                    array.SetValue(value, (int)indexes[0]);
                    return;
                case 2:
                    array.SetValue(value, (int)indexes[0], (int)indexes[1]);
                    return;
                case 3:
                    array.SetValue(value, (int)indexes[0], (int)indexes[1], (int)indexes[2]);
                    return;
            }

            throw new InvalidOperationException("Invalid number of subindices");
        }

        private static object GetIndexedValue(System.Array array, object[] indexes)
        {
            switch (indexes.Length)
            {
                case 1:
                    return array.GetValue((int)indexes[0]);
                case 2:
                    return array.GetValue((int)indexes[0], (int)indexes[1]);
                case 3:
                    return array.GetValue((int)indexes[0], (int)indexes[1], (int)indexes[2]);
            }

            throw new InvalidOperationException("Invalid number of subindices");
        }

        private static object GetIndexedValue(IList list, object[] indexes)
        {
            if (indexes.Length != 1)
                throw new InvalidOperationException("Invalid number of subindices");

            return list[(int)indexes[0]];
        }

        private static object GetIndexedValue(IDictionary dictionary, object[] indexes)
        {
            if (indexes.Length != 1)
                throw new InvalidOperationException("Invalid number of subindices");

            return dictionary[indexes[0]];
        }
    }
}
