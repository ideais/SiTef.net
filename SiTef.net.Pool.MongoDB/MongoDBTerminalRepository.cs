using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool.MongoDB
{
    public class MongoDBTerminalRepository : ITerminalRepository
    {
        public List<model.TerminalLease> Lease(string id, int quantity)
        {
            throw new NotImplementedException();
        }

        public void Release(List<model.TerminalLease> leases)
        {
            throw new NotImplementedException();
        }
    }
}
