using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class ConsultaCartaoResponse : AbstractActionModel, IActionResponse
    {
        public ConsultaCartaoResponse(ITerminal terminal)
        {
            Rede = new Type.Rede(terminal);
            CodigoDeRespostaSitef = new Type.CodigoDeRespostaSiTef(terminal);
            TextoParaExibicao = new Type.TextoParaExibicao(terminal);
            BandeiraDoCartao = new Type.BandeiraDoCartao(terminal);
            ValidaEmbosso = new Type.StringField(24, 1, terminal) { Label = "Valida Embosso" }; //Valida Embosso
            CodigoValidacao = new Type.StringField(25, 4, terminal); //Codigo Validacao
            TipoSenha = new Type.StringField(26, 1, terminal); //Tipo Senha
            TaxaServico = new Type.TaxaServico(terminal);
            NumMinParcelas = new Type.NumericField(28, 2, terminal); //No Min Parcela
            NumMaxParcelas = new Type.NumericField(29, 2, terminal); //No Max Parcela
            PercentualMaxTaxaServico = new Type.NumericField(30, 4, terminal); // Percentual Máximo da Taxa de Servico
            DataLimitePreDatado = new Type.DateField(31, terminal) { Pattern = "ddMMyyyy", Label = "Data Limite Pre Datado" }; // Data Limite Pre Datado
            DataLimitePrimeiraParcela = new Type.DateField(32, terminal) { Pattern = "ddMMyyyy", Label = "Data Limite 1a parcela" }; // Data Limite 1a parcela
            CapturaCodigoSeguranca = new Type.CapturaCodigoSeguranca(terminal);
            GarantiaPreDatado = new Type.ZeroOrOneField(34, terminal) { Label = "Garantia Pre-Datado" };
            TransacaoComChip = new Type.ZeroOrOneField(35, terminal) { Label = "Transacao com Chip" };
            VendaVista = new Type.ZeroOrOneField(36, terminal) { Label = "Venda a Vista" }; // Venda a Vista
            VendaParcelada = new Type.ZeroOrOneField(37, terminal) { Label = "Venda Parcelada" }; // Venda Parcelada
            VendaParceladaJurosAdministradora = new Type.ZeroOrOneField(38, terminal) { Label = "Venda Parcelada C/ Juros Administradora" };
            VendaProRataVista = new Type.ZeroOrOneField(39, terminal) { Label = "Venda Pro-Rata a Vista" };
            VendaProRataParcelada = new Type.ZeroOrOneField(40, terminal) { Label = "Venda Pro-Rata parcelada" };
            CancelamentoEstornoDeCaptura = new Type.ZeroOrOneField(41, terminal) { Label = "Cancelamento (tr.36h/ 37h) e Estorno de Captura de Pré-Autorização (tr. 12h)" };
            PreAutorizacao = new Type.ZeroOrOneField(42, terminal) { Label = "Pré-autorização" };
            ConsulaVendaParcelada = new Type.ZeroOrOneField(43, terminal) { Label = "Consulta venda Parcelada" };
            CancelamentoPreAutorizacao = new Type.ZeroOrOneField(44, terminal) { Label = "Cancelamento de Pre-Autorizacao" };
            CapturaPreAutorizacao = new Type.ZeroOrOneField(45, terminal) { Label = "Captura de Pre-Autorizacao" };
            ConsultaAVS = new Type.ZeroOrOneField(46, terminal) { Label = "Consulta AVS" };
            OpcoesVariaveisComPrefixo = new Type.StringField(155, 128, terminal) { Label = "Opcoes Variaveis com Prefixo" }; 
            /*
            new Field(163, 99, terminal);
            new Field(164, 99, terminal);
            new Field(165, 99, terminal);
            new Field(166, 99, terminal);
            new Field(167, 99, terminal);
            new Field(168, 99, terminal);
            new Field(169, 99, terminal);
            new Field(170, 99, terminal);
            new Field(171, 99, terminal);
            new Field(172, 99, terminal);
            new Field(173, 99, terminal);
            new Field(174, 99, terminal);
            new Field(175, 99, terminal);
            new Field(176, 99, terminal);
            new Field(177, 99, terminal);
            new Field(178, 99, terminal);
            new Field(179, 99, terminal);
            new Field(180, 99, terminal);
            new Field(181, 99, terminal);
            new Field(182, 99, terminal);
            new Field(236, 99, terminal);
            new Field(237, 99, terminal);
            new Field(239, 99, terminal);
            new Field(241, 99, terminal);
            new Field(242, 99, terminal);
            new Field(243, 99, terminal);
            new Field(244, 99, terminal);*/
            NumMaxParcelasLoja = new Type.NumericField(245, 2, terminal) { Label = "Numero Maximo de Parcelas Loja" };
            /*
            new Field(246, 99, terminal);
            new Field(350, 99, terminal);
            new Field(351, 99, terminal);
            new Field(352, 99, terminal);
            new Field(353, 99, terminal);
            new Field(354, 99, terminal);
            new Field(561, 99, terminal);
            new Field(562, 99, terminal);
            new Field(563, 99, terminal);
            new Field(564, 99, terminal);
            new Field(578, 99, terminal);
            new Field(579, 99, terminal);*/
            _fields = new List<Type.IField>{ 
                Rede, CodigoDeRespostaSitef, TextoParaExibicao, BandeiraDoCartao,
                ValidaEmbosso, CodigoValidacao, TipoSenha, TaxaServico, NumMinParcelas, NumMaxParcelas,
                PercentualMaxTaxaServico, DataLimitePreDatado, DataLimitePrimeiraParcela, CapturaCodigoSeguranca,
                GarantiaPreDatado, TransacaoComChip, VendaVista,VendaParcelada, VendaParceladaJurosAdministradora,
                VendaProRataVista,VendaProRataParcelada,CancelamentoEstornoDeCaptura, PreAutorizacao,
                ConsulaVendaParcelada, CancelamentoPreAutorizacao, CapturaPreAutorizacao, ConsultaAVS,
                OpcoesVariaveisComPrefixo, NumMaxParcelasLoja
            };
        }


        public Type.Rede Rede { get; set; }

        public Type.CodigoDeRespostaSiTef CodigoDeRespostaSitef { get; set; }

        public Type.TextoParaExibicao TextoParaExibicao { get; set; }

        public Type.BandeiraDoCartao BandeiraDoCartao { get; set; }

        public Type.StringField ValidaEmbosso { get; set; }

        public Type.StringField CodigoValidacao { get; set; }

        public Type.StringField TipoSenha { get; set; }

        public Type.TaxaServico TaxaServico { get; set; }

        public Type.NumericField NumMinParcelas { get; set; }

        public Type.NumericField NumMaxParcelas { get; set; }

        public Type.NumericField PercentualMaxTaxaServico { get; set; }

        public Type.DateField DataLimitePreDatado { get; set; }

        public Type.DateField DataLimitePrimeiraParcela { get; set; }

        public Type.CapturaCodigoSeguranca CapturaCodigoSeguranca { get; set; }

        public Type.ZeroOrOneField GarantiaPreDatado { get; set; }

        public Type.ZeroOrOneField TransacaoComChip { get; set; }

        public Type.ZeroOrOneField VendaVista { get; set; }

        public Type.ZeroOrOneField VendaParcelada { get; set; }

        public Type.ZeroOrOneField VendaParceladaJurosAdministradora { get; set; }

        public Type.ZeroOrOneField VendaProRataVista { get; set; }

        public Type.ZeroOrOneField VendaProRataParcelada { get; set; }

        public Type.ZeroOrOneField CancelamentoEstornoDeCaptura { get; set; }

        public Type.ZeroOrOneField PreAutorizacao { get; set; }

        public Type.ZeroOrOneField ConsulaVendaParcelada { get; set; }

        public Type.ZeroOrOneField CancelamentoPreAutorizacao { get; set; }

        public Type.ZeroOrOneField CapturaPreAutorizacao { get; set; }

        public Type.ZeroOrOneField ConsultaAVS { get; set; }

        public Type.StringField OpcoesVariaveisComPrefixo { get; set; }

        public Type.NumericField NumMaxParcelasLoja { get; set; }

        public bool Failure()
        {
            return !CodigoDeRespostaSitef.Approved();
        }


        public string Message()
        {
            return TextoParaExibicao.Value;
        }
    }
}
