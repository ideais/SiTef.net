using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public class VendaTicketCulturaAction : AbstractAction<Model.VendaTicketCulturaRequest, Model.VendaTicketCulturaResponse>
    {

        public VendaTicketCulturaAction(ITerminal terminal) : base(ActionType.VENDA_DEBITO, terminal) { }

        protected override Model.VendaTicketCulturaResponse ReadOutput()
        {
            return new Model.VendaTicketCulturaResponse(_terminal);
        }
    }
}
