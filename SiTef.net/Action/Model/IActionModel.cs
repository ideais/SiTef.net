using SiTef.net.Type;
using System.Collections.Generic;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Defina as entradas e saidas de uma ação do SiTef
    /// </summary>
    public interface IActionRequest
    {
        /// <summary>
        /// Coleção de campos lidas do terminal apos a execução da Ação
        /// </summary>
        /// <returns>
        ///     A list of Fields defined for the Action
        /// </returns>
        IList<IField> GetFields();
    }

    public interface IActionResponse : IActionRequest
    {
        /// <summary>
        /// Indica se a requisição teve sucesso ou não, examinando os campos
        /// de resposta. Ex: Codigo de Resposta SiTef, Codigo de Resposta da Instituição, etc...
        /// </summary>
        /// <returns></returns>
        bool Failure();

        /// <summary>
        /// Em caso de sucesso ou erro, retorna a mensagem do SiTef
        /// </summary>
        /// <returns></returns>
        string Message();
    }
}
