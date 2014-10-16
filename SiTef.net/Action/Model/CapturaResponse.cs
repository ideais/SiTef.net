using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class CapturaResponse : AbstractActionModel
    {

        public CapturaResponse(Terminal terminal)
        {
            // Rede 1
            rede = new Rede(terminal);
            //* Dados de confirmação 9 
            confirmacao = new DadosDeConfirmacao(terminal);
            //* Código de Resposta do SITEF 10 
            codigoSitef = new CodigoDeRespostaSiTef(terminal);
            //* Texto para exibição 11 
            textoExibicao = new TextoParaExibicao(terminal);
            //* Código de resposta HOST 12
            codigoInstituicao = new CodigoRespostaInstituicao(terminal);
            //* Data 13
            data = new Data(terminal);
            //* Hora 14 
            hora = new Hora(terminal);
            //* NSU_Host 15
            nsuHost = new NSUHost(terminal);
            //* Código Estabelecimento 16 
            codigoEstabelecimento = new CodigoDoEstabelecimento(terminal);
            //* Número Autorização 17 
            numeroAutorizacao = new NumeroAutorizacao(terminal);
            //* Nome da Instituição 21 
            instituicao = new NomeDaInstituicao(terminal);
            //* Nsu do SiTef 22
            nsuSiTef = new NSUSiTef(terminal);
            //* Linhas de cupom 76
            cupom = new LinhasDeCupom(terminal);

            _fields = new List<IField>{
                rede,confirmacao,codigoSitef,data,hora,nsuHost,
                codigoEstabelecimento, numeroAutorizacao,instituicao,nsuSiTef,cupom
            };
        }

        public Rede rede { get; set; }
        public DadosDeConfirmacao confirmacao { get; set; }
        public TextoParaExibicao textoExibicao { get; set; }
        public CodigoRespostaInstituicao codigoInstituicao { get; set; }
        public CodigoDeRespostaSiTef codigoSitef { get; set; }
        public Data data { get; set; }
        public Hora hora { get; set; }
        public NSUHost nsuHost { get; set; }
        public CodigoDoEstabelecimento codigoEstabelecimento { get; set; }
        public NumeroAutorizacao numeroAutorizacao { get; set; }
        public NomeDaInstituicao instituicao { get; set; }
        public NSUSiTef nsuSiTef { get; set; }
        public LinhasDeCupom cupom { get; set; }
    }
}
