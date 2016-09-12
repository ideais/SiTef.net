using SiTef.net.Pool.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiTef.net.Pool.Dynamic
{
    public class DynamicTerminalRepository : ITerminalRepository
    {
        public Task<TerminalLease> LeaseAsync(string id)
        {
            //SiTef format AA999999

            var letterRandom = new Random(Guid.NewGuid().GetHashCode());
            var position1 = (char)letterRandom.Next(65, 90);
            var position2 = (char)letterRandom.Next(65, 90);

            var numberRandom = new Random(Guid.NewGuid().GetHashCode());

            int number;

            do
            {
                number = numberRandom.Next(1, 999999);
            }
            while (number >= 900 && number <= 999);

            var terminal = new TerminalLease();
            terminal.Terminal = string.Format("{0}{1}{2}", position1, position2, number.ToString().PadLeft(6, '0'));

            return Task.FromResult(terminal);
        }

        public Task<List<TerminalLease>> ReclaimAsync(string id)
        {
            return Task.FromResult(new List<TerminalLease>());
        }

        public Task ReleaseAsync(string terminal)
        {
            // Ignora o Release do Repository
            return Task.FromResult(0);
        }
    }
}
