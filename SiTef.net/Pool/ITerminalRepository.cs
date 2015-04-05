using SiTef.net.Pool.Model;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public interface ITerminalRepository
    {
        Task<TerminalLease> LeaseAsync(string id);
        Task ReleaseAsync(string terminal);
    }
}
