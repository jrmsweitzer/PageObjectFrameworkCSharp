using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectFramework.Framework.Exceptions
{
    public class WrongPageException : Exception
    {
        public WrongPageException()
            : base() { }

        public WrongPageException(string message)
            : base(message) { }
    }
}
