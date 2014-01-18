namespace AjLisp.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using AjLisp.Language;

    // Based on AjSharp AjLanguage.TypeUtilities
    // Based on PythonSharp.Utilitites
    // Based on RubySharp.Utilitites
    public class TypeUtilities
    {
        private static bool referencedAssembliesLoaded = false;

        public static Type GetType(ValueEnvironment environment, object typeref)
        {
            if (typeref is IExpression && !(typeref is Identifier))
                typeref = Machine.Evaluate(typeref, environment);

            if (typeref is Type)
                return (Type)typeref;

            if (typeref is string)
                return GetType(environment, (string)typeref);

            if (typeref is Identifier)
                return GetType(environment, (Identifier)typeref);

            throw new InvalidOperationException("Invalid type");
        }

        public static Type GetType(ValueEnvironment environment, Identifier identifier)
        {
            string name = identifier.Name;

            object obj = environment.GetValue(name);

            if (obj != null && obj is Type)
                return (Type)obj;

            return GetType(name);
        }

        public static Type GetType(ValueEnvironment environment, string name)
        {
            object obj = environment.GetValue(name);

            if (obj != null && obj is Type)
                return (Type)obj;

            return GetType(name);
        }

        public static Type AsType(string name)
        {
            Type type = Type.GetType(name);

            if (type != null)
                return type;

            type = GetTypeFromLoadedAssemblies(name);

            if (type != null)
                return type;

            type = GetTypeFromPartialNamedAssembly(name);

            if (type != null)
                return type;

            LoadReferencedAssemblies();

            type = GetTypeFromLoadedAssemblies(name);

            if (type != null)
                return type;

            return null;
        }

        public static Type GetType(string name)
        {
            Type type = AsType(name);

            if (type != null)
                return type;

            throw new InvalidOperationException(string.Format("Unknown type '{0}'", name));
        }

        public static ICollection<Type> GetTypesByNamespace(string @namespace)
        {
            IList<Type> types = new List<Type>();

            LoadReferencedAssemblies();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var type in assembly.GetTypes().Where(tp => tp.Namespace == @namespace))
                    types.Add(type);

            return types;
        }

        public static bool IsNamespace(string name)
        {
            if (GetNamespaces().Contains(name))
                return true;

            return GetNamespaces().Any(n => n != null && n.StartsWith(name + "."));
        }

        public static IList<string> GetNames(Type type)
        {
            return type.GetMembers(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Select(m => m.Name).ToList();
        }

        public static object GetValue(Type type, string name)
        {
            try
            {
                return type.InvokeMember(name, System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, null, null);
            }
            catch
            {
                return type.GetMethod(name);
            }
        }

        public static object InvokeTypeMember(Type type, string name, IList<object> parameters)
        {
            return type.InvokeMember(name, System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static, null, null, parameters == null ? null : parameters.ToArray());
        }

        public static object ParseEnumValue(Type type, string name)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo fi = fields[i];
                if (fi.Name == name)
                    return fi.GetValue(null);
            }

            throw new InvalidOperationException(string.Format("'{0}' is not a valid value of '{1}'", name, type.Name));
        }

        private static ICollection<string> GetNamespaces()
        {
            List<string> namespaces = new List<string>();

            LoadReferencedAssemblies();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type type in assembly.GetTypes())
                    if (!namespaces.Contains(type.Namespace))
                        namespaces.Add(type.Namespace);

            return namespaces;
        }

        private static Type GetTypeFromPartialNamedAssembly(string name)
        {
            int p = name.LastIndexOf(".");

            if (p < 0)
                return null;

            string assemblyName = name.Substring(0, p);

            try
            {
                Assembly assembly = Assembly.LoadWithPartialName(assemblyName);

                return assembly.GetType(name);
            }
            catch
            {
                return null;
            }
        }

        private static Type GetTypeFromLoadedAssemblies(string name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(name);

                if (type != null)
                    return type;
            }

            return null;
        }

        private static void LoadReferencedAssemblies()
        {
            if (referencedAssembliesLoaded)
                return;

            List<string> loaded = new List<string>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                loaded.Add(assembly.GetName().Name);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                LoadReferencedAssemblies(assembly, loaded);

            referencedAssembliesLoaded = true;
        }

        private static void LoadReferencedAssemblies(Assembly assembly, List<string> loaded)
        {
            foreach (AssemblyName referenced in assembly.GetReferencedAssemblies())
                if (!loaded.Contains(referenced.Name))
                {
                    loaded.Add(referenced.Name);
                    Assembly newassembly = Assembly.Load(referenced);
                    LoadReferencedAssemblies(newassembly, loaded);
                }
        }
    }
}
