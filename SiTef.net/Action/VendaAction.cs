using SiTef.net.Action.Model;

namespace SiTef.net.Action
{
    public class VendaAction : AbstractAction<VendaRequest, VendaResponse>
    {
        public VendaAction(ITerminal terminal) : base(ActionType.VENDA_SITEF, terminal) { }

        protected override VendaResponse ReadOutput()
        {
            return new VendaResponse(_terminal);
        }
    }
}
