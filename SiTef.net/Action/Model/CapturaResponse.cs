using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class CapturaResponse : AbstractActionModel , IActionResponse
    {

        public CapturaResponse(Terminal terminal)
        {
            // Rede 1
            Rede = new Type.Rede(terminal);
            //* Dados de confirmação 9 
            DadosDeConfirmacao = new Type.DadosDeConfirmacao(terminal);
            //* Código de Resposta do SITEF 10 
            CodigoDeRespostaSiTef = new Type.CodigoDeRespostaSiTef(terminal);
            //* Texto para exibição 11 
            TextoParaExibicao = new Type.TextoParaExibicao(terminal);
            //* Código de resposta HOST 12
            CodigoRespostaInstituicao = new Type.CodigoRespostaInstituicao(terminal);
            //* Data 13
            Data = new Type.Data(terminal);
            //* Hora 14 
            Hora = new Type.Hora(terminal);
            //* NSU_Host 15
            NSUHost = new Type.NSUHost(terminal);
            //* Código Estabelecimento 16 
            CodigoDoEstabelecimento = new Type.CodigoDoEstabelecimento(terminal);
            //* Número Autorização 17 
            NumeroAutorizacao = new Type.NumeroAutorizacao(terminal);
            //* Nome da Instituição 21 
            NomeDaInstituicao = new Type.NomeDaInstituicao(terminal);
            //* Nsu do SiTef 22
            NSUSiTef = new Type.NSUSiTef(terminal);
            //* Linhas de cupom 76
            LinhasDeCupom = new Type.LinhasDeCupom(terminal);

            _fields = new List<Type.IField>{
                Rede, DadosDeConfirmacao, CodigoDeRespostaSiTef, TextoParaExibicao, CodigoDeRespostaSiTef,
                Data, Hora, NSUHost, CodigoDoEstabelecimento, NumeroAutorizacao, NomeDaInstituicao,
                NSUSiTef, LinhasDeCupom
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

        public Type.NomeDaInstituicao NomeDaInstituicao { get; set; }

        public Type.NSUSiTef NSUSiTef { get; set; }

        public Type.LinhasDeCupom LinhasDeCupom { get; set; }


        public string Message()
        {
            return TextoParaExibicao.Value;
        }
    }
}
