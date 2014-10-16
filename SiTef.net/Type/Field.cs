using SiTef.net.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SiTef.net.Type
{

    public class Field
    {
        protected short _id;
        public int Id { get { return _id; } }
        protected string _value;
        public string Value
        {
            get { return _value; }
        }

        public Field(short id, int length, Terminal terminal)
        {
            _id = id;
            try
            {
                _value = terminal.LeCampo(id, length);
            }
            catch (TerminalException ex)
            {
                //TODO LOG
            }

        }


        protected Field(short id, string value, short length, string pattern)
        {
            if (value == null)
                throw new ArgumentNullException();

            if (value.Length == 0)
                throw new ArgumentException("empty", "value");

            if (length > 0 && value.Length > length)
                throw new ArgumentException(String.Format("{0} exceeds the maximum length: {1}", value.Length, length), "value");

            if (pattern != null && !Regex.IsMatch(value, pattern))
                throw new ArgumentException("format not valid");

            _id = id;
            _value = value;
        }

        public static Field InstanceOf(short id, string value)
        {
            switch (id)
            {
                default:
                    return new Field(id, value, 0, null);
            }
        }

        public override string ToString()
        {
            if (_value != null)
                return String.Format("{2}({0})\n{1}", _id,  _value, this.GetType().Name );
            return "null";
        }
    }

    /// <summary>
    /// Implementação base para todos os campos numéricos
    /// </summary>
    public class NumericField : Field
    {
        protected const string PATTERN = @"^\d*$";

        public NumericField(short id, short length, Terminal terminal) : base(id, length, terminal) { }
        public NumericField(short id, int value, short length) : base(id, value.ToString(), length, PATTERN) { }
        public NumericField(short id, string value, short length) : base(id, value, length, PATTERN) { }

    }

    /// <summary>
    /// Implementação base para todos os campos de data
    /// </summary>
    public class DateField : Field
    {

        protected const string PATTERN = @"^[0-3]\d[0-1]\d\d{4,4}$";
        protected static short LENGTH = 8;
        private static string FORMAT = "ddMMyyyy";
        private short ID;
        private Terminal terminal;

        public DateField(short id, DateTime data) : base(id, data.ToString(FORMAT), LENGTH, PATTERN) { }

        public DateField(short id, String data) : base(id, data, LENGTH, PATTERN) { }

        public DateField(short id, Terminal terminal) : base(id, LENGTH, terminal) { }


        public DateTime? ToDateTime()
        {
            if (_value != null)
                return DateTime.ParseExact(_value, FORMAT, null);
            return null;
        }
    }

    /// <summary>
    /// Código da rede de destino.
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos. 
    /// </summary>
    public class Rede : Field
    {
        public static short LENGTH = 4;
        public Rede(Terminal terminal) : base(1, LENGTH, terminal) { }
        public Rede(string codigo) : base(1, codigo, LENGTH, @"^\s*\d*$") { }
        
        public static Rede TECHAN = new Rede("1");
        public static Rede REDE = new Rede("5");
        public static Rede AMEX = new Rede("6");
        public static Rede DINNERS = new Rede("5");
        public static Rede SERASA = new Rede("9");
        public static Rede BANRISUL = new Rede("21");
        public static Rede EDENRED = new Rede("41");
        public static Rede HIPER = new Rede("51");
        public static Rede CETELEN = new Rede("55");
        public static Rede GWCEL = new Rede("106");
        public static Rede CIELO = new Rede("125");
        public static Rede CSU = new Rede("170");
        public static Rede TSYS = new Rede("190");
        public static Rede HUG = new Rede("218");
        public static Rede CIAGROUP = new Rede("234");
        public static Rede CORRESPONDENTE_BANCARIO_BRADESCO = new Rede("805");
    }

    /// <summary>
    /// Número de parcelas da compra, quando a vista preencher com ’1’
    /// </summary>
    public class NumeroDeParcelas : Field
    {
        public static short LENGTH = 2;
        public NumeroDeParcelas(Terminal terminal) : base(2, LENGTH, terminal) { }
        public NumeroDeParcelas(string parcelas) : base(2, parcelas, LENGTH, @"^\d*$") { }
    }

    /// <summary>
    /// ‘2’ – se pagamento pré-datado 
    /// ‘3’ – se parcelamento financiado pela administradora 
    /// ‘4’ – se parcelamento financiado pelo estabelecimento  
    /// ‘5´ - se for parcelamento pró rata, utilizado somente em alguns cartões de redes especificas 
    /// ‘6’ - para compra IATA parcelada com juros 
    /// ‘7’ - para compra IATA parcelada sem juros 
    /// ‘8’ – se parcelamento diferenciado 
    /// ‘11´ - compra com pontos 
    /// ‘21’ – compra com milhas 
    /// ‘22’ – compra com cupons  
    /// 
    /// Para transações de pré-autorização: 
    /// ‘1’ – Crédito à vista 
    /// ‘2’ – Crédito parcelado pela administradora 
    /// ‘3’ – Crédito parcelado pelo lojista 
    /// </summary>
    public class TipoDeFinanciamento : Field
    {
        public TipoDeFinanciamento(string tipo) : base(3, tipo, 2, @"^\s*\d*$") { }

        //TODO: Criar as instancias estaticas aí em cima
    }

    /// <summary>
    /// Número do cartão de crédito. Para envio de número de cartão criptografado, incluir o prefixo “CRIPSITEF:” antes do cartão.
    /// </summary>
    public class NumeroDoCartao : Field
    {
        public NumeroDoCartao(string numero) : base(4, Regex.Replace(numero,"[^0-9]",""), 30, @"^\d{0,30}$") { }
    }

    /// <summary>
    /// Data de vencimento do cartão MMAA (MM=mês AA=ano) 
    /// </summary>
    public class DataDeVencimento : Field
    {
        public DataDeVencimento(string numero) : base(5, numero, 4, @"^[0-1]\d{3}$") { }
    }

    /// <summary>
    /// Indica o número do código de segurança (CVV2-Visanet, CVC- Redecard Alfa Code-Amex) grafado no verso do cartão.
    /// Informar o Valor ‘0’ se não existir ou ‘1’ se estiver ilegível. 
    /// </summary>
    public class CodigoDeSeguranca : Field
    {
        public CodigoDeSeguranca(string codigo) : base(6, codigo, 5, @"^\d*$") { }
    }

    /// <summary>
    /// Valor da Compra deve ser enviado com duas casas decimais, porém sem a virgula.
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class Valor : Field
    {
        public Valor(string quantia) : base(7, quantia, 12, @"^\d*$") { }
    }

    /// <summary>
    /// Código do cliente, campo administrativo utilizado pelo SITEFWEB
    /// </summary>
    public class CodigoDoCliente : Field
    {
        public CodigoDoCliente(string codigo) : base(8, codigo, 12, null) { }
    }

    /// <summary>
    /// Dados que devem ser utilizados para confirmação da transação. 
    /// </summary>
    public class DadosDeConfirmacao : Field
    {
        public static short ID = 9;
        public DadosDeConfirmacao(Terminal terminal) : base(ID, 128, terminal) { }
    }

    /// <summary>
    /// Resultado da transação, quando ‘000’ indica que a transação foi aprovada ou Nada Consta para Consulta ACSP. 
    /// </summary>
    public class CodigoDeRespostaSiTef : Field
    {
        public static short ID = 10;
        public CodigoDeRespostaSiTef(Terminal terminal) : base(ID, 3, terminal) { }
        public bool Approved()
        {
            return "000".Equals(_value);
        }
    }

    /// <summary>
    /// Texto retornado pelo SITEF para exibição no terminal.  
    /// Este campo será repetido tantas vezes quanto for o número de linhas a serem exibidas
    /// </summary>
    public class TextoParaExibicao : Field
    {
        public static short ID = 11;
        public static short LENGTH = 64;
        public TextoParaExibicao(Terminal terminal)
            : base(ID, LENGTH, terminal)
        {
            while (terminal.ExistemMaisElementos(ID))
                _value += String.Format("\n{0}", terminal.LeCampo(ID, LENGTH));
        }
    }

    /// <summary>
    /// Código retornado pela Instituição ou pelo SiTef.
    /// Caso retorne ‘SC’ deve- se solicitar aprovação da transação ao Operador;/Supervisor.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class CodigoRespostaInstituicao : Field
    {
        public static short ID = 12;
        public CodigoRespostaInstituicao(Terminal terminal) : base(ID, 12, terminal) { }
    }

    /// <summary>
    /// Data da efetivação da transação.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class Data : Field
    {
        public static short ID = 13;
        public Data(Terminal terminal) : base(ID, 4, terminal) { }
    }

    /// <summary>
    /// Hora da efetivação da transação.
    /// Para pagamento de contas: 
    /// Este campo será repetido tantas vezes quanto for o número de Documentos pagos.
    /// </summary>
    public class Hora : Field
    {
        public static short ID = 14;
        public static short LENGTH = 6;
        const string PATTERN = @"^[0-2]\d[0-5]\d[0-5]\d$";
        public Hora(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public Hora(string hora) : base(ID, hora, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Número único que identifica a transação, é retornado pela instituição.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos.
    /// </summary>
    public class NSUHost : Field
    {
        public static short ID = 15;
        public static short LENGTH = 12;
        const string PATTERN = @"^\d*$";
        public NSUHost(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public NSUHost(string numero) : base(ID, numero, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Código do estabelecimento do lojista perante a Instituição
    /// </summary>
    public class CodigoDoEstabelecimento : Field
    {
        public static short ID = 16;
        public static short LENGTH = 15;
        const string PATTERN = @"^\d*$";
        public CodigoDoEstabelecimento(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public CodigoDoEstabelecimento(string codigo) : base(ID, codigo, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Número da autorização da transação de compra com cartão de crédito. 
    /// </summary>
    public class NumeroAutorizacao : Field
    {
        public static short ID = 17;
        public static short LENGTH = 6;
        public NumeroAutorizacao(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public NumeroAutorizacao(String numero) : base(ID, numero, LENGTH, null) { }
    }

    /// <summary>
    /// Descrição da Instituição que processou a transação
    /// </summary>
    public class NomeDaInstituicao : Field
    {
        public static short ID = 21;
        public static short LENGTH = 16;
        public NomeDaInstituicao(Terminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Número seqüencial único gerado pelo SiTef, para identificar uma transação.  
    /// Para pagamento de contas:
    /// Este campo será repetido tantas vezes quanto for o número de Documentos pagos. 
    /// </summary>
    public class NSUSiTef : Field
    {
        public static short ID = 22;
        public static short LENGTH = 6;
        const string PATTERN = @"^\d*$";
        public NSUSiTef(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public NSUSiTef(string nsu) : base(ID, nsu, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Indica a Bandeira do Cartão.
    /// </summary>
    public class BandeiraDoCartao : NumericField
    {
        public static short ID = 23;
        public static short LENGTH = 5;
        public BandeiraDoCartao(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public BandeiraDoCartao(int value) : base(ID, value, LENGTH) { }
        public BandeiraDoCartao(string value) : base(ID, value, LENGTH) { }
    }

    /// <summary>
    /// ‘0’: Não deve coletar taxa de serviço
    /// ‘1’: Coletar taxa de serviço se necessário
    /// </summary>
    public class TaxaServico : Field
    {
        public static short ID = 27;
        public static short LENGTH = 1;
        const string PATTERN = @"^[0-1]$";
        public TaxaServico(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public TaxaServico(bool coletar) : base(ID, coletar ? "1" : "0", LENGTH, PATTERN) { }
    }

    /// <summary>
    /// ‘0’: Não deve coletar código de segurança do cartão
    /// ‘1’: Coletar código de segurança do cartão
    /// </summary>
    public class CapturaCodigoSeguranca : Field
    {
        public static short ID = 33;
        public static short LENGTH = 1;
        const string PATTERN = @"^[0-1]$";
        public CapturaCodigoSeguranca(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public CapturaCodigoSeguranca(bool coletar) : base(ID, coletar ? "1" : "0", LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Linha que compõe o cupom da transação. Se repete até completar o cupom. 
    /// </summary>
    public class LinhasDeCupom : Field
    {
        public static short ID = 76;
        public static short LENGTH = 80;
        public LinhasDeCupom(Terminal terminal)
            : base(ID, LENGTH, terminal)
        {
            while (terminal.ExistemMaisElementos(ID))
                _value += String.Format("\n{0}", terminal.LeCampo(ID, LENGTH));
        }
    }

    /// <summary>
    /// ‘0’: Venda
    /// ‘1’: Pagamento de contas
    /// Para estorno de pré-autorização:
    /// ‘2’: Indica estorno de pré-autorização
    /// ‘4’: Indica estorno de captura de pré-autorização
    /// </summary>
    public class TipoTransacao : NumericField
    {
        public static short ID = 83;
        public static short LENGTH = 1;
        public TipoTransacao(Terminal terminal) : base(ID, LENGTH, terminal) { }
        private TipoTransacao(string tipo) : base(ID, tipo, LENGTH) { }

        public static TipoTransacao VENDA = new TipoTransacao("0");
        public static TipoTransacao PAGAMENTO_DE_CONTAS = new TipoTransacao("1");

        public static TipoTransacao ESTORNO_PRE_AUTORIZACAO = new TipoTransacao("2");
        public static TipoTransacao ESTORNO_CAPTURA = new TipoTransacao("4");

    }

    /// <summary>
    /// Data da impressora fiscal (Onde DD=dia, MM=mês e AAAA=ano).
    /// </summary>
    public class DataFiscal : DateField
    {
        const short ID = 147;
        public DataFiscal(string data) : base(ID, data) { }
        public DataFiscal(DateTime data) : base(ID, data) { }
        public DataFiscal(Terminal terminal) : base(ID, terminal) { }
    }

    /// <summary>
    /// Hora da impressora fiscal (Onde HH=hora, MM=minuto e SS=segundo).
    /// </summary>
    public class HoraFiscal : Field
    {
        const string PATTERN = @"^[0-2]\d[0-5]\d[0-5]\d$";
        const short ID = 148;
        const short LENGTH = 6;

        public HoraFiscal(string hora) : base(ID, hora, LENGTH, PATTERN) { }

        public HoraFiscal(short horas, short minutos, short segundos) : base(ID, String.Format("{0}{1}{2}", horas, minutos, segundos), LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Informa o número do cupom fiscal. 
    /// </summary>
    public class CupomFiscal : Field
    {
        public CupomFiscal(string cupom) : base(149, cupom, 12, @"^\d*$") { }
    }

    /// <summary>
    /// Informa o Operador do PDV
    /// </summary>
    public class Operador : Field
    {
        public Operador(string operador) : base(150, operador, 20, null) { }
    }

    /// <summary>
    /// Informa o Operador do PDV
    /// </summary>
    public class Supervisor : Field
    {
        public Supervisor(string supervisor) : base(151, supervisor, 20, null) { }
    }

    /// <summary>
    /// Valor da taxa de serviço a ser cobrada na operação de venda (Por exemplo: gorjeta).
    /// Quando compra IATA o valor indica a taxa de embarque.
    /// Este valor é opcional
    /// </summary>
    public class ValorTaxaDeServico : Field
    {
        public static short ID = 187;
        public static short LENGTH = 9;
        const string PATTERN = @"^\d*$";
        public ValorTaxaDeServico(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public ValorTaxaDeServico(float value) : base(ID, (value * 100).ToString(), LENGTH, PATTERN) { }
        public ValorTaxaDeServico(string value) : base(ID, value, LENGTH, PATTERN) { }

    }

    /// <summary>
    /// Data de expiração da pré- autorização, onde DD=Dia, MM=Mês, AAAA=Ano. (Usado pela Redecard) 
    /// </summary>
    public class DataExpiracao : DateField
    {
        public static short ID = 216;

        public DataExpiracao(Terminal terminal) : base(ID, terminal) { }
        public DataExpiracao(String data) : base(ID, data) { }
        public DataExpiracao(DateTime data) : base(ID, data) { }
    }

    /// <summary>
    /// Data em que a transação foi efetuada, onde DD=Dia, MM=Mês, AAAA=Ano. 
    /// </summary>
    public class DataDaTransacao : DateField
    {
        public static short ID = 217;

        public DataDaTransacao(Terminal terminal) : base(ID, terminal) { }
        public DataDaTransacao(String data) : base(ID, data) { }
        public DataDaTransacao(DateTime data) : base(ID, data) { }
    }

    /// <summary>
    /// ‘1’: Transação com Tarja Magnética
    /// ‘2’: Transação Digitada
    /// ‘6’: EGift (Utilizado pela rede Hug nas transações de consulta de saldo, venda e cancelamento de venda com cartões GIFT)
    /// </summary>
    public class TipoOperacaoDeVenda : Field
    {
        public static short ID = 379;
        public TipoOperacaoDeVenda(Terminal terminal) : base(ID, 1, terminal) { }
    }

    /// <summary>
    /// Indica o tipo de transação que será realizada (Utilizada na Consulta Cartão): 
    /// 1 – Consulta Saldo Cartão Gift 
    /// 2 – Recarga de Cartão Gift 
    /// 3 – Venda com Cartão Gift 
    /// 4 – Cancelamento de Venda com Cartão Gift 
    /// 5 – Cancelamento de Recarga de cartão Gift 
    /// 6 – Cancelamento Generico Ciagroup Gift 
    /// 7 – Inutilização de Cartão Gift 
    /// </summary>
    public class TipoDeTransacaoConsultaCartao : Field
    {
        public TipoDeTransacaoConsultaCartao(string tipo) : base(560, tipo, 4, @"^\s*\d*$") { }

        public static TipoDeTransacaoConsultaCartao CONSULTA_SALDO_CARTAO_GIFT = new TipoDeTransacaoConsultaCartao("1");
        public static TipoDeTransacaoConsultaCartao RECARGA_CARTAO_GIFT = new TipoDeTransacaoConsultaCartao("2");
        public static TipoDeTransacaoConsultaCartao VENDA_CARTAO_GIFT = new TipoDeTransacaoConsultaCartao("3");
        public static TipoDeTransacaoConsultaCartao CANCELAMENTO_VENDA_CARTAO_GIFT = new TipoDeTransacaoConsultaCartao("4");
        public static TipoDeTransacaoConsultaCartao CANCELAMENTO_RECARGA_CARTAO_GIFT = new TipoDeTransacaoConsultaCartao("5");
        public static TipoDeTransacaoConsultaCartao CANCELAMENTO_GENERICO_CIAGROUP_GIFT = new TipoDeTransacaoConsultaCartao("6");
        public static TipoDeTransacaoConsultaCartao INUTILIZACAO_CARTAO_GIFT = new TipoDeTransacaoConsultaCartao("7");

    }

}
