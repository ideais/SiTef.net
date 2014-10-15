using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class EstornoRequest : AbstractActionModel
    {
        public EstornoRequest(
            NumeroDoCartao cartao,
            DataDeVencimento vencimento,
            Valor valor,
            DataDaTransacao data,
            NumeroAutorizacao autorizacao,
            NSUHost nsuHost,
            CodigoDeSeguranca codigoSeguranca
        )
            : base(TipoTransacao.ESTORNO_PRE_AUTORIZACAO, cartao, vencimento, valor, data, autorizacao, nsuHost, codigoSeguranca) { }
    }
}
