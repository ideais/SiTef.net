
namespace SiTef.net.Action
{
    public class ConsultaCartaoAction : AbstractAction<Model.ConsultaCartaoRequest, Model.ConsultaCartaoResponse>
    {
        public ConsultaCartaoAction(ITerminal terminal) : base(ActionType.CONSULTA_CARTAO, terminal) { }

        protected override Model.ConsultaCartaoResponse ReadOutput()
        {
            return new Model.ConsultaCartaoResponse(_terminal);
        }
    }
}
