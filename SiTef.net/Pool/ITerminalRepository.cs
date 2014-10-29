using SiTef.net.Pool.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public interface ITerminalRepository
    {
        TerminalLease Lease(string id);
        void Release(string terminal);
    }
}
