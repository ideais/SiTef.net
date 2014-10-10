
using System;
namespace SiTef.net.Action
{
    /// <summary>
    /// Relação dos códigos de Ações
    /// </summary>
    public class ActionType
    {
        public const int CONSULTA_CARTAO = 1;
        public const int VENDA_SITEF = 2;
        public const int CANCELA_SITEF = 3;
        public const int FINALIZA_TRANSACAO = 4;
        public const int CONSULTA_PARAMETROS_G1_CB = 5;
        public const int CONSULTA_CODIGO_BARRAS_CB = 6;
        public const int CONSULTA_CHEQUE_CB = 7;
        public const int CONSULTA_PAGAMENTO_CB = 8;
        public const int PAGAMENTO_CB = 9;
        public const int CONTINUA_PAGAMENTO_CB = 10;
        public const int ESTORNO_PAGAMENTO_CB = 11;
        public const int CONSULTA_PARAMETROS_G3_CB = 12;
        public const int CONSULTA_DEPOSITO_CB = 13;
        public const int DEPOSITO_CB = 14;
        public const int ESTORNO_DEPOSITO_CB = 15;
        public const int CONSULTA_SALDO_CB = 16;
        public const int SAQUE_CB = 17;

        /*
         * 18 ESTORNO_SAQUE_CB 
         * 19 CONSULTA_AVS 
         * 20 CANCELAMENTO_IDENTIFICADO 
         * 21 INCLUSAO_PEDIDO_SONDA 
         * 22 CONSULTA_CHEQUE_GENERICA 
         * 23 RC_CONSULTA_CONCESSIONARIA 
         * 24 RC_CONSULTA_VALORES 
         * 25 RC_RECARGA 
         * 26 CONSULTA_PARAMETROS_G4 
         * 27 PAGAMENTO_DARF 
         * 28 PAGAMENTO_DARF_SIMPLES 
         * 29 PAGAMENTO_GPS 
         * 30 ESTORNO_PAGAMENTO_G4 
         */

        public const int PRE_AUTORIZACAO = 31;
        public const int ESTORNO_PRE_AUTORIZACAO = 32;
        public const int CAPTURA_PRE_AUTORIZACAO = 33;

