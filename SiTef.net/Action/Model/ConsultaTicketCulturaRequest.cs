using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    public class ConsultaTicketCulturaRequest : AbstractActionModel
    {

        /// <summary>
        /// Construtor com todos os campos opcionais.
        /// </summary>
        /// <param name="rede">Opcional</param>
        /// <param name="data">Opcional</param>
        /// <param name="hora">Opcional</param>
        /// <param name="cupom">Opcional</param>
        /// <param name="cliente">Opcional</param>
        /// <param name="operador">Opcional</param>
        /// <param name="supervisor">Opcional</param>
        /// <param name="cartao">Obrigatório</param>
        public ConsultaTicketCulturaRequest(
            Type.Rede rede,
            Type.DataFiscal data,
            Type.HoraFiscal hora,
            Type.CupomFiscal cupom,
            Type.CodigoDoCliente cliente,
            Type.Operador operador,
            Type.Supervisor supervisor,
            Type.NumeroDoCartao cartao)
            : base(rede, data, hora, cupom, cliente, operador, supervisor, cartao, Type.TipoVenda.DEBITO) { }

        /// <summary>
        /// Construtuor apenas com os campos obrigatórioas
        /// </summary>
        /// <param name="cartao"></param>
        public ConsultaTicketCulturaRequest(Type.NumeroDoCartao cartao) : base(cartao, Type.TipoVenda.DEBITO) { }
    }
}
