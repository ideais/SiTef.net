using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    /// <summary>
    /// Efetua uma transação de estorno de pré-autorização ou captura de pré-autorização.
    /// </summary>
    public class EstornoPreAutorizacaoAction : AbstractAction<Model.EstornoRequest, Model.EstornoResponse>
    {

        public EstornoPreAutorizacaoAction(Terminal terminal) : base(ActionType.ESTORNO_PRE_AUTORIZACAO, terminal) { }

        protected override Model.EstornoResponse ReadOutput()
        {
            throw new NotImplementedException();
        }
    }
}
