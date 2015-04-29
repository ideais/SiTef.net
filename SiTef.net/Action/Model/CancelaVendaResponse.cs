using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class CancelaVendaResponse : AbstractActionModel, IActionResponse
    {
        public CancelaVendaResponse(ITerminal terminal)
        {

            this.Rede = new Type.Rede(terminal);
            this.DadosDeConfirmacao = new Type.DadosDeConfirmacao(terminal);
            this.CodigoDeReposta = new Type.CodigoDeRespostaSiTef(terminal);
            this.Data = new Type.Data(terminal);
            this.Hora = new Type.Hora(terminal);
            this.NSUHost = new Type.NSUHost(terminal);
            this.CodigoDoEstabelecimento = new Type.CodigoDoEstabelecimento(terminal);
            this.NumeroAutorizacao = new Type.NumeroAutorizacao(terminal);
            this.DocumentoCancelado = new Type.DocumentoCancelado(terminal);
            this.DataDoCancelamento = new Type.DataDaCompraCancelada(terminal);
            this.HoraDoCancelamento = new Type.HoraDaCompraCancelada(terminal);
            this.NomeDaInstituicao = new Type.NomeDaInstituicao(terminal);
            this.NSUSiTef = new Type.NSUSiTef(terminal);
            this.LinhasCupom = new Type.LinhasDeCupom(terminal);
            this.LinhasCupomEstabelecimento = new Type.LinhasDeCupomEstabelecimento(terminal);
            this.TextoParaExibicao = new Type.TextoParaExibicaoVisorCliente(terminal);

            Fields = new List<Type.IField> { 
                Rede, DadosDeConfirmacao, CodigoDeReposta, Data, Hora, NSUHost,
                CodigoDoEstabelecimento, NumeroAutorizacao, DocumentoCancelado, DataDoCancelamento,
                HoraDoCancelamento, NomeDaInstituicao, NSUSiTef, LinhasCupom, LinhasCupomEstabelecimento,
                TextoParaExibicao
            };

        }

        public bool Failure()
        {
            return !CodigoDeReposta.Approved();
        }

        public string Message()
        {
            return TextoParaExibicao.Value;
        }

        public Type.Rede Rede { get; set; }

        public Type.DadosDeConfirmacao DadosDeConfirmacao { get; set; }

        public Type.CodigoDeRespostaSiTef CodigoDeReposta { get; set; }

        public Type.Data Data { get; set; }

        public Type.Hora Hora { get; set; }

        public Type.NSUHost NSUHost { get; set; }

        public Type.CodigoDoEstabelecimento CodigoDoEstabelecimento { get; set; }

        public Type.NumeroAutorizacao NumeroAutorizacao { get; set; }

        public Type.DocumentoCancelado DocumentoCancelado { get; set; }

        public Type.DataDaCompraCancelada DataDoCancelamento { get; set; }

        public Type.HoraDaCompraCancelada HoraDoCancelamento { get; set; }

        public Type.NomeDaInstituicao NomeDaInstituicao { get; set; }

        public Type.NSUSiTef NSUSiTef { get; set; }

        public Type.LinhasDeCupom LinhasCupom { get; set; }

        public Type.LinhasDeCupomEstabelecimento LinhasCupomEstabelecimento { get; set; }

        public Type.TextoParaExibicaoVisorCliente TextoParaExibicao { get; set; }
    }
}
