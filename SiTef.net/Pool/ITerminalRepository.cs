using SiTef.net.Pool.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public interface ITerminalRepository
    {
        Task<TerminalLease> LeaseAsync(string id);
        Task ReleaseAsync(string terminal);
        /// <summary>
        /// Goes over the persistence, and reclaims any left-over leases
        /// from previeous sessions.
        /// </summary>
        /// <returns></returns>
        Task<List<TerminalLease>> ReclaimAsync(string id);
    }
}
