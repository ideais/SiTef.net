using SiTef.net.Exceptions;
using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net
{
    /// <summary>
    /// Wrapper para operações utilizando a LibSitef
    /// </summary>
    public class Terminal : ITerminal
    {
        private IntPtr term;

        private int transaction;

        private List<System.Action<string>> DisposeCallbacks;

        private string _terminal;

        public Terminal(string servidor, string terminal, string empresa)
        {
            _terminal = terminal;
            term = SiTef.IniciaTerminal(servidor, terminal, empresa);
            if (IntPtr.Zero == term)
                throw new TerminalException("unable to initialize terminal");
            IniciaTransacao();
        }
        
        public void IniciaTransacao()
        {
            transaction = SiTef.IniciaTransacao(term);
            if (transaction < 0)
                throw new TerminalException(DescricaoErro(transaction));
        }

        public void GravaCampo(IntPtr id, string value)
        {
            int result = SiTef.GravaCampo(term, id, value);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));

        }

        public void Executa(int acao)
        {
            int result = SiTef.Executa(term, (IntPtr)acao);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));
        }

        public String LeCampo(int id, int length)
        {
            StringBuilder valor = new StringBuilder(length);
            int result = SiTef.LeCampo(term, (IntPtr)id, valor);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));
            if (valor.Length < length) length = valor.Length;
            return valor.ToString().Substring(0,length);
        }

        public bool ExistemMaisElementos(int campo)
        {
            return SiTef.ExistemMaisElementos(term, campo) == 1;
        }


        public string DescricaoErro(int erro)
        {
            StringBuilder descricao = new StringBuilder(127);
            SiTef.DescricaoErro(term, (IntPtr)erro, descricao);
            return descricao.ToString();
        }

        public void FinalizaTerminal()
        {
            SiTef.FinalizaTerminal(term);
        }


        public void Dispose()
        {
            SiTef.FinalizaTerminal(term);
            foreach (var action in DisposeCallbacks)
                action(_terminal);
        }


        public void AddDisposeCallback(Action<string> callback)
        {
            DisposeCallbacks.Add(callback);
        }
    }
}
