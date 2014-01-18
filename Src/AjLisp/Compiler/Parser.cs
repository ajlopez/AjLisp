namespace AjLisp.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjLisp.Language;
    using AjLisp.Primitives;

    public class Parser
    {
        private Lexer lexer;

        public Parser(Lexer tokenizer)
        {
            this.lexer = tokenizer;
        }

        public Parser(string expr) : this(new Lexer(expr))
        {
        }

        public object Compile()
        {
            if (!this.HasMoreTokens())
                return null;
            
            return this.CompileTerm();
        }

        private List CompileList()
        {
            if (this.NextTokenIs(TokenType.Separator, ')'))
            {
                return null;
            }

            object first = this.CompileTerm();

            if (first is Identifier)
            {
                var id = (Identifier)first;

                if (id.Name.Length > 1 && id.Name.EndsWith("."))
                    first = new SubrNew(id.Name.Substring(0, id.Name.Length - 1));
            }

            object rest;

            if (this.NextTokenIs(TokenType.Name, "."))
            {
                rest = this.CompileTerm();
                this.ParseNextToken(TokenType.Separator, ')');
            }
            else
                rest = this.CompileList();

            List list = new List(first, rest);
            return list;
        }

        private object CompileTerm()
        {
            Token token = this.NextToken();

            if (token.Type == TokenType.Separator && token.Value.Equals('('))
                return this.CompileList();

            if (token.Type == TokenType.Number)
                return token.Value;

            if (token.Type == TokenType.String)
                return token.Value;

            if (token.Type == TokenType.Name)
            {
                if (token.Value.Equals("nil"))
                    return null;

                if (token.Value.Equals("false"))
                    return false;

                if (token.Value.Equals("true"))
                    return true;

                if (token.Value.Equals("'"))
                    return new List(new Identifier("quote"), new List(this.CompileTerm()));

                if (token.Value.Equals("`"))
                    return new List(new Identifier("backquote"), new List(this.CompileTerm()));

                if (token.Value.Equals(","))
                    return new List(new Identifier("comma"), new List(this.CompileTerm()));

                if (token.Value.Equals(",@"))
                    return new List(new Identifier("comma-at"), new List(this.CompileTerm()));

                return new Identifier((string)token.Value);
            }

            throw new CompilerException(string.Format("Unexpected token: {0}", token.Value));
        }

        private bool HasMoreTokens()
        {
            Token token;

            token = this.lexer.NextToken();

            if (!(token == null))
            {
                this.lexer.PushToken(token);
                return true;
            }

            return false;
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }

        private void ParseNextToken(TokenType type, object expected)
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                throw new CompilerException(string.Format("Expected '{0}'", expected));

            if (token.Type != type || !token.Value.Equals(expected))
                throw new CompilerException(string.Format("Expected '{0}'", expected));
        }

        private bool NextTokenIs(TokenType type, object expected)
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return false;

            if (token.Type == type && token.Value.Equals(expected))
                return true;

            this.lexer.PushToken(token);

            return false;
        }
    }

    public class CompilerException : Exception
    {
        public CompilerException(string msg) : base(msg)
        {
        }
    }
}
