using SiTef.net.Pool.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public class AbstractTerminalPool : ITerminalPool
    {

        public ITerminalRepository Repository { get; set; }

        /// <summary>
        /// Quantidade de Terminais no Pool
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Endereço IP do Servidor SiTef
        /// </summary>
        public string Server { get; set; }

        public string BusinessId { get; set; }

        /// <summary>
        /// Identificador único para esta instância de Pool,
        /// quando operando num ambiente de cluster ou distribuído.
        /// </summary>
        public string Id { get; set; }

        protected Stack<TerminalLease> Available { get; set; }

        protected List<TerminalLease> Leased { get; set; }

        public AbstractTerminalPool(string id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            Leased = new List<TerminalLease>(capacity);
        }

        protected void Init()
        {
            if (Available == null)
            {
                var terms = Repository.Lease(Id, Capacity);
                Available = new Stack<TerminalLease>(terms);
            }
        }

        public ITerminal GetTerminal()
        {
            if (Available == null) Init();

            if (Available.Count > 0)
            {
                TerminalLease lease = Available.Pop();
                Leased.Add(lease);
                var term = new Terminal(Server, String.Format("{D8}",lease.Id) , BusinessId);
                term.AddDisposeCallback(termId => { ReleaseTerminal(termId); });
                return term;
            }
            return null;
        }

        public void ReleaseTerminal(string terminal)
        {
            TerminalLease lease = Leased.FirstOrDefault(x => x.Id == int.Parse(terminal));
            Leased.Remove(lease);
            Available.Push(lease);
        }
    }
}
