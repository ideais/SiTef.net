using System;
using System.Threading.Tasks;

namespace SiTef.net
{
    public interface ITerminal : IDisposable
    {
        void IniciaTransacao();

        void GravaCampo(IntPtr id, string value);

        string LeCampo(int id, int length);

        void Executa(int acao);

        bool ExistemMaisElementos(int campo);

        string DescricaoErro(int erro);

        void FinalizaTerminal();

        void AddDisposeCallback(Func<ITerminal, Task> callback);

        string Id { get; set; }
        string Servidor { get; set; }
        string Empresa { get; set; }
    }
}
