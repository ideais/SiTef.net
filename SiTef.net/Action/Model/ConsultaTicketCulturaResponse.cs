using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class ConsultaTicketCulturaResponse : AbstractActionModel, IActionResponse
    {

        public ConsultaTicketCulturaResponse(ITerminal terminal)
        {

            Rede = new Type.Rede(terminal);
            Vencimento = new Type.DataDeVencimento(terminal);
            CodRespostaSitef = new Type.CodigoDeRespostaSiTef(terminal);
            TextoExibicao = new Type.TextoParaExibicao(terminal);
            BandeiraCartao = new Type.BandeiraDoCartao(terminal);
            ValidaEmbosso = new Type.ValidaEmbosso(terminal);
            CodigoValidacao = new Type.CodigoValidacao(terminal);
            TipoSenha = new Type.TipoSenha(terminal);
            TaxaServico = new Type.TaxaServico(terminal);
            NumMinParcela = new Type.NumMinParcela(terminal);
            NumMaxParcela = new Type.NumMaxParcela(terminal);
            PercentualMaxTaxaServico = new Type.PercentualMaxTaxaServico(terminal);
            DataLimPreDatado = new Type.DataLimPreDatado(terminal);
            DataLimPrimeiraParcela = new Type.DataLimPrimeiraParcela(terminal);
            CapturaCodigoSeguranca = new Type.CapturaCodigoSeguranca(terminal);
            GarantiaPreDatado = new Type.GarantiaPreDatado(terminal);
            TransacaoComChip = new Type.TransacaoComChip(terminal);
            CodigoDoRoteamento = new Type.CodigoDoRoteamento(terminal);
            CodigoDoProduto = new Type.CodigoDoProduto(terminal);
            DescricaoDoProduto = new Type.DescricaoDoProduto(terminal);
            NomeDaEmpresa = new Type.NomeDaEmpresa(terminal);
            NomeDoPortador = new Type.NomeDoPortador(terminal);
            AutorizaSaldoDisponivel = new Type.AutorizaSaldoDisponivel(terminal);

            LinhasCreditoValeCultura = new List<Type.LinhaCreditoValeCultura>();
            var linha = new Type.LinhaCreditoValeCultura(terminal);
            LinhasCreditoValeCultura.Add(linha);
            while (terminal.ExistemMaisElementos(Type.CodigoLinhaDeCredito.ID))
            {
                linha = new Type.LinhaCreditoValeCultura(terminal);
                LinhasCreditoValeCultura.Add(linha);
            }

            _fields = new List<Type.IField> { Rede, Vencimento, CodRespostaSitef, TextoExibicao, BandeiraCartao, ValidaEmbosso, CodigoValidacao, TipoSenha, TaxaServico,
            NumMinParcela, NumMaxParcela, PercentualMaxTaxaServico, DataLimPreDatado, DataLimPrimeiraParcela, CapturaCodigoSeguranca, GarantiaPreDatado, TransacaoComChip,
            CodigoDoRoteamento, CodigoDoProduto, DescricaoDoProduto, NomeDaEmpresa, NomeDoPortador, AutorizaSaldoDisponivel};

            foreach (var l in LinhasCreditoValeCultura)
                _fields.Add(l);

        }

        public bool Failure()
        {
            throw new NotImplementedException();
        }

        public string Message()
        {
            return TextoExibicao.Value;
        }


        public List<Type.LinhaCreditoValeCultura> LinhasCreditoValeCultura { get; set; }

        public Type.Rede Rede { get; set; }

        public Type.DataDeVencimento Vencimento { get; set; }

        public Type.CodigoDeRespostaSiTef CodRespostaSitef { get; set; }

        public Type.TextoParaExibicao TextoExibicao { get; set; }

        public Type.BandeiraDoCartao BandeiraCartao { get; set; }

        public Type.ValidaEmbosso ValidaEmbosso { get; set; }

        public Type.CodigoValidacao CodigoValidacao { get; set; }

        public Type.TipoSenha TipoSenha { get; set; }

        public Type.TaxaServico TaxaServico { get; set; }

        public Type.NumMinParcela NumMinParcela { get; set; }

        public Type.NumMaxParcela NumMaxParcela { get; set; }

        public Type.PercentualMaxTaxaServico PercentualMaxTaxaServico { get; set; }

        public Type.DataLimPreDatado DataLimPreDatado { get; set; }

        public Type.DataLimPrimeiraParcela DataLimPrimeiraParcela { get; set; }

        public Type.CapturaCodigoSeguranca CapturaCodigoSeguranca { get; set; }

        public Type.GarantiaPreDatado GarantiaPreDatado { get; set; }

        public Type.TransacaoComChip TransacaoComChip { get; set; }

        public Type.CodigoDoProduto CodigoDoProduto { get; set; }

        public Type.DescricaoDoProduto DescricaoDoProduto { get; set; }

        public Type.NomeDaEmpresa NomeDaEmpresa { get; set; }

        public Type.NomeDoPortador NomeDoPortador { get; set; }

        public Type.AutorizaSaldoDisponivel AutorizaSaldoDisponivel { get; set; }

        public Type.CodigoDoRoteamento CodigoDoRoteamento { get; set; }
    }
}
