using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net
{
    public class TerminalFactory
    {

        public TerminalFactory(string Servidor, string Empresa)
        {
            this.Servidor = Servidor;
            this.Empresa = Empresa;
        }
        
        /// <summary>
        /// Endereço IP do Servidor SiTef
        /// </summary>
        public string Servidor { get; set; }

        /// <summary>
        /// Identificador da empresa
        /// </summary>
        public String Empresa { get; set; }

        /// <summary>
        /// Retorna um novo terminal já inicializado e pronto para ser utilizado.
        /// </summary>
        /// <returns></returns>
        public Terminal NewInstance()
        {
            return new Terminal(Servidor, NextTerm(), Empresa);
        }

        private int _term = 0;
        private string NextTerm()
        {
            if (_term++ > 99999999) _term = 1;
            return _term.ToString("D8");
        }
    }
}
