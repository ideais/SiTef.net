using SiTef.net.Pool.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public class TerminalPool : ITerminalPool, IDisposable
    {

        public ITerminalRepository Repository { get; set; }
        
        private Stack<TerminalLease> Cache;
        private List<TerminalLease> Leased;

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
            Cache = new Stack<TerminalLease>();
            Leased = new List<TerminalLease>();
            Id = id;            
        }
       
        public ITerminal GetTerminal()
        {

            TerminalLease lease = null;

            if (Cache.Count == 0)
                lease = Repository.Lease(Id);
            else
                lease = Cache.Pop();

            if (lease == null)
                return null;

            Leased.Add(lease);

            // As propriedades do Lease tem prioridade sobre as configuradas no Pool
            var server = lease.Servidor != null ? lease.Servidor : Server;
            var empresa = lease.Empresa != null ? lease.Empresa : Server;

            ITerminal term = new Terminal(server, lease.Terminal, empresa);
            term.AddDisposeCallback(instance => { ReleaseTerminal(instance); });
            return term;
        }

        public void ReleaseTerminal(ITerminal terminal)
        {
            var lease = Leased.FirstOrDefault<TerminalLease>(x => x.Terminal == terminal.Id);
            Leased.Remove(lease);
            Cache.Push(lease);
        }

        public void Dispose()
        {
            foreach (var lease in Cache)
            {
                Repository.Release(lease.Terminal);
            }
            foreach (var lease in Leased)
            {
                Repository.Release(lease.Terminal);
            }
        }
    }
}
