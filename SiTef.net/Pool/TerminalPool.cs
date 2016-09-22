using System;
using System.Collections.Generic;
using System.Linq;
using SiTef.net.Pool.Model;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public class TerminalPool : ITerminalPool, IDisposable
    {
        public ITerminalRepository Repository { get; set; }

        private Stack<TerminalLease> _cache;
        private List<TerminalLease> _leased;

        private int _timeout = 30; // Leases ficam no cache por apenas 30s

        /// <summary>
        /// Endereço IP do Servidor SiTef
        /// </summary>
        public string Server { get; set; }

        public string Empresa { get; set; }

        /// <summary>
        /// Identificador único para esta instância de Pool,
        /// quando operando num ambiente de cluster ou distribuído.
        /// </summary>
        public string Id { get; set; }


        public TerminalPool(string id)
        {
            _cache = new Stack<TerminalLease>();
            _leased = new List<TerminalLease>();
            Id = id;
        }

        public TerminalPool(string id, int timeout)
            : this(id)
        {
            _timeout = timeout;
        }

        public async Task<ITerminal> GetTerminalAsync()
        {
            TerminalLease lease;

            if (_cache.Count == 0)
                lease = await Repository.LeaseAsync(Id);
            else
                lease = _cache.Pop();

            if (lease == null)
                return null;

            _leased.Add(lease);

            // As propriedades do Lease tem prioridade sobre as configuradas no Pool
            var server = lease.Servidor ?? Server;
            var empresa = lease.Empresa ?? Empresa;

            ITerminal term = new Terminal(server, lease.Terminal, empresa);
            term.AddDisposeCallback(async instance => { await ReleaseTerminalAsync(instance); });
            return term;
        }

        public async Task ReleaseTerminalAsync(ITerminal terminal)
        {
            var lease = _leased.FirstOrDefault(x => x.Terminal == terminal.Id);
            _leased.Remove(lease);
            if ((DateTime.Now - lease.LeasedAt).TotalSeconds >= _timeout)
                await Repository.ReleaseAsync(lease.Terminal);
            else
                _cache.Push(lease);
        }

        public void Dispose()
        {
            foreach (var lease in _cache)
            {
                Repository.ReleaseAsync(lease.Terminal);
            }
            foreach (var lease in _leased)
            {
                Repository.ReleaseAsync(lease.Terminal);
            }
        }

        ~TerminalPool()
        {
            Dispose();
        }

        public async Task ReclaimTerminalsAsync()
        {
            var leases = await Repository.ReclaimAsync(Id);
            foreach (var lease in leases)
                if (_leased.Where(x => x.LeasedTo == Id).Count() == 0)
                    _cache.Push(lease);
        }
    }
}
