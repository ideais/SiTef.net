using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public class QueryCardAction : AbstractAction<Model.QueryCardRequest, Model.QueryCardResponse>
    {

        public QueryCardAction(Terminal terminal) : base(ActionType.CONSULTA_CARTAO, terminal) { }

        protected override Model.QueryCardResponse ReadOutput()
        {
            return new Model.QueryCardResponse(_terminal);
        }
    }
}
