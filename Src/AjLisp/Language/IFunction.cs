namespace AjLisp.Language
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public interface IFunction
    {
        object Apply(List arguments, ValueEnvironment environment);
    }
}
