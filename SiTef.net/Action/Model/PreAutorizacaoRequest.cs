using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Requisição de pré-autorização
    /// </summary>
    public class PreAutorizacaoRequest : AbstractActionModel
    {
        /*
         * Rede 1 Opcional 
         * Data Fiscal 147 Opcional 
         * Hora Fiscal 148 Opcional 
         * Cupom Fiscal 149 Opcional 
         * Código de Cliente 8 Opcional 
         * Operador 150 Opcional 
         * Supervisor 151 Opcional 
         * Número do Cartão 4 Obrigatório 
         * Data de Vencimento 5 Obrigatório 
         * Valor 7 Obrigatório 
         * Valor Taxa Serviço 187 Opcional 
         * Código de Segurança 6 Deve ser informado de acordo com o resultado da consulta cartão. 
         * RG 161 Opcional 
         */
        public PreAutorizacaoRequest(Rede network, DataFiscal date, HoraFiscal time, NumeroDoCartao cartao, DataDeVencimento expiration, Valor value, ValorTaxaDeServico serviceTax, CodigoDeSeguranca securityCode)
            : base(network, date, time, cartao, expiration, value, serviceTax, securityCode) { }
    }
}
