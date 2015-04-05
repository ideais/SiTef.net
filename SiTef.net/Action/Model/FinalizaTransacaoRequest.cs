
namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Finaliza uma transação, a confirmando ou cancelando.
    /// </summary>
    public class FinalizaTransacaoRequest : AbstractActionModel
    {
        public FinalizaTransacaoRequest(Type.TipoTransacao tipoTransacao, Type.DadosDeConfirmacao dadosDeConfirmacao, Type.TipoConfirmacao tipoConfimacao, Type.FormasDePagamento formasDePagamento)
            : base(tipoTransacao, dadosDeConfirmacao, tipoConfimacao, formasDePagamento) { }
    }

    /// <summary>
    /// Confirma uma transação de venda
    /// </summary>
    public class ConfirmaVenda : FinalizaTransacaoRequest
    {
        public ConfirmaVenda(Type.DadosDeConfirmacao dadosDeConfirmacao) : base(Type.TipoTransacao.VENDA, dadosDeConfirmacao, Type.TipoConfirmacao.CONFIRMA, null) { }
    }

    /// <summary>
    /// Cancela uma transação de venda
    /// </summary>
    public class CancelaVenda : FinalizaTransacaoRequest
    {
        public CancelaVenda(Type.DadosDeConfirmacao dadosDeConfirmacao) : base(Type.TipoTransacao.VENDA, dadosDeConfirmacao, Type.TipoConfirmacao.CANCELA, null) { }
    }
}
