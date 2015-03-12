using System;

namespace PageObjectFramework.Framework.Exceptions
{
    [Serializable]
    public class InvalidSelectOptionException : Exception
    {
        public InvalidSelectOptionException()
            : base() { }

        public InvalidSelectOptionException(string message)
            : base(message) { }
    }
}
