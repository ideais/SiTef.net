using SiTef.net.Action.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public class VendaSiTefAction : AbstractAction<VendaSiTefRequest, VendaSiTefResponse>
    {

        public VendaSiTefAction(Terminal terminal) : base(ActionType.VENDA_SITEF, terminal) { }

        protected override VendaSiTefResponse ReadOutput()
        {
            return new VendaSiTefResponse(_terminal);
        }
    }
}
