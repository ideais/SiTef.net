using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool
{
    public interface ITerminalPool
    {
        /// <summary>
        /// Pega um terminal configurado do Pool
        /// </summary>
        /// <returns></returns>
        ITerminal GetTerminal();

        /// <summary>
        /// Devolve o terminal ao Pool
        /// </summary>
        /// <param name="terminal">Terminal a ser devolvido</param>
        /// <returns></returns>
        void ReleaseTerminal(ITerminal terminal);
    }
}
