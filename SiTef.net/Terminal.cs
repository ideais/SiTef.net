using SiTef.net.Exceptions;
using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net
{
    public class Terminal
    {
        private UIntPtr tef;

        private int transaction;

        public Terminal(string servidor, string terminal, string empresa)
        {
            tef = SiTef.IniciaTerminal(servidor, terminal, empresa);
            if (UIntPtr.Zero == tef)
                throw new TerminalException("unable to initialize terminal");
        }

        public void IniciaTransacao()
        {
            transaction = SiTef.IniciaTransacao(tef);
            if (transaction < 0)
                throw new TerminalException(DescricaoErro(transaction));
        }

        public void GravaCampo(Field campo)
        {
            if (transaction == 0) IniciaTransacao();

            int result = SiTef.GravaCampo(tef, campo.Id, campo.Value);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));

        }

        public void Executa(int acao)
        {
            int result = SiTef.Executa(tef, acao);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));
        }

        public String LeCampo(int id)
        {
            StringBuilder valor = new StringBuilder();
            int result = SiTef.LeCampo(tef, id, valor);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));
            return valor.ToString();
        }

        public bool ExistemMaisElementos(int campo)
        {
            return SiTef.ExistemMaisElementos(tef, campo) == 1;
        }


        public string DescricaoErro(int erro)
        {
            StringBuilder descricao = new StringBuilder(128);
            SiTef.DescricaoErro(tef, erro, descricao);
            return descricao.ToString();
        }

        public void FinalizaTerminal()
        {
            SiTef.FinalizaTerminal(tef);
        }

    }
}
