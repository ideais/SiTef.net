using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool.model
{
    /// <summary>
    /// Representa um terminal, disponível ou não.
    /// </summary>
    public class TerminalLease
    {
        public int Id { get; set; }
        public string LeasedTo { get; set; }
        public DateTime LeasedAt { get; set; }
    }
}
