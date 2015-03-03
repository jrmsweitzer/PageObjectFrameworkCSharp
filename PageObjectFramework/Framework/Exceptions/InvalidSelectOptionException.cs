using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectFramework.Framework.Exceptions
{
    public class InvalidSelectOptionException : Exception
    {
        public InvalidSelectOptionException()
            : base() { }

        public InvalidSelectOptionException(string message)
            : base(message) { }
    }
}