        /* 34 AJUSTA_VENDA 
         * 35 FECHAMENTO_LOTE 
         * 36 RC_BRADESCO_CONSULTA_FILIAIS 
         * 37 RC_BRADESCO_CONSULTA_VALORES 
         * 38 RC_BRADESCO_RECARGA_DESVINCULADA
         * 39 RC_BRADESCO_RECARGA_TEF_AUTORIZADOR 
         * 40 CONSULTA_EXTRATO_CB
         * 41 CONSULTA_EMPRESTIMO_CB 
         * 42 SIMULA_EMPRESTIMO_CB 
         * 43 EMPRESTIMO_CB 
         * 44 CONSULTA_PARAMETROS_G5_CB 
         * 45 CONSULTA_DADOS_CADASTRAIS_CB 
         * 46 ABERTURA_CONTA_CB 
         * 47 VENDA_PAGGO 
         * 48 CANCELAMENTO_PAGGO 
         * 49 VENDA_WAPPA 
         * 50 CANCELAMENTO_WAPPA 
         * 51 VENDA_DEBITO (Não disponível)
         * 52 CANCELAMENTO_DEBITO 
         * 53 FINALIZA_TRN_RETAGUARDA 
         * 54 CONSULTA_CANC_VISA 
         * 55 CANCELAMENTO_VISA_LOG 
         * 56 CANCELAMENTO_VISA_HOST 
         * 57 CONSULTA_PARAMETROS_G6_CB 
         * 58 CONSULTA_SEGMENTO_PAGUE_FACIL_CB 
         * 59 CONSULTA_UF_PAGUE_FACIL_CB 
         * 60 CONSULTA_EMPRESA_PAGUE_FACIL_CB 
         * 61 CONSULTA_PRODUTO_PAGUE_FACIL_CB 
         * 62 CONSULTA_ENTRADA_PAGUE_FACIL_CB 
         * 63 CONSULTA_VALORES_PAGUE_FACIL_CB 
         * 64 EFETUA_TRANSACAO_PAGUE_FACIL_CB 
         * 65 CANCELAMENTO_PAGAMENTO_FATURA 
         * 66 INCLUSAO_VENDA 
         * 67 CONSULTA_BOLETO 
         * 68 CONSULTA_AUTORIZADORES_PJ 
         * 69 CONSULTA_PUBLISHER_PJ 
         * 70 VENDA_PJ 
         * 71 CONSULTA_TRANSFERENCIA_CB 
         * 72 TRANSFERENCIA_ENTRE_CONTAS_CB 
         * 73 CONSULTA_DESBLOQUEIO_CHEQUE_CB 
         * 74 DESBLOQUEIO_CHEQUE_CB 
         * 75 REVALIDACAO_SENHA_CARTAO_INSS_CB 
         * 76 CONSULTA_DEPOSITO_IDENT_CB 
         * 77 DEPOSITO_IDENTIFICADO_CB 
         * 78 EMISSAO_PONTOS_GIVEX 
         * 79 RECARGA_GIVEX 
         * 80 CANCELA_RECARGA_GIVEX 
         * 81 CANCELA_EMISSAO_GIVEX 
         * 82 CONSULTA_SALDO_GIVEX 
         * 83 RESGATE_PONTOS_GIVEX 
         * 84 CONSULTA_CESTA_SERVICOS 
         * 85 ALTERA_CESTA_SERVICOS 
         * 86 CONSULTA_BENEFICIOS_INSS 
         * 87 RC_CONSULTA_CONCESSIONARIA_NACIONAL_V5 
         * 88 RC_CONSULTA_DADOS_FILIAL_NACIONAL_V5 
         * 89 RC_RECARGA_NACIONAL_V5 
         * 90 RC_CONSULTA_CONCESSIONARIA_OUTROS_V5 
         * 91 RC_CONSULTA_DADOS_FILIAL_OUTROS_V5 
         * 92 RC_RECARGA_OUTROS_V5 
         * 93 RC_RECARGA_NACIONAL_MISTA_V5 
         * 94 CONSULTA_DATAS_DEMONSTRATIVO_CB 
         * 95 PEDIDO_2AVIA_CARTAO_DEBITO_CB 
         * 96 CONSULTA_DEMOSNTRATIVO_CRED_BENEFICIOS_CB 
         * 97 TRANSACAO_GENERICA 
         * 98 VENDA_DEBITO_ECOMMERCE 
         * 99 CONSULTA_DADOS_INSS 
         * 100 CONSULTA_SALDO_CARTAO_GIFT 
         * 101 RECARGA_CARTAO_GIFT 
         * 102 VENDA_CARTAO_GIFT 
         * 103 CANCELAMENTO_VENDA_CARTAO_GIFT 
         * 104 Reservado 
         * 105 Reservado 
         * 106 Reservado 
         * 107 Reservado 
         * 108 CONSULTA_UF_LICENCIAMENTO_CB 
         * 109 CONSULTA_SERVICOS_UF_CB 
         * 110 PAGAMENTO_DEBITO_VEICULO_CB 
         * 111 CONSULTA_FORNECEDORES_VOUCHER 
         * 112 CONSULTA_PRODUTOS_VOUCHER 
         * 113 VENDA_VOUCHER 
         * 114 CANCELAMENTO_VOUCHER 
         * 115 Reservado 
         * 116 Reservado 
         * 117 Reservado 
         * 118 Reservado 
         * 119 Reservado 
         * 120 Reservado 
         * 121 VPF_CRIACAO_TICKET 
         * 122 VPF_CANCELAMENTO_TICKET 
         * 123 VPF_ENVIO_ENTRADAS 
         */
    }
}
