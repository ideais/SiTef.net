using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    /// <summary>
    /// Ação utilizada para confirmar, ou cancelar uma transação de venda
    /// </summary>
    public class FinalizaTransacaoAction : AbstractAction<Model.FinalizaTransacaoRequest, Model.NullResponse>
    {
        public FinalizaTransacaoAction(ITerminal terminal) : base(ActionType.FINALIZA_TRANSACAO, terminal) { }

        protected override Model.NullResponse ReadOutput()
        {
            return new Model.NullResponse();
        }
    }
}
