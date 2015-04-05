
namespace SiTef.net.Action.Model
{
    /// <summary>
    /// MKT_VENDA_SITEF  
    /// Efetua uma transação de venda crédito.
    /// </summary>
    public class VendaRequest : AbstractActionModel
    {
        public VendaRequest(
            Type.DataFiscal DataFiscal, //opcional
            Type.HoraFiscal HoraFiscal, //opcional
            Type.NumeroDeParcelas NumeroDeParcelas, //opcional
            Type.TipoDeFinanciamento TipoDeFinanciamento, //opcional
            Type.NumeroDoCartao NumeroDoCartao,
            Type.DataDeVencimento DataDeVencimento,
            Type.CodigoDeSeguranca CodigoDeSeguranca,
            Type.Valor Valor)
            : base(DataFiscal, HoraFiscal, NumeroDeParcelas, TipoDeFinanciamento, NumeroDoCartao, DataDeVencimento, CodigoDeSeguranca, Valor) { }
    }
}
