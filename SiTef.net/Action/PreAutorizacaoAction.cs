
namespace SiTef.net.Action
{
    /// <summary>
    /// Executa uma transação de pré-autorização.
    /// Quando reservamos um valor no cartão do cliente, a fim de efetuar a cobrança posteriormente
    /// numa operação de captura.
    /// </summary>
    public class PreAutorizacaoAction : AbstractAction<Model.PreAutorizacaoRequest, Model.PreAutorizacaoResponse>
    {
        public PreAutorizacaoAction(ITerminal terminal) : base(ActionType.PRE_AUTORIZACAO, terminal) { }

        protected override Model.PreAutorizacaoResponse ReadOutput()
        {
            return new Model.PreAutorizacaoResponse(_terminal);
        }
    }
}
