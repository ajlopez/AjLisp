namespace AjLisp.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using AjLisp;
    using AjLisp.Compiler;
    using AjLisp.Language;

    public class Program
    {
        private static Machine intr = new Machine();

        public static void ProcessFile(string filename)
        {
            try
            {
                StreamReader tr = new StreamReader(filename);
                Compiler.Parser cmp = new Compiler.Parser(new Lexer(tr));
                object sexpr;
                sexpr = cmp.Compile();
                while (!(sexpr == null))
                {
                    sexpr = cmp.Compile();
                    intr.Evaluate(sexpr);
                }

                tr.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public static void Main(string[] args)
        {
            foreach (string filename in args)
            {
                ProcessFile(filename);
            }

            System.Console.WriteLine("AjLisp Interpreter");
            System.Console.Write("> ");
            Compiler.Parser cmp = new Compiler.Parser(new Lexer(System.Console.In));
            object sexpr;
            sexpr = cmp.Compile();
            while (!(sexpr == null))
            {
                try
                {
                    System.Console.WriteLine(intr.Evaluate(sexpr).ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }

                try
                {
                    System.Console.Write("> ");
                    sexpr = cmp.Compile();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
