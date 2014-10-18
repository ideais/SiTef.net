using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Finaliza uma transação, a confirmando ou cancelando.
    /// </summary>
    public class FinalizaTransacaoRequest : AbstractActionModel
    {
        public FinalizaTransacaoRequest( Type.TipoTransacao TipoTransacao, Type.DadosDeConfirmacao DadosDeConfirmacao, Type.TipoConfirmacao TipoConfimacao, Type.FormasDePagamento FormasDePagamento ) 
            : base(TipoTransacao, DadosDeConfirmacao, TipoConfimacao, FormasDePagamento ) { }
    }

    /// <summary>
    /// Confirma uma transação de venda
    /// </summary>
    public class ConfirmaVenda : FinalizaTransacaoRequest
    {
        public ConfirmaVenda(Type.DadosDeConfirmacao DadosDeConfirmacao) : base(Type.TipoTransacao.VENDA, DadosDeConfirmacao, Type.TipoConfirmacao.CONFIRMA, null) { }
    }

    /// <summary>
    /// Cancela uma transação de venda
    /// </summary>
    public class CancelaVenda : FinalizaTransacaoRequest
    {
        public CancelaVenda(Type.DadosDeConfirmacao DadosDeConfirmacao) : base(Type.TipoTransacao.VENDA, DadosDeConfirmacao, Type.TipoConfirmacao.CANCELA, null) { }
    }
}
