using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SiTef.net
{
    /// <summary>
    /// Wrapper que dá acesso às funções da LibSiTef. Utilize se quizer fazer o acesso
    /// de forma mais direta aos serviços da Software Express.
    /// 
    /// LibSiTef.dll 1.1.83.0
    /// </summary>
    public class SiTefWrapper
    {
        /// <summary>
        /// Esta função atua como um construtor, e retorna um handle que deverá ser usado nas demais funções.
        /// Desta forma, para cada chamada à MKT_Inicia_Terminal bem-sucedida,
        /// deve-se chamar a MKT_Finaliza_Terminal correspondente no final de sua utilização. 
        /// </summary>
        /// <param name="servidor">
        ///     Endereço IP da estação onde está o SiTef.
        /// </param>
        /// <param name="terminal">
        ///     Identificação do terminal no formato: "AA999999"
        /// </param>
        /// <param name="empresa">
        ///     Código da empresa para qual se está efetuando a transação. Se estiver no ambiente de única empresa este campo deve conter zeros em ASCII (“00000000”). 
        /// </param>
        /// <returns>
        ///     <> 0, ID_TEF, identificador que deve ser utilizado nas demais funções. = 0, falha ao iniciar TEF. 
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Inicia_Terminal", SetLastError = true, CharSet = CharSet.Ansi)]
        public unsafe static extern Int16 IniciaTerminal(string servidor, string terminal, string empresa);

        /// <summary>
        /// Esta função indica que uma nova transação será iniciada,
        /// e deve ser a primeira a ser chamada antes da gravação dos campos de qualquer transação.
        /// Ele reinicia as variáveis internas relativas à última transação.
        /// </summary>
        /// <param name="tef">
        ///     Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal
        /// </param>
        /// <returns>
        ///    >= 0, sucesso < 0 erro.  
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Inicia_Transacao", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern Int16 IniciaTransacao(Int16 tef);

        /// <summary>
        /// Salva na DLL um campo que será utilizado nas demais funções.
        /// Várias chamadas para esta função para um mesmo campo sem que entre elas seja iniciada uma nova transação indica repetição. 
        /// </summary>
        /// <param name="tef">
        ///     Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal 
        /// </param>
        /// <param name="campo">
        ///     Identificador do campo que se deseja gravar. Vide Anexo I, Tabela de Campos.
        /// </param>
        /// <param name="valor">
        ///     Conteúdo que será atribuído ao campo
        /// </param>
        /// <returns>
        ///     >= 0, se conseguiu gravar o campo. < 0, se não foi possível gravar o campo.
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Grava_Campo", SetLastError = true)]
        public unsafe static extern Int16 GravaCampo(Int16 tef, Int16 campo, string valor);

        /// <summary>
        /// Indica se existe algum elemento a ser lido em um dado campo, campo que pode ser simples ou múltiplo.
        /// Enquanto esta função retornar 1 deve ser chamada a função MKT_Le_Campo para leitura do próxima elemento.
        /// Utilizar este método é uma forma alternativa àquela descrita no item 1.1 deste manual,
        /// em que a função MKT_Le_Campo é chamada novamente para o mesmo campo sempre que o seu retorno for maior do que 0. 
        /// </summary>
        /// <param name="tef">
        ///     Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal 
        /// </param>
        /// <param name="campo">
        ///     Identificador do campo que se deseja ler. Vide Anexo I, Tabela de Campos.
        /// </param>
        /// <returns>
        ///     Retorna 1 no caso de existirem mais elementos a serem lidos do dado campo ou 0 no caso de não existirem.
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Existem_Mais_Elementos", SetLastError = true)]
        public unsafe static extern Int16 ExistemMaisElementos(Int16 tef, Int16 campo);

        /// <summary>
        /// Obtém da DLL um campo que foi gerado como resultado de uma transação com o SiTef. O conteúdo de um campo será retornado somente um única vez. 
        /// </summary>
        /// <param name="tef">
        ///     Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal 
        /// </param>
        /// <param name="campo">
        ///     Identificador do campo que se deseja ler. Vide Anexo I, Tabela de Campos.
        /// </param>
        /// <param name="valor">
        ///     Área onde será retornado o campo solicitado. Deve ser preenchido com brancos
        /// </param>
        /// <returns>
        ///     = 0, indica que o campo foi obtido com sucesso e não há novas ocorrências.
        ///     > 0, indica que o campo foi obtido com sucesso e uma nova chamada a função retornará a próxima ocorrência.
        ///     < 0, se não foi possível obter o campo. 
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Le_Campo", SetLastError = true)]
        public unsafe static extern Int16 LeCampo(Int16 tef, Int16 campo, StringBuilder valor);

        /// <summary>
        /// Executa uma determinada ação na biblioteca, como uma venda ou um cancelamento. 
        /// </summary>
        /// <param name="tef">
        ///     Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal 
        /// </param>
        /// <param name="acao">
        ///     Identificador da ação que se deseja executar. Vide Anexo I, Tabela de Ações.
        /// </param>
        /// <returns>
        ///     >= 0, se conseguiu realizar a transação com o SiTef.
        ///     < 0, falha de comunicação. 
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Executa", SetLastError = true)]
        public unsafe static extern Int16 Executa(Int16 tef, Int16 acao);

        /// <summary>
        /// Libera todos os recursos utilizados por um terminal. Atua como um destrutor,
        /// e deve ser chamada quando um determinado terminal não mais realizará transações.
        /// </summary>
        /// <param name="tef">
        ///     Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal 
        /// </param>
        /// <returns>
        ///     >= 0, se conseguiu realizar a transação com o SiTef.
        ///     < 0, se falha de comunicação. 
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Finaliza_Terminal", SetLastError = true)]
        public unsafe static extern Int16 FinalizaTerminal(Int16 tef);

        /// <summary>
        /// Obtém a descrição de um determinado código de erro retornado por qualquer uma das rotinas descritas anteriormente.
        /// </summary>
        /// <param name="tef">
        /// Índice que identifica o terminal solicitante da transação. Obtido na chamada a função MKT_Inicia_Terminal 
        /// </param>
        /// <param name="erro">
        ///     Índice do erro a ser consultado
        /// </param>
        /// <param name="descricao">
        ///     Área onde será retornada a descrição do erro consultado. Deve ser preenchido com brancos.
        /// </param>
        /// <returns>
        ///     = 0, se conseguiu realizar a consulta.
        ///     <> 0, se erro.
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Obtem_Descricao_Erro", SetLastError = true)]
        public unsafe static extern Int16 DescricaoErro(Int16 tef, Int16 erro, StringBuilder descricao);

        /// <summary>
        /// Retorna a versão da biblioteca LibSiTef. 
        /// </summary>
        /// <param name="saida">
        ///     Área onde será retornada a versão da biblioteca (ASCII null-terminated). 
        /// </param>
        /// <param name="max">
        ///     Tamanho da área do Buffer. 
        /// </param>
        /// <returns>
        ///     Esta função retorna sempre zero. 
        /// </returns>
        [DllImport("LibSiTef.dll", EntryPoint = "MKT_Obtem_Versao", SetLastError = true)]
        public unsafe static extern Int16 Versao(StringBuilder saida, Int16 max);

    }
}
