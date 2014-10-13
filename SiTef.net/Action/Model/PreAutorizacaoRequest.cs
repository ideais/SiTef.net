using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class PreAutorizacaoRequest : AbstractActionModel
    {
        public PreAutorizacaoRequest(params Field[] fields) : base(fields) { }

        public PreAutorizacaoRequest(string network, DateTime date, string card, string expiration, decimal value, decimal serviceTax, string securityCode)
        {

        }

        public PreAutorizacaoRequest(Rede network, DataFiscal date, HoraFiscal time, DataDeVencimento expiration, Valor value, ValorTaxaDeServico serviceTax, CodigoDeSeguranca securityCode)
            : base(network, date, time, expiration, value, serviceTax, securityCode) { }

    }
}
