﻿using System;

namespace SiTef.net.Pool
{
    public class TerminalFactory
    {

        public int Range { get; set; }

        public int Offset { get; set; }

        public TerminalFactory(string Servidor, string Empresa)
        {
            Range = 1000;
            Offset = 0;
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
            if (_term++ > ( Offset + Range + _term )) _term = 1;
            return _term.ToString("D8");
        }
    }
}
