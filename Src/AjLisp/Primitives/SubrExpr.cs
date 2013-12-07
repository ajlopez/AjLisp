namespace AjLisp.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;

    public class SubrExpr : Subr
    {
        private object arguments;
        private List body;

        public SubrExpr(object args, List body)
        {
            this.arguments = args;
            this.body = body;
        }

        public override object Execute(List args, ValueEnvironment env)
        {
            ValueEnvironment nenv;
            nenv = this.MakeEnvironment(args, env);
            return this.ExecuteBody(nenv);
        }

        internal ValueEnvironment MakeEnvironment(List arguments, ValueEnvironment parent)
        {
            ValueEnvironment nenv = new ValueEnvironment(parent);

            if (Predicates.IsNil(this.arguments))
                return nenv;

            if (Predicates.IsIdentifier(this.arguments))
            {
                nenv.SetValue(((Identifier) this.arguments).Name, arguments);
                return nenv;
            }

            List argnames = (List) this.arguments;
            List argvalues = arguments;
            Identifier argname;
            object argvalue;

            while (!Predicates.IsNil(argnames) && !Predicates.IsNil(argvalues))
            {
                argname = (Identifier) argnames.First;
                argvalue = argvalues.First;
                nenv.SetValue(argname.Name, argvalue);
                argnames = argnames.Next;
                argvalues = argvalues.Next;
            }

            return nenv;
        }

        internal object ExecuteBody(ValueEnvironment env)
        {
            FSubrProgN progn = new FSubrProgN();
            return progn.Execute(this.body, env);
        }
    }
}
