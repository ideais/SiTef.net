using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Type
{
    /// <summary>
    /// Ao realizar a consulta de um cartão Ticket Cultura, (através da ação MKT_CONSULTA_CARTAO), pode ser retornados os  campos referentes a Linha de crédito.
    /// Através da função MKT_Existem_Mais_Elementos (passando o campo 368), é possível saber quantas linhas de crédito existem para o cartão consultado.
    /// Serão retornadas as mesmas quantidades de elementos para os campos 358, 369 e 370.
    /// Normalmente será retornado apenas uma linha de crédito, porém a automação deve estar preparada caso, no futuro, seja retornadas outras linhas de crédito.
    /// Uma vez escolhido o código da Linha de Crédito, deve-se verificar se há um tratamento para o campo PERGUNTAS (358).
    /// </summary>
    public class LinhaCreditoValeCultura : IField
    {

        public Perguntas Perguntas { get; set; }

        public CodigoLinhaDeCredito CodigoLinhaDeCredito { get; set; }

        public DescricaoLinhaDeCredito DescricaoLinhaDeCredito { get; set; }

        public FlagsDeControleDeOperacao FlagsDeControleDeOperacao { get; set; }

        public LinhaCreditoValeCultura(ITerminal terminal)
        {
            Perguntas = new Perguntas(terminal);
            CodigoLinhaDeCredito = new CodigoLinhaDeCredito(terminal);
            DescricaoLinhaDeCredito = new DescricaoLinhaDeCredito(terminal);
            FlagsDeControleDeOperacao = new FlagsDeControleDeOperacao(terminal);
        }

        public void WriteTo(ITerminal terminal)
        {
            throw new NotImplementedException();
        }
    }
}
