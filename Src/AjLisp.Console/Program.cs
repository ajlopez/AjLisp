using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using AjLisp;
using AjLisp.Language;
using AjLisp.Compiler;

namespace AjLisp.Console
{
    class Program
    {
        static Machine intr = new Machine();

        static void ProcessFile(string filename)
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

        static void Main(string[] args)
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
