using SiTef.net.Action.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public class SiTefSaleAction : BaseAction<SiTefSaleRequest, SiTefSaleResponse>
    {

        public SiTefSaleAction(Terminal terminal) : base(ActionType.VENDA_SITEF, terminal) { }

        protected override SiTefSaleResponse ReadOutput()
        {
            return new SiTefSaleResponse(_terminal);
        }
    }
}
