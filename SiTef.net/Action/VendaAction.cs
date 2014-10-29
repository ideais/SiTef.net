using SiTef.net.Action.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
