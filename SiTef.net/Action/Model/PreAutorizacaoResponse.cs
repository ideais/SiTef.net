using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class PreAutorizacaoResponse : AbstractActionModel
    {

        public PreAutorizacaoResponse(Terminal terminal)
        {
            rede = new Rede(terminal);
            confirmacao = new DadosDeConfirmacao(terminal);
            respostaSiTef = new CodigoDeRespostaSiTef(terminal);
            textoExibicao = new TextoParaExibicao(terminal);
            respostaInstituicao = new CodigoRespostaInstituicao(terminal);
            data = new Data(terminal);
            hora = new Hora(terminal);
            nsuHost = new NSUHost(terminal);
            codigoEstabelecimento = new CodigoDoEstabelecimento(terminal);
            autorizacao = new NumeroAutorizacao(terminal);
            nomeInstituicao = new NomeDaInstituicao(terminal); //Nome da Institucao
            nsuSiTef = new NSUSiTef(terminal); //NSU SiTef
            cupom = new LinhasDeCupom(terminal); //Linhas de cupom
            expiracao = new DataExpiracao(terminal); //Data Expiracao REDECARD
            
            _fields = new List<Field>{
                rede, confirmacao, respostaSiTef, textoExibicao, respostaInstituicao, data, hora,
                nsuHost, codigoEstabelecimento, autorizacao, nomeInstituicao, nsuSiTef, cupom, expiracao
            };
        }

        public Rede rede { get; set; }

        public DadosDeConfirmacao confirmacao { get; set; }

        public CodigoDeRespostaSiTef respostaSiTef { get; set; }

        public TextoParaExibicao textoExibicao { get; set; }

        public CodigoRespostaInstituicao respostaInstituicao { get; set; }

        public Data data { get; set; }

        public Hora hora { get; set; }

        public NSUHost nsuHost { get; set; }

        public CodigoDoEstabelecimento codigoEstabelecimento { get; set; }

        public NumeroAutorizacao autorizacao { get; set; }

        public NomeDaInstituicao nomeInstituicao { get; set; }

        public NSUSiTef nsuSiTef { get; set; }

        public LinhasDeCupom cupom { get; set; }

        public DataExpiracao expiracao { get; set; }
    }
}
