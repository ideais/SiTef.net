
namespace SiTef.net.Action
{
    public class CapturaPreAutorizacaoAction : AbstractAction<Model.CapturaRequest, Model.CapturaResponse>
    {
        public CapturaPreAutorizacaoAction(ITerminal terminal) : base(ActionType.CAPTURA_PRE_AUTORIZACAO, terminal) { }

        protected override Model.CapturaResponse ReadOutput()
        {
            return new Model.CapturaResponse(_terminal);
        }
    }
}
