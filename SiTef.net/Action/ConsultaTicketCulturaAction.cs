
namespace SiTef.net.Action
{
    /// <summary>
    /// Para este produto específico, a ação MKT_CONSULTA_CARTAO não requer o campo Data de vencimento (5) como entrada.
    /// Entretanto, ele é retornado como resultado e deve ser repassado na transação de venda.
    /// </summary>
    public class ConsultaTicketCulturaAction : AbstractAction<Model.ConsultaTicketCulturaRequest, Model.ConsultaTicketCulturaResponse>
    {
        public ConsultaTicketCulturaAction(ITerminal terminal) : base(ActionType.CONSULTA_CARTAO, terminal) { }

        protected override Model.ConsultaTicketCulturaResponse ReadOutput()
        {
            return new Model.ConsultaTicketCulturaResponse(_terminal);
        }
    }
}
