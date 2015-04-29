using SiTef.net.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SiTef.net.Type
{

    public interface IField
    {
        void WriteTo(ITerminal terminal);
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
        public void WriteTo(ITerminal terminal)
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
        public Field(short id, int length, ITerminal terminal)
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
                return String.Format("{2}({0}):{1}", Id, Value, Label != null ? Label : this.GetType().Name);
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

        public BooleanField(short id, int length, ITerminal terminal, string trueValue, string falseValue)
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
        public ZeroOrOneField(short id, ITerminal terminal) : base(id, 1, terminal, "1", "0") { }
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

        public StringField(short id, int length, ITerminal terminal)
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
        public NumericField(short id, int length, ITerminal terminal) : base(id, length, terminal) { }

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

        public DateField(short id, int length, ITerminal terminal) : base(id, length, terminal) { }

        public DateField(short id, ITerminal terminal) : this(id, 8, terminal) { }

        public override DateTime? Convert(string value)
        {
            try
            {
                return DateTime.ParseExact(value, Pattern, null);
            }
            catch (FormatException)
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
        public Rede(ITerminal terminal) : base(1, LENGTH, terminal) { }
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
        public NumeroDeParcelas(ITerminal terminal) : base(2, LENGTH, terminal) { }
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
        public DataDeVencimento(ITerminal terminal) : base(ID, terminal) { }
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
        public Valor(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public Valor(int quantia) : base(ID, quantia, LENGTH) { }

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
        public DadosDeConfirmacao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public DadosDeConfirmacao(string dados) : base(ID, dados, LENGTH) { }
    }

    /// <summary>
    /// Resultado da transação, quando ‘000’ indica que a transação foi aprovada ou Nada Consta para Consulta ACSP. 
    /// </summary>
    public class CodigoDeRespostaSiTef : StringField
    {
        public static short ID = 10;
        public CodigoDeRespostaSiTef(ITerminal terminal) : base(ID, 3, terminal) { }
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
        public TextoParaExibicao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Código retornado pela Instituição ou pelo SiTef.
    /// Caso retorne ‘SC’ deve- se solicitar aprovação da transação ao Operador;/Supervisor.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class CodigoRespostaInstituicao : StringField
    {
        public static short ID = 12;
        public CodigoRespostaInstituicao(ITerminal terminal) : base(ID, 12, terminal) { }
    }

    /// <summary>
    /// Data da efetivação da transação.  
    /// Para pagamento de contas: Este campo será repetido tantas vezes quanto for o número de Documentos pagos
    /// </summary>
    public class Data : DateField
    {
        public static short ID = 13;
        public Data(ITerminal terminal) : base(ID, 4, terminal) { Pattern = "MMdd"; }
        public Data(DateTime date) : base(ID, date, 4, "MMdd") { }
        public Data(int day, int month, int year) : base(ID, day, month, year, 4, "MMdd") { }
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
        public Hora(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
        public NSUHost(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
        public CodigoDoEstabelecimento(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public CodigoDoEstabelecimento(string codigo) : base(ID, codigo, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Número da autorização da transação de compra com cartão de crédito. 
    /// </summary>
    public class NumeroAutorizacao : StringField
    {
        public static short ID = 17;
        public static short LENGTH = 6;
        public NumeroAutorizacao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public NumeroAutorizacao(String numero) : base(ID, numero, LENGTH, null) { }
    }

    /// <summary>
    /// Número do documento da transação cancelada. 
    /// </summary>
    public class DocumentoCancelado : NumericField
    {
        public static short ID = 18;
        public static short LENGTH = 12;
        public DocumentoCancelado(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public DocumentoCancelado(int value) : base(ID, value, LENGTH) { }
    }

    /// <summary>
    /// Data de efetivação da compra cancelada.
    /// </summary>
    public class DataDaCompraCancelada : DateField
    {
        public static short ID = 19;
        public static short LENGTH = 4;
        public DataDaCompraCancelada(ITerminal terminal) : base(ID, LENGTH, terminal) {
            this.Pattern = "MMdd";
        }
        public DataDaCompraCancelada(DateTime date) : base(ID, date, LENGTH, "MMdd") { }
    }

    /// <summary>
    /// Hora de efetivação da compra cancelada.
    /// </summary>
    public class HoraDaCompraCancelada : StringField
    {
        public static short ID = 20;
        public static short LENGTH = 6;
        const string PATTERN = @"^[0-2]\d[0-5]\d[0-5]\d$";
        public HoraDaCompraCancelada(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public HoraDaCompraCancelada(string hora) : base(ID, hora, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Descrição da Instituição que processou a transação
    /// </summary>
    public class NomeDaInstituicao : StringField
    {
        public static short ID = 21;
        public static short LENGTH = 16;
        public NomeDaInstituicao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
        public NSUSiTef(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public NSUSiTef(string nsu) : base(ID, nsu, LENGTH, PATTERN) { }
    }

    /// <summary>
    /// Indica a Bandeira do Cartão.
    /// </summary>
    public class BandeiraDoCartao : NumericField
    {
        public static short ID = 23;
        public static short LENGTH = 5;
        public BandeiraDoCartao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public BandeiraDoCartao(int value) : base(ID, value, LENGTH) { }
    }

    /// <summary>
    /// ‘0’: Não deve validar
    /// ‘1’: Devem fazer a validação  solicitando ao operador do PDV a digitação dos últimos 4 dígitos grafados no cartão
    /// e comparando o resultado com 4 dígitos presentes no próximo campo.
    /// Caso não coincidam, apresentar a mensagem “Cartão Inválido” e re-iniciar a transação. 
    /// </summary>
    public class ValidaEmbosso : ZeroOrOneField
    {
        public static short ID = 24;
        public ValidaEmbosso(ITerminal terminal) : base(ID, terminal) { }
    }

    /// <summary>
    /// Código a ser validado quando da obrigatoriedade da consistência do embosso
    /// </summary>
    public class CodigoValidacao : NumericField
    {
        public static short ID = 25;
        public static int LENGTH = 4;
        public CodigoValidacao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// ‘0’: Não deve coletar senha
    /// ‘1’: Coletar senha obrigatoriamente
    /// ‘2’: Senha opcional, deve ser coletada se existir pin no PDV. Caso a senha não seja coletada (por ser opcional ou não necessária), o campo correspondente nas mensagens enviadas ao SiTef deve ser passado vazio.
    /// </summary>
    public class TipoSenha : NumericField
    {
        public static short ID = 26;
        public static int LENGTH = 1;
        public TipoSenha(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// ‘0’: Não deve coletar taxa de serviço
    /// ‘1’: Coletar taxa de serviço se necessário
    /// </summary>
    public class TaxaServico : ZeroOrOneField
    {
        public static short ID = 27;
        public TaxaServico(ITerminal terminal) : base(ID, terminal) { }
        public TaxaServico(bool coletar) : base(ID, coletar) { }
    }

    /// <summary>
    /// Indica o número mínimo de parcelas 
    /// </summary>
    public class NumMinParcela : NumericField
    {
        public static short ID = 28;
        public static int LENGTH = 2;
        public NumMinParcela(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Indica o número máximo de parcelas
    /// </summary>
    public class NumMaxParcela : NumericField
    {
        public static short ID = 29;
        public static int LENGTH = 2;
        public NumMaxParcela(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Percentual máximo de taxa de serviço
    /// </summary>
    public class PercentualMaxTaxaServico : NumericField
    {
        public static short ID = 30;
        public static int LENGTH = 4;
        public PercentualMaxTaxaServico(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Data máxima limite da venda pré-datada. 
    /// </summary>
    public class DataLimPreDatado : DateField
    {
        public static short ID = 31;
        public static int LENGTH = 8;
        public static string PATTERN = @"ddMMyyyy";
        public DataLimPreDatado(ITerminal terminal)
            : base(ID, LENGTH, terminal)
        {
            Pattern = PATTERN;
        }

    }

    /// <summary>
    /// Data limite para a primeira parcela para venda a Débito 
    /// parcelada com juros pela Administradora. (DDMMAAAA)
    /// </summary>
    public class DataLimPrimeiraParcela : DateField
    {
        public static short ID = 32;
        public static int LENGTH = 8;
        public static string PATTERN = @"ddMMyyyy";
        public DataLimPrimeiraParcela(ITerminal terminal)
            : base(ID, LENGTH, terminal)
        {
            Pattern = PATTERN;
        }

    }

    /// <summary>
    /// ‘0’: Não deve coletar código de segurança do cartão
    /// ‘1’: Coletar código de segurança do cartão
    /// </summary>
    public class CapturaCodigoSeguranca : ZeroOrOneField
    {
        public static short ID = 33;
        public CapturaCodigoSeguranca(ITerminal terminal) : base(ID, terminal) { }
        public CapturaCodigoSeguranca(bool coletar) : base(ID, coletar) { }
    }

    /// <summary>
    /// ‘0’: Não solicitar tipo de garantia 
    /// ‘1’: Solicitar tipo de garantia (COM ou SEM garantia
    /// </summary>
    public class GarantiaPreDatado : ZeroOrOneField
    {
        public static short ID = 34;
        public GarantiaPreDatado(ITerminal terminal) : base(ID, terminal) { }
    }

    /// <summary>
    /// ‘0’: Não utiliza Chip 
    /// ‘1’: Utiliza Chip
    /// </summary>
    public class TransacaoComChip : ZeroOrOneField
    {
        public static short ID = 35;
        public TransacaoComChip(ITerminal terminal) : base(ID, terminal) { }
    }

    /// <summary>
    /// Linha que compõe o cupom da transação. Se repete até completar o cupom. 
    /// </summary>
    public class LinhasDeCupom : StringField
    {
        public static short ID = 76;
        public static short LENGTH = 80;
        public LinhasDeCupom(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Linha que compõe o cupom da transação. Se repete até completar o cupom. 
    /// </summary>
    public class LinhasDeCupomEstabelecimento : StringField
    {
        public static short ID = 80;
        public static short LENGTH = 80;
        public LinhasDeCupomEstabelecimento(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
        public TipoTransacao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
        public TipoConfirmacao(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
        public DataFiscal(ITerminal terminal) : base(ID, terminal) { }
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
    /// Este é um campo que deve ser utilizado para gravação de prefixo´s opcionais do SITEF.
    /// Exemplos:
    ///     VIACRT:5
    ///     TITULAR:1
    /// </summary>
    public class CamposVariaveisComPrefixo : StringField
    {
        public CamposVariaveisComPrefixo(string valor) : base(156, valor, 128, null) { }
    }

    /// <summary>
    /// Número do documento de identidade (RG)
    /// </summary>
    public class RG : StringField
    {
        public RG(string value) : base(161, value, 20, null) { }
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
        public ValorTaxaDeServico(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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

        public DataExpiracao(ITerminal terminal) : base(ID, terminal) { }
        public DataExpiracao(DateTime data) : base(ID, data, 8, "ddMMyyyy") { }
    }

    /// <summary>
    /// Data em que a transação foi efetuada, onde DD=Dia, MM=Mês, AAAA=Ano. 
    /// </summary>
    public class DataDaTransacao : DateField
    {
        public static short ID = 217;

        public DataDaTransacao(ITerminal terminal) : base(ID, terminal) { }
        public DataDaTransacao(DateTime data) : base(ID, data, 8, "ddMMyyyy") { }
    }

    /// <summary>
    /// O formato deste campo, quando retornado, é:
    /// 
    ///     PERG:<ID>,<Pergunta>,<Regra>,<tamMin>,<tamMax>;    
    /// 
    /// Onde:  
    /// ID - Campo alfanumérico com tamanho máximo de 4 caracteres.
    ///     Cada mensagem apresentada terá sua identificação, 
    ///     que deverá ser repassada ao SiTef futuramente. 
    ///     Esse campo não possui valores repetidos.  
    /// 
    /// Pergunta - Campo alfanumérico com a mensagem de coleta que deverá 
    ///     ser apresentada no display.
    ///     Tamanho máximo de 40 caracteres, caso o dispositivo seja o PDV, 
    ///     ou 20 caracteres, caso o dispositivo seja o PINPAD.  
    /// 
    /// Regra - Campo numérico com tamanho fixo 1. 
    ///     Este campo indica de qual dispositivo o PDV deve coletar os dados:  
    ///     0 – Teclado do operador (PDV).  
    ///     1 – PINPAD (neste caso o dado retornará criptografado).  
    ///     2 – Leitora do cartão.  
    /// 
    /// tamMin - Campo numérico com tamanho máximo de 2 caracteres. 
    ///     Indica o tamanho mínimo da resposta.  
    /// 
    /// tamMax - Campo numérico com tamanho máximo de 2 caracteres. 
    ///     Indica o tamanho máximo da resposta.  
    /// 
    /// Importante: 
    /// em um único MKT_Le_Campo (358), poderão vir diversas perguntas concatenadas, ou seja:   
    ///     
    ///     PERG1;PERG2;...PERGn;  
    ///     
    /// Uma vez coletada a pergunta, a resposta correspondente deve ser enviada
    /// no mesmo campo 358 da transação propriamente dita, 
    /// no seguinte formato:   
    ///     
    ///     PERG:<ID1>,<Resposta1>,<Regra1>;...PERG:<IDn>,<Respostan>,<Regran>; 
    ///     
    /// </summary>
    public class Perguntas : StringField
    {
        public static short ID = 358;
        public static int LENGTH = 128;
        public Perguntas(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Código do roteamento
    /// </summary>
    public class CodigoDoRoteamento : NumericField
    {
        public static short ID = 363;
        public static int LENGTH = 2;
        public CodigoDoRoteamento(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Código do produto
    /// </summary>
    public class CodigoDoProduto : NumericField
    {
        public static short ID = 364;
        public static int LENGTH = 4;
        public CodigoDoProduto(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public class DescricaoDoProduto : StringField
    {
        public static short ID = 365;
        public static int LENGTH = 20;
        public DescricaoDoProduto(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Nome da empresa
    /// </summary>
    public class NomeDaEmpresa : StringField
    {
        public static short ID = 366;
        public static int LENGTH = 24;
        public NomeDaEmpresa(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Nome do portador do cartão
    /// </summary>
    public class NomeDoPortador : StringField
    {
        public static short ID = 367;
        public static int LENGTH = 24;
        public NomeDoPortador(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }


    /// <summary>
    /// Código da linha de crédito 
    /// </summary>
    public class CodigoLinhaDeCredito : NumericField
    {
        public static short ID = 368;
        public static int LENGTH = 4;
        public CodigoLinhaDeCredito(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Descrição da linha de crédito
    /// </summary>
    public class DescricaoLinhaDeCredito : StringField
    {
        public static short ID = 369;
        public static int LENGTH = 20;
        public DescricaoLinhaDeCredito(ITerminal terminal) : base(ID, LENGTH, terminal) { }
    }

    /// <summary>
    /// Flags de controle de operação: 
    ///     0 = Inativo; 
    ///     1 = Ativo. 
    /// 
    /// Formato: 
    ///     xxxxxxxxxxKLMN 
    ///     
    /// K – não utilizado 
    /// L – habilita verificação dos 4 últimos dígitos do cartão 
    /// M – não utilizado 
    /// N – não utilizado 
    /// x – Reservado para uso futuro
    /// </summary>
    public class FlagsDeControleDeOperacao : StringField
    {
        public static short ID = 370;
        public static int LENGTH = 14;
        public FlagsDeControleDeOperacao(ITerminal terminal) : base(ID, LENGTH, terminal) { }

        /// <summary>
        ///  /// L – habilita verificação dos 4 últimos dígitos do cartão 
        /// </summary>
        /// <returns></returns>
        public bool VerificaDigitosCartao()
        {
            var flag = Value.ToCharArray()[12];
            return flag == '1';
        }

    }

    /// <summary>
    /// ‘0’: Transação não habilitada 
    /// ‘1’: Transação habilitada
    /// </summary>
    public class AutorizaSaldoDisponivel : ZeroOrOneField
    {
        public static short ID = 371;
        public AutorizaSaldoDisponivel(ITerminal terminal) : base(ID, terminal) { }
    }

    /// <summary>
    /// ‘0’: Crédito 
    /// ‘1’: Débito 
    /// </summary>
    public class TipoVenda : NumericField
    {
        public static short ID = 372;
        public TipoVenda(ITerminal terminal) : base(ID, 1, terminal) { }
        private TipoVenda(int tipo) : base(ID, tipo, 1) { }

        public static TipoVenda CREDITO = new TipoVenda(0);
        public static TipoVenda DEBITO = new TipoVenda(1);

    }

    /// <summary>
    /// ‘1’: Transação com Tarja Magnética
    /// ‘2’: Transação Digitada
    /// ‘6’: EGift (Utilizado pela rede Hug nas transações de consulta de saldo, venda e cancelamento de venda com cartões GIFT)
    /// </summary>
    public class TipoOperacaoDeVenda : NumericField
    {
        public static short ID = 379;
        public TipoOperacaoDeVenda(ITerminal terminal) : base(ID, 1, terminal) { }
        private TipoOperacaoDeVenda(int code) : base(ID, code, 1){}

        public static TipoOperacaoDeVenda TRANSACAO_COM_TARJA_MAGNETICA = new TipoOperacaoDeVenda(1);
        public static TipoOperacaoDeVenda TRANSACAO_DIGITADA = new TipoOperacaoDeVenda(2);
        public static TipoOperacaoDeVenda E_GIFT = new TipoOperacaoDeVenda(6);
    }

    /// <summary>
    /// Trilha 1 do Cartao
    /// </summary>
    public class Trilha1 : StringField
    {
        public static short ID = 380;
        public Trilha1(ITerminal terminal) : base(ID, 76, terminal) { }
        public Trilha1(string value) : base(ID, value, 76) { } 
    }

    /// <summary>
    /// Trilha 2 do Cartao
    /// </summary>
    public class Trilha2 : StringField
    {
        public static short ID = 381;
        public Trilha2(ITerminal terminal) : base(ID, 37, terminal) { }
        public Trilha2(string value) : base(ID, value, 37) { }
    }

    /// <summary>
    /// Texto para exibição no visor do cliente 
    /// </summary>
    public class TextoParaExibicaoVisorCliente : StringField
    {
        public static short ID = 409;
        public static int LENGTH = 64;
        public TextoParaExibicaoVisorCliente(ITerminal terminal) : base(ID, LENGTH, terminal) { }
        public TextoParaExibicaoVisorCliente(string texto) : base(ID, texto, LENGTH) { }
    }

    /// <summary>
    /// Tipo de Terminal
    /// </summary>
    public class TipoDeTerminal : NumericField
    {
        public static short ID = 527;
        public TipoDeTerminal(ITerminal terminal) : base(ID, 2, terminal) { }
        private TipoDeTerminal(int tipo) : base(ID, tipo, 2) { }

        public static TipoDeTerminal FRENTE_DE_LOJA = new TipoDeTerminal(1);
        public static TipoDeTerminal MAGAZINE = new TipoDeTerminal(2);
        public static TipoDeTerminal RETAGUARDA = new TipoDeTerminal(3);
        public static TipoDeTerminal POSTO_DE_GASOLINA = new TipoDeTerminal(4);
        public static TipoDeTerminal WEB = new TipoDeTerminal(5);
        public static TipoDeTerminal TELEVENDAS = new TipoDeTerminal(6);
        public static TipoDeTerminal MOBILE = new TipoDeTerminal(7);
        public static TipoDeTerminal RESERVADO = new TipoDeTerminal(8);

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
        public FormasDePagamento(ITerminal terminal) : base(ID, LENGTH, terminal) { }
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
