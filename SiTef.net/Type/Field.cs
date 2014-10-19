using SiTef.net.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SiTef.net.Type
{

    public interface IField
    {
        void WriteTo(Terminal terminal);
    }

    /// <summary>
    /// Classe base para todos os mapeamentos de campos no SiTef.
    /// Campos são responsáveis por fornecer os dados utilizados na
    /// execução das ações pela LibSiTef.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Field<T> : IField
    {
        public string Label { get; set; }
        public short Id { get; set; }
        public string WireValue { get; set; }
        private T _value;
        public T Value
        {
            get
            {
                if (_value != null)
                    return _value;


                if (_value == null && WireValue != null)
                    _value = Convert(WireValue);
                else
                    return default(T);

                return _value;
            }
            set { _value = value; }
        }
        public int Length { get; set; }

        /// <summary>
        /// Efetua a conversão necessária da String retornada pelo Terminal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Valor convertido</returns>
        public abstract T Convert(string value);

        /// <summary>
        /// Formata o valor do campo de forma que possa ser corretamente escrito no Terminal.
        /// </summary>
        /// <returns>Valor formatado do Campo</returns>
        public abstract string Format();

        /// <summary>
        /// Escreve o valor do Campo no Terminal
        /// efetuando as conversões necessárias;
        /// </summary>
        /// <param name="terminal"></param>
        public void WriteTo(Terminal terminal)
        {
            if (Value != null)
                terminal.GravaCampo((IntPtr)Id, Format());
        }

        /// <summary>
        /// Construtor utilizado para ler o campo do Terminal SiTef
        /// </summary>
        /// <param name="id">ID do Campo</param>
        /// <param name="length">Tamanho do Campo</param>
        /// <param name="terminal">Terminal de onde o campo será lido</param>
        public Field(short id, int length, Terminal terminal)
        {
            this.Id = id;
            this.Length = length;
            try
            {
                this.WireValue = terminal.LeCampo(id, length);
            }
            catch (TerminalException ex)
            {
                Console.Error.WriteLine(String.Format("{0} {1}", Label != null ? Label : GetType().Name, ex.Message));
            }
        }


        public Field(short id, T value, int length)
        {
            Id = id;
            Value = value;
            Length = length;

            if (value == null)
                throw new ArgumentException("Null", "value");

        }

        public override string ToString()
        {
            if (Value != null)
                return String.Format("{2}({0})\n{1}", Id, Value, Label != null ? Label : this.GetType().Name);
            return "null";
        }

    }

    /// <summary>
    /// Classe base para todos os campos Booleanos
    /// </summary>
    public class BooleanField : Field<bool?>
    {

        public string TrueValue { get; set; }

        public string FalseValue { get; set; }

        public BooleanField(short id, bool value, short length, string trueValue, string falseValue)
            : base(id, value, length)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
        }

        public BooleanField(short id, int length, Terminal terminal, string trueValue, string falseValue)
            : base(id, length, terminal)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
        }

        public override bool? Convert(string value)
        {
            if (value == null)
                return null;
            return value.Equals(TrueValue);
        }

        public override string Format()
        {
            if (Value == null)
                return null;
            return Value == true ? TrueValue : FalseValue;
        }
    }
    /// <summary>
    /// Campo booleano implementado como valor de 0 ou 1
    /// </summary>
    public class ZeroOrOneField : BooleanField
    {
        public ZeroOrOneField(short id, bool value) : base(id, value, 1, "1", "0") { }
        public ZeroOrOneField(short id, Terminal terminal) : base(id, 1, terminal, "1", "0") { }
    }

    /// <summary>
    /// Campos com valor Alfanumérico podem herdar dessa classe base,
    /// que também é útil para gravar ou ler valores de um terminal
    /// sem a necessidade de se utilizar um campo específico para tanto.
    /// </summary>
    public class StringField : Field<string>
    {

        /// <summary>
        /// Constroi um novo campo utilizado RegExp para validação
        /// </summary>
        /// <param name="id">Código do Campo</param>
        /// <param name="value">Valor</param>
        /// <param name="length">Tamanho máximo do campo</param>
        /// <param name="pattern">RegExp para validação</param>
        protected StringField(short id, string value, short length, string pattern)
            : base(id, value, length)
        {
            if (value == null)
                throw new ArgumentNullException();

            if (value.Length == 0)
                throw new ArgumentException("empty", "value");

            if (length > 0 && value.Length > length)
                throw new ArgumentException(String.Format("{0} exceeds the maximum length: {1}", value.Length, length), "value");

            if (pattern != null && !Regex.IsMatch(value, pattern))
                throw new ArgumentException("format not valid", "value");

        }

        public StringField(short id, string value, int length) : base(id, value, length) { }

        public StringField(short id, int length, Terminal terminal)
            : base(id, length, terminal)
        {
            while (terminal.ExistemMaisElementos(id))
                Value += String.Format("\n{0}", terminal.LeCampo(id, length));
        }

        public override string Convert(string value)
        {
            return value;
        }


        public override string Format()
        {
            return Value;
        }
    }

    /// <summary>
    /// Implementação base para todos os campos numéricos
    /// </summary>
    public class NumericField : Field<int?>
    {

        /// <summary>
        /// Devemos completar o valor convertido
        /// com Zeros à esquerda?
        /// </summary>
        public bool Padding { get; set; }

        public NumericField(short id, int value, int length, bool padding)
            : base(id, value, length)
        {
            this.Padding = padding;
        }
        public NumericField(short id, int value, int length) : this(id, value, length, true) { }
        public NumericField(short id, int length, Terminal terminal) : base(id, length, terminal) { }

        public override int? Convert(string value)
        {
            if (value == null)
                return null;
            return int.Parse(value);
        }

        public override string Format()
        {
            if (Value == null)
                return null;
            return String.Format("{0}", Value);
        }
    }

    /// <summary>
    /// Implementação base para todos os campos de data
    /// </summary>
    public class DateField : Field<DateTime?>
    {
        /// <summary>
        /// Formato utilizado para fazer parse da Data.
        /// Ex.: ddMMyyyy
        /// </summary>
        public string Pattern { get; set; }

        public DateField(short id, DateTime? data, int length, string format)
            : base(id, data, length)
        {
            Pattern = format;
        }

        public DateField(short id, int day, int month, int year, int length, string format)
            : base(id, new DateTime(year, month, day), length)
        {
            Pattern = format;
        }

        public DateField(short id, int length, Terminal terminal) : base(id, length, terminal) { }

        public DateField(short id, Terminal terminal) : this(id, 8, terminal) { }

        public override DateTime? Convert(string value)
        {
            try
            {
                return DateTime.ParseExact(value, Pattern, null);
            }
            catch (FormatException ex)
            {
                return null;
            }
        }

        public override string Format()
        {
            if (Value != null)
                return ((DateTime)Value).ToString(Pattern);
            return null;
        }

        public override string ToString()
        {
            if (Value != null)
                return base.ToString();

            return String.Format("{1}({0})\nData Inválida, ou Null", Id, Label != null ? Label : this.GetType().Name);

        }
    }

    /// <summary>
    /// Código da rede de destino.
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos. 
    /// </summary>
    public class Rede : NumericField
    {
        public static short LENGTH = 4;
        public Rede(Terminal terminal) : base(1, LENGTH, terminal) { }
        public Rede(int codigo) : base(1, codigo, LENGTH) { }

        public static Rede TECHAN = new Rede(1);
        public static Rede REDE = new Rede(5);
        public static Rede AMEX = new Rede(6);
        public static Rede DINNERS = new Rede(5);
        public static Rede SERASA = new Rede(9);
        public static Rede BANRISUL = new Rede(21);
        public static Rede EDENRED = new Rede(41);
        public static Rede HIPER = new Rede(51);
        public static Rede CETELEN = new Rede(55);
        public static Rede GWCEL = new Rede(106);
        public static Rede CIELO = new Rede(125);
        public static Rede CSU = new Rede(170);
        public static Rede TSYS = new Rede(190);
        public static Rede HUG = new Rede(218);
        public static Rede CIAGROUP = new Rede(234);
        public static Rede CORRESPONDENTE_BANCARIO_BRADESCO = new Rede(805);
    }

    /// <summary>
    /// Número de parcelas da compra, quando a vista preencher com ’1’
    /// </summary>
    public class NumeroDeParcelas : NumericField
    {
        public static short LENGTH = 2;
        public NumeroDeParcelas(Terminal terminal) : base(2, LENGTH, terminal) { }
        public NumeroDeParcelas(int parcelas) : base(2, parcelas, LENGTH, false) { }
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
    public class TipoDeFinanciamento : NumericField
    {
        public TipoDeFinanciamento(int tipo) : base(3, tipo, 2, false) { }

        //TODO: Criar as instancias estaticas aí em cima
    }

    /// <summary>
    /// Número do cartão de crédito. Para envio de número de cartão criptografado, incluir o prefixo “CRIPSITEF:” antes do cartão.
    /// </summary>
    public class NumeroDoCartao : StringField
    {
        public NumeroDoCartao(string numero) : base(4, numero, 30, @"^\d{0,30}$") { }
    }

    /// <summary>
    /// Data de vencimento do cartão MMAA (MM=mês AA=ano) 
    /// </summary>
    public class DataDeVencimento : DateField
    {
        public static short ID = 5;
        public static int LENGTH = 4;
        public static string FORMAT = "MMyy";
        public DataDeVencimento(DateTime data) : base(ID, data, LENGTH, FORMAT) { }
        public DataDeVencimento(int mes, int ano) : base(ID, 1, mes, ano, LENGTH, FORMAT) { }
    }

    /// <summary>
    /// Indica o número do código de segurança (CVV2-Visanet, CVC- Redecard Alfa Code-Amex) grafado no verso do cartão.
    /// Informar o Valor ‘0’ se não existir ou ‘1’ se estiver ilegível. 
    /// </summary>
    public class CodigoDeSeguranca : StringField
    {
        public CodigoDeSeguranca(string codigo) : base(6, codigo, 5, @"^\d*$") { }
    }

    /// <summary>
    /// Valor da Compra deve ser enviado com duas casas decimais, porém sem a virgula.
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class Valor : NumericField
    {
        public static short ID = 7;
        public static short LENGTH = 12;
        public Valor(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public Valor(int quantia) : base(ID, quantia * 100, LENGTH) { }
        public Valor(double quantia) : this(System.Convert.ToInt32(quantia * 100)) { }
        public Valor(decimal quantia) : this(System.Convert.ToInt32(quantia * 100)) { }
    }

    /// <summary>
    /// Código do cliente, campo administrativo utilizado pelo SITEFWEB
    /// </summary>
    public class CodigoDoCliente : StringField
    {
        public CodigoDoCliente(string codigo) : base(8, codigo, 12, null) { }
    }

    /// <summary>
    /// Dados que devem ser utilizados para confirmação da transação. 
    /// </summary>
    public class DadosDeConfirmacao : StringField
    {
        public static short ID = 9;
        public static short LENGTH = 128;
        public DadosDeConfirmacao(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public DadosDeConfirmacao(string dados) : base(ID, dados, LENGTH) { } 
    }

    /// <summary>
    /// Resultado da transação, quando ‘000’ indica que a transação foi aprovada ou Nada Consta para Consulta ACSP. 
    /// </summary>
    public class CodigoDeRespostaSiTef : StringField
    {
        public static short ID = 10;
        public CodigoDeRespostaSiTef(Terminal terminal) : base(ID, 3, terminal) { }
        public bool Approved()
        {
            return "000".Equals(Value);
        }
    }

    /// <summary>
    /// Texto retornado pelo SITEF para exibição no terminal.  
    /// Este campo será repetido tantas vezes quanto for o número de linhas a serem exibidas
    /// </summary>
    public class TextoParaExibicao : StringField
    {
        public static short ID = 11;
        public static short LENGTH = 64;
        public TextoParaExibicao(Terminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Código retornado pela Instituição ou pelo SiTef.
    /// Caso retorne ‘SC’ deve- se solicitar aprovação da transação ao Operador;/Supervisor.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class CodigoRespostaInstituicao : StringField
    {
        public static short ID = 12;
        public CodigoRespostaInstituicao(Terminal terminal) : base(ID, 12, terminal) { }
    }

    /// <summary>
    /// Data da efetivação da transação.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class Data : StringField
    {
        public static short ID = 13;
        public Data(Terminal terminal) : base(ID, 4, terminal) { }
    }

    /// <summary>
    /// Hora da efetivação da transação.
    /// Para pagamento de contas: 
    /// Este campo será repetido tantas vezes quanto for o número de Documentos pagos.
    /// </summary>
    public class Hora : StringField
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
    public class NSUHost : StringField
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
    public class CodigoDoEstabelecimento : StringField
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
    public class NumeroAutorizacao : StringField
    {
        public static short ID = 17;
        public static short LENGTH = 6;
        public NumeroAutorizacao(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public NumeroAutorizacao(String numero) : base(ID, numero, LENGTH, null) { }
    }

    /// <summary>
    /// Descrição da Instituição que processou a transação
    /// </summary>
    public class NomeDaInstituicao : StringField
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
    public class NSUSiTef : StringField
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
    }

    /// <summary>
    /// ‘0’: Não deve coletar taxa de serviço
    /// ‘1’: Coletar taxa de serviço se necessário
    /// </summary>
    public class TaxaServico : ZeroOrOneField
    {
        public static short ID = 27;
        public TaxaServico(Terminal terminal) : base(ID, terminal) { }
        public TaxaServico(bool coletar) : base(ID, coletar) { }
    }

    /// <summary>
    /// ‘0’: Não deve coletar código de segurança do cartão
    /// ‘1’: Coletar código de segurança do cartão
    /// </summary>
    public class CapturaCodigoSeguranca : ZeroOrOneField
    {
        public static short ID = 33;
        public CapturaCodigoSeguranca(Terminal terminal) : base(ID, terminal) { }
        public CapturaCodigoSeguranca(bool coletar) : base(ID, coletar) { }
    }

    /// <summary>
    /// Linha que compõe o cupom da transação. Se repete até completar o cupom. 
    /// </summary>
    public class LinhasDeCupom : StringField
    {
        public static short ID = 76;
        public static short LENGTH = 80;
        public LinhasDeCupom(Terminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Linha que compõe o cupom da transação. Se repete até completar o cupom. 
    /// </summary>
    public class LinhasDeCupomEstabelecimento : StringField
    {
        public static short ID = 80;
        public static short LENGTH = 80;
        public LinhasDeCupomEstabelecimento(Terminal terminal) : base(ID, LENGTH, terminal) { }
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
        private TipoTransacao(int tipo) : base(ID, tipo, LENGTH) { }

        public static TipoTransacao VENDA = new TipoTransacao(0);
        public static TipoTransacao PAGAMENTO_DE_CONTAS = new TipoTransacao(1);

        public static TipoTransacao ESTORNO_PRE_AUTORIZACAO = new TipoTransacao(2);
        public static TipoTransacao ESTORNO_CAPTURA = new TipoTransacao(4);

    }

    /// <summary>
    /// ‘0’ – Confirma a transação
    /// ‘1’ – Cancela a Transação
    /// </summary>
    public class TipoConfirmacao : NumericField
    {
        public static short ID = 85;
        public static short LENGTH = 1;
        public TipoConfirmacao(Terminal terminal) : base(ID, LENGTH, terminal) { }
        private TipoConfirmacao(int tipo) : base(ID, tipo, LENGTH) { }

        public static TipoConfirmacao CONFIRMA = new TipoConfirmacao(0);
        public static TipoConfirmacao CANCELA = new TipoConfirmacao(1);
    }

    /// <summary>
    /// Data da impressora fiscal (Onde DD=dia, MM=mês e AAAA=ano).
    /// </summary>
    public class DataFiscal : DateField
    {
        public const short ID = 147;
        public const int LENGTH = 8;

        public DataFiscal(DateTime data) : base(ID, data, 8, "ddMMyyy") { }
        public DataFiscal(Terminal terminal) : base(ID, terminal) { }
    }

    /// <summary>
    /// Hora da impressora fiscal (Onde HH=hora, MM=minuto e SS=segundo).
    /// </summary>
    public class HoraFiscal : DateField
    {
        const string PATTERN = @"HHmmss";
        const short ID = 148;
        const short LENGTH = 6;

        public HoraFiscal(DateTime hora) : base(ID, hora, LENGTH, PATTERN) { }

        public HoraFiscal(short horas, short minutos, short segundos) : this(new DateTime()) { }
    }

    /// <summary>
    /// Informa o número do cupom fiscal. 
    /// </summary>
    public class CupomFiscal : StringField
    {
        public CupomFiscal(string cupom) : base(149, cupom, 12, @"^\d*$") { }
    }

    /// <summary>
    /// Informa o Operador do PDV
    /// </summary>
    public class Operador : StringField
    {
        public Operador(string operador) : base(150, operador, 20, null) { }
    }

    /// <summary>
    /// Informa o Operador do PDV
    /// </summary>
    public class Supervisor : StringField
    {
        public Supervisor(string supervisor) : base(151, supervisor, 20, null) { }
    }

    /// <summary>
    /// Valor da taxa de serviço a ser cobrada na operação de venda (Por exemplo: gorjeta).
    /// Quando compra IATA o valor indica a taxa de embarque.
    /// Este valor é opcional
    /// </summary>
    public class ValorTaxaDeServico : StringField
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
        public static int LENGTH = 8;

        public DataExpiracao(Terminal terminal) : base(ID, terminal) { }
        public DataExpiracao(DateTime data) : base(ID, data, 8, "ddMMyyyy") { }
    }

    /// <summary>
    /// Data em que a transação foi efetuada, onde DD=Dia, MM=Mês, AAAA=Ano. 
    /// </summary>
    public class DataDaTransacao : DateField
    {
        public static short ID = 217;

        public DataDaTransacao(Terminal terminal) : base(ID, terminal) { }
        public DataDaTransacao(DateTime data) : base(ID, data, 8, "ddMMyyyy") { }
    }

    /// <summary>
    /// ‘1’: Transação com Tarja Magnética
    /// ‘2’: Transação Digitada
    /// ‘6’: EGift (Utilizado pela rede Hug nas transações de consulta de saldo, venda e cancelamento de venda com cartões GIFT)
    /// </summary>
    public class TipoOperacaoDeVenda : NumericField
    {
        public static short ID = 379;
        public TipoOperacaoDeVenda(Terminal terminal) : base(ID, 1, terminal) { }
    }

    /// <summary>
    /// Campo indicativo das formas de pagamento disponíveis (recebimento) ou utilizadas (envio).
    /// 
    ///  Transações Gift(Recebido na resposta da transação Recarga)
    ///  . Será retornado n registros no formato: FF:CC1-CC2...-CCn onde:
    ///  
    /// FF – Indica a forma de pgto Permitida.
    /// CC - indica o ID do dado que o PDV deve enviar ao módulo Sitef na confirmação 
    /// 
    /// OBS: Poderá ser retornado registro com apenas o campo FF ou com apenas 1 campo CC:
    /// Ex: 00
    /// 02:10
    /// 
    /// - Transações Gift(Enviado na confirmação) 
    /// . A aplicação da Automação Comercial deverá enviar cada forma de pagamento utilizada no seguinte formato:
    /// 
    /// FF:Valor:CC:Dado a enviar..CC:Dado a enviar
    /// 
    /// Onde:
    /// FF - Indica a forma de pgto Utilizada.
    /// 
    /// Valor – Valor da transação.
    /// 
    /// CC - indica o ID do dado que o PDV deve enviar
    /// 
    /// Dado a enviar – Dado a ser enviado ao Sitef, conforme o CC.
    /// 
    /// Ex: Utilizando forma de pgto ‘00’ e ‘02’ com valor de 5,00:
    /// 00:500
    /// 02:500:10:0319190013DQ
    /// 
    /// (Consulte documento “Novos serviço e prefixo de Formas de Pagamento.doc”)
    /// </summary>
    public class FormasDePagamento : StringField
    {
        public static short ID = 545;
        public static short LENGTH = 200;
        public FormasDePagamento(Terminal terminal) : base(ID, LENGTH, terminal) { }
        public FormasDePagamento(string formas) : base(ID, formas, LENGTH) { }
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
    public class TipoDeTransacaoConsultaCartao : StringField
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
