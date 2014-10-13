using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Resposta de uma requisição de Venda à Crédito
    /// </summary>
    public class SiTefSaleResponse : AbstractActionModel
    {
        public SiTefSaleResponse(Terminal terminal)
        {
            _fields = new List<Field>
            {
                //1
                new Rede(terminal),
                //9
                new DadosDeConfirmacao(terminal),
                new CodigoDeRespostaSiTef(terminal),
                new TextoParaExibicao(terminal),
                new CodigoRespostaInstituicao(terminal),
                new Data(terminal)/*,
                new Hora(terminal),
                new NSUHost(terminal),
                new CodigoDoEstabelecimento(terminal),
                new NumeroAutorizacao(terminal),
                new NSUSiTef(terminal),
                new LinhasDeCupon(terminal),
                new LinhasDeCupomEstabelecimento(terminal),
                new TextoParaExibicaoVisorCliente(terminal)*/
            };
        }
    }
}
