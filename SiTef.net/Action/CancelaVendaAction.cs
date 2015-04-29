using SiTef.net.Action.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    /// <summary>
    /// Realiza uma transação de Cancelamento de venda e inclusão de venda.
    /// </summary>
    public class CancelaVendaAction : AbstractAction<CancelaVendaRequest, CancelaVendaResponse>
    {

        public CancelaVendaAction(ITerminal terminal) : base(ActionType.CANCELA_SITEF, terminal) { }

        protected override CancelaVendaResponse ReadOutput()
        {
            return new CancelaVendaResponse(_terminal);
        }
    }
}
