using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class PreAutorizacaoResponse : AbstractActionModel
    {
        public PreAutorizacaoResponse(Terminal terminal) {
            _fields = new List<Field>{
                new Rede(terminal),
                new DadosDeConfirmacao(terminal),
                new CodigoDeRespostaSiTef(terminal),
                new TextoParaExibicao(terminal),
                new Field(12,99,terminal), //Codigo de Resposta HOST
                new Data(terminal),
                new Field(14,4,terminal), //Hora
                new Field(15,99,terminal), //NSU_Host
                new Field(16,99,terminal), //Codigo do estabelecimento
                new Field(17,99,terminal), //Numero de Autorizacao
                new Field(21,99,terminal), //Nome da Institucao
                new Field(22,99,terminal), //NSU SiTef
                new Field(76,99,terminal), //Linhas de cupom
                new Field(216,4,terminal), //Data Expiracao REDECARD
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
                 * Data da Transação 217 Obrigatório (DDMMAAAA) 
                 * Número Autorização 17 Obrigatório 
                 * Nsu do HOST 15 Obrigatório Valor Taxa Serviço 187 Opcional Tipo de Financiamento 3 Obrigatório Numero de Parcelas 2 Obrigatório, se Tipo de Financiamento for igual a ‘2’ ou ‘3’. Código de Segurança 6 Deve ser informado de acordo com o resultado da consulta cartão. Data da Emissão do Cartão 218 Opcional (Usado pelo IBI) Ciclos 219 Opcional (Usado pelo IBI) RG 161 Opcional  
                 * 
                 * 
                 */
            };
        }
    }
}
