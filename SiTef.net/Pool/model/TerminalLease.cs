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
        /// <summary>
        /// Código do Terminal SiTef
        /// </summary>
        public string Terminal { get; set; }

        /// <summary>
        /// IP do Servidor SiTef
        /// </summary>
        public string Servidor { get; set; }

        /// <summary>
        /// Código da Empresa
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Identificação única do Pool para onde este terminal foi
        /// alocado.
        /// </summary>
        public string LeasedTo { get; set; }

        /// <summary>
        /// Data de alocação do terminal no Pool.
        /// </summary>
        public DateTime LeasedAt { get; set; }
    }
}
