using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Realiza a consulta de bins, que retorna o conjunto de funcionalidades relacionadas ao cartão 
    /// (por exemplo, se é permitido parcelamento da venda, se deve ser capturado código de segurança, qual rede fará o roteamento da transação, etc).
    /// </summary>
    public class QueryCardRequest : IActionModel
    {

        private IList<Field> _fields;

        public QueryCardRequest(NumeroDoCartao card, DataDeVencimento expiration)
        {
            _fields = new List<Field> { card, expiration };
        }

        public QueryCardRequest(Rede network, DataFiscal fiscalDate, HoraFiscal fiscalTime, CupomFiscal fiscalCoupom, CodigoDoCliente client, Operador oper, Supervisor supervisor, NumeroDoCartao card, DataDeVencimento expiration, TipoDeTransacao transaction)
        {
            _fields = new List<Field> { network, fiscalDate, fiscalTime, fiscalCoupom, client, oper, supervisor, card, expiration, transaction };
        }

        public IList<Field> GetFields()
        {
            return _fields;
        }
    }
}
