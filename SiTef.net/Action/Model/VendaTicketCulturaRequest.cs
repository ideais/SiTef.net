using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class VendaTicketCulturaRequest : AbstractActionModel
    {
        public VendaTicketCulturaRequest(
            Type.Rede rede,
            Type.DataFiscal dataFiscal,
            Type.HoraFiscal horaFiscal,
            Type.CupomFiscal cupomFiscal,
            Type.CodigoDoCliente codigoDoCliente,
            Type.Operador operador,
            Type.Supervisor supervisor,
            Type.NumeroDoCartao cartao,
            Type.DataDeVencimento vencimento,
            Type.Perguntas perguntas,
            Type.CodigoDoRoteamento codigoDoRoteamento,
            Type.CodigoDoProduto codigoDoProduto,
            Type.CodigoLinhaDeCredito codigoLinhaDeCredito,
            Type.CodigoDeSeguranca codigoDeSeguranca,
            Type.Valor valor
            )
            : base(rede, dataFiscal, horaFiscal, cupomFiscal, codigoDoCliente, operador, supervisor, cartao, vencimento, perguntas, codigoDoRoteamento, codigoDoProduto, codigoLinhaDeCredito,
                codigoDeSeguranca, valor) { }

        public VendaTicketCulturaRequest(
            Type.NumeroDoCartao cartao,
            Type.DataDeVencimento vencimento,
            Type.Perguntas perguntas,
            Type.CodigoDoRoteamento codigoDoRoteamento,
            Type.CodigoDoProduto codigoDoProduto,
            Type.CodigoLinhaDeCredito codigoLinhaDeCredito,
            Type.Valor valor)
            : base(cartao, vencimento, perguntas, codigoDoRoteamento, codigoDoProduto, codigoLinhaDeCredito, valor) { }
    }
}
