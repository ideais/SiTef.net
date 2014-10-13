using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// MKT_VENDA_SITEF  
    /// Efetua uma transação de venda crédito.
    /// </summary>
    public class VendaSiTefRequest : AbstractActionModel
    {
        public VendaSiTefRequest( 
            NumeroDeParcelas parcels, 
            TipoDeFinanciamento installment,
            NumeroDoCartao cardNumber,
            DataDeVencimento cardExpiration,
            CodigoDeSeguranca securityCode,
            Valor value) : base( parcels,installment,cardNumber,cardExpiration,securityCode,value) { }
    }
}
