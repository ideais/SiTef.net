using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Resposta de uma requisição de Venda à Crédito
    /// </summary>
    public class VendaResponse : AbstractActionModel, IActionResponse
    {
        public VendaResponse(ITerminal terminal)
        {
                Rede = new Type.Rede(terminal);
                DadosDeConfirmacao = new Type.DadosDeConfirmacao(terminal);
                CodigoDeRespostaSiTef = new Type.CodigoDeRespostaSiTef(terminal);
                TextoParaExibicao = new Type.TextoParaExibicao(terminal);
                CodigoRespostaInstituicao = new Type.CodigoRespostaInstituicao(terminal);
                Data = new Type.Data(terminal);
                Hora = new Type.Hora(terminal);
                NSUHost = new Type.NSUHost(terminal);
                CodigoDoEstabelecimento = new Type.CodigoDoEstabelecimento(terminal);
                NumeroAutorizacao = new Type.NumeroAutorizacao(terminal);
                NSUSiTef = new Type.NSUSiTef(terminal);
                LinhasDeCupom = new Type.LinhasDeCupom(terminal);
                LinhasDeCupomEstabelecimento = new Type.LinhasDeCupomEstabelecimento(terminal);
                TextoExibicaoVisorCliente = new Type.StringField(409, 64, terminal) { Label = "Texto para Exibição no Visor do Cliente" };
            
            _fields = new List<Type.IField>
            {
                Rede, DadosDeConfirmacao, CodigoDeRespostaSiTef, TextoParaExibicao, CodigoRespostaInstituicao,
                Data, Hora, NSUHost, CodigoDoEstabelecimento, NumeroAutorizacao, NSUSiTef, LinhasDeCupom,
                LinhasDeCupomEstabelecimento, TextoExibicaoVisorCliente
            };
        }

        public bool Failure()
        {
            return !CodigoDeRespostaSiTef.Approved();
        }

        public Type.Rede Rede { get; set; }

        public Type.DadosDeConfirmacao DadosDeConfirmacao { get; set; }

        public Type.CodigoDeRespostaSiTef CodigoDeRespostaSiTef { get; set; }

        public Type.TextoParaExibicao TextoParaExibicao { get; set; }

        public Type.CodigoRespostaInstituicao CodigoRespostaInstituicao { get; set; }

        public Type.Data Data { get; set; }

        public Type.Hora Hora { get; set; }

        public Type.NSUHost NSUHost { get; set; }

        public Type.CodigoDoEstabelecimento CodigoDoEstabelecimento { get; set; }

        public Type.NumeroAutorizacao NumeroAutorizacao { get; set; }

        public Type.NSUSiTef NSUSiTef { get; set; }

        public Type.LinhasDeCupom LinhasDeCupom { get; set; }

        public Type.LinhasDeCupomEstabelecimento LinhasDeCupomEstabelecimento { get; set; }

        public Type.StringField TextoExibicaoVisorCliente { get; set; }


        public string Message()
        {
            return TextoParaExibicao.Value;
        }
    }
}
