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
        List<TerminalLease> Lease(string id, int quantity);
        void Release(List<TerminalLease> leases);
    }
}
