
namespace SiTef.net.Action
{
    /// <summary>
    /// Efetua uma transação de estorno de pré-autorização ou captura de pré-autorização.
    /// </summary>
    public class EstornoPreAutorizacaoAction : AbstractAction<Model.EstornoRequest, Model.EstornoResponse>
    {
        public EstornoPreAutorizacaoAction(ITerminal terminal) : base(ActionType.ESTORNO_PRE_AUTORIZACAO, terminal) { }

        protected override Model.EstornoResponse ReadOutput()
        {
            return new Model.EstornoResponse(_terminal);
        }
    }
}
