using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Exceptions
{
    public class TerminalException: Exception
    {
        public TerminalException() : base() { }
        public TerminalException(string message) : base(message) { }
    }
}
