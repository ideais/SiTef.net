using SiTef.net.Type;

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
