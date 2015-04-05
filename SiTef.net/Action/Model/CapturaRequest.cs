using SiTef.net.Type;

namespace SiTef.net.Action.Model
{
    public class CapturaRequest : AbstractActionModel
    {
        /*
         * Rede 1 Opcional 
         * Data Fiscal 147 Opcional 
         * Hora Fiscal 148 Opcional 
         * Cupom Fiscal 149 Opcional 
         * Código de Cliente 8 Opcional 
         * Operador 150 Opcional 
         * Supervisor 151 Opcional 
         * 
         * Número do Cartão 4 Obrigatório 
         * 
         * Data de Vencimento 5 Obrigatório 
         * 
         * Valor 7 Obrigatório 
         * 
         * Data da Transação 217 Obrigatório (DDMMAAAA) 
         * 
         * Número Autorização 17 Obrigatório 
         * 
         * Nsu do HOST 15 Obrigatório 
         * 
         * Valor Taxa Serviço 187 Opcional 
         * 
         * Tipo de Financiamento 3 Obrigatório 
         * 
         * Numero de Parcelas 2 Obrigatório, se Tipo de Financiamento for igual a ‘2’ ou ‘3’.
         * 
         * Código de Segurança 6 Deve ser informado de acordo com o resultado da consulta cartão.
         * Data da Emissão do Cartão 218 Opcional (Usado pelo IBI)
         * Ciclos 219 Opcional (Usado pelo IBI)
         * RG 161 Opcional  
         */
        public CapturaRequest(
            NumeroDoCartao cartao,
            DataDeVencimento vencimento,
            Valor valor,
            DataDaTransacao data,
            NumeroAutorizacao autorizacao,
            NSUHost nsuHost,
            ValorTaxaDeServico taxa,
            TipoDeFinanciamento financiamento,
            NumeroDeParcelas parcelas,
            CodigoDeSeguranca codigoSeguranca
                )
            : base(cartao, vencimento, valor, data, autorizacao, nsuHost, taxa, financiamento, parcelas, codigoSeguranca) { }
    }
}
