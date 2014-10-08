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

        private short transaction;

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
            {

            }
        }

        public void GravaCampo(Field campo)
        {
            if (transaction == 0) IniciaTransacao();
            SiTef.GravaCampo(tef, campo.Id, campo.Value);
        }

        public void Executa(short acao)
        {
            SiTef.Executa(tef, acao);
        }

        public Field LeCampo(short id)
        {
            StringBuilder valor = new StringBuilder();
            SiTef.LeCampo(tef, id, valor);
            return Field.InstanceOf(id, valor.ToString());
        }

        public bool ExistemMaisElementos(short campo)
        {
            return SiTef.ExistemMaisElementos(tef, campo) == 1;
        }


        public string DescricaoErro(short erro)
        {
            StringBuilder descricao = new StringBuilder();
            SiTef.DescricaoErro(tef, erro, descricao);
            return descricao.ToString();
        }

        public void FinalizaTerminal()
        {
            SiTef.FinalizaTerminal(tef);
        }

    }
}
