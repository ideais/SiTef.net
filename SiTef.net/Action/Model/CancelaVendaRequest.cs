using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// MKT_CANCELA_SITEF
    /// </summary>
    public class CancelaVendaRequest : AbstractActionModel
    {
        public CancelaVendaRequest(Rede rede, NumeroDoCartao cartao, NSUHost nsuHost, Data data, Valor valor)
            : base(rede, cartao, nsuHost, data, valor) { }

        public CancelaVendaRequest(
            Rede rede,
            DataFiscal dataFiscal,
            HoraFiscal horaFiscal,
            CupomFiscal cupomFiscal,
            CodigoDoCliente codigoDoCliente,
            Operador operador,
            Supervisor supervisor,
            TipoDeTerminal terminal,
            TipoOperacaoDeVenda tipoOperacaoDeVenda,
            NumeroDoCartao numeroDoCartao,
            Trilha1 trilha1,
            Trilha2 trilha2,
            NSUHost nsuHost,
            Data data,
            CodigoDeSeguranca codigoDeSeguranca,
            Valor valor,
            RG identidade,
            CamposVariaveisComPrefixo camposVariaveis
        )
            : base(rede, dataFiscal, horaFiscal, cupomFiscal, codigoDoCliente, operador, supervisor, terminal, tipoOperacaoDeVenda,
              numeroDoCartao, trilha1, trilha2, nsuHost, data, codigoDeSeguranca, valor, identidade, camposVariaveis) { }
    }
}
