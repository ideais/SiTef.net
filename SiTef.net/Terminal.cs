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
        private short tef;

        private short transaction;

        public Terminal(string servidor, string terminal, string empresa)
        {
            tef = SiTefWrapper.IniciaTerminal(servidor, terminal, empresa);
        }

        public void IniciaTransacao()
        {
            transaction = SiTefWrapper.IniciaTransacao(tef);
            if (transaction < 0)
            {

            }
        }

        public void GravaCampo(Field campo)
        {
            if (transaction == 0) IniciaTransacao();
            SiTefWrapper.GravaCampo(tef, campo.Id, campo.Value);
        }

        public void Executa(short acao)
        {
            SiTefWrapper.Executa(tef, acao);
        }

        public Field LeCampo(short id)
        {
            StringBuilder valor = new StringBuilder();
            SiTefWrapper.LeCampo(tef, id, valor);
            return Field.InstanceOf(id, valor.ToString());
        }

        public bool ExistemMaisElementos(short campo)
        {
            return SiTefWrapper.ExistemMaisElementos(tef, campo) == 1;
        }


        public string DescricaoErro(short erro)
        {
            StringBuilder descricao = new StringBuilder();
            SiTefWrapper.DescricaoErro(tef, erro, descricao);
            return descricao.ToString();
        }

        public void FinalizaTerminal()
        {
            SiTefWrapper.FinalizaTerminal(tef);
        }

    }
}
