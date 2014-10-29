using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class PreAutorizacaoResponse : AbstractActionModel, IActionResponse
    {

        public PreAutorizacaoResponse(ITerminal terminal)
        {
            Rede = new Type.Rede(terminal);
            DadosDeConfirmacao = new Type.DadosDeConfirmacao(terminal);
            CodigoDeRespostaSiTef = new Type.CodigoDeRespostaSiTef(terminal);
            TextoParaExibicao = new Type.TextoParaExibicao(terminal);
            CodigoRespostaInstituicao = new Type.CodigoRespostaInstituicao(terminal);
            Data = new Type.Data(terminal);
            Hora = new Type.Hora(terminal);
            NsuHost = new Type.NSUHost(terminal);
            CodigoDoEstabelecimento = new Type.CodigoDoEstabelecimento(terminal);
            NumeroAutorizacao = new Type.NumeroAutorizacao(terminal);
            NomeDaInstituicao = new Type.NomeDaInstituicao(terminal); //Nome da Institucao
            NSUSiTef = new Type.NSUSiTef(terminal); //NSU SiTef
            LinhasDeCupom = new Type.LinhasDeCupom(terminal); //Linhas de cupom
            DataExpiracao = new Type.DataExpiracao(terminal); //Data Expiracao REDECARD

            _fields = new List<Type.IField>{
                Rede, DadosDeConfirmacao, CodigoDeRespostaSiTef, TextoParaExibicao, CodigoRespostaInstituicao, Data, Hora,
                NsuHost, CodigoDoEstabelecimento, NumeroAutorizacao, NomeDaInstituicao, NSUSiTef, LinhasDeCupom, DataExpiracao
            };
        }

        public Type.Rede Rede { get; set; }

        public Type.DadosDeConfirmacao DadosDeConfirmacao { get; set; }

        public Type.CodigoDeRespostaSiTef CodigoDeRespostaSiTef { get; set; }

        public Type.TextoParaExibicao TextoParaExibicao { get; set; }

        public Type.CodigoRespostaInstituicao CodigoRespostaInstituicao { get; set; }

        public Type.Data Data { get; set; }

        public Type.Hora Hora { get; set; }

        public Type.NSUHost NsuHost { get; set; }

        public Type.CodigoDoEstabelecimento CodigoDoEstabelecimento { get; set; }

        public Type.NumeroAutorizacao NumeroAutorizacao { get; set; }

        public Type.NomeDaInstituicao NomeDaInstituicao { get; set; }

        public Type.NSUSiTef NSUSiTef { get; set; }

        public Type.LinhasDeCupom LinhasDeCupom { get; set; }

        public Type.DataExpiracao DataExpiracao { get; set; }

        public bool Failure()
        {
            return !CodigoDeRespostaSiTef.Approved();
        }


        public string Message()
        {
            return TextoParaExibicao.Value;
        }
    }
}
