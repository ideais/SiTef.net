using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Exceptions
{
    public class ActionException : Exception
    {
        public ActionException() : base() { }
        public ActionException(string message) : base(message) { }
    }
}
