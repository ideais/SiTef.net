using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        void AddDisposeCallback(System.Action<string> callback);

    }
}
