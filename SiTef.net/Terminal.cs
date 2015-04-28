using SiTef.net.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net
{
    /// <summary>
    /// Wrapper para operações utilizando a LibSitef
    /// </summary>
    public class Terminal : ITerminal
    {
        private IntPtr _term;

        private int _transaction;

        private List<Action<ITerminal>> _disposeCallbacks;

        private string _terminal;
        public string Id { get { return _terminal; } set { _terminal = value; } }
        public string Empresa { get; set; }
        public string Servidor { get; set; }

        public Terminal(string servidor, string terminal, string empresa)
        {
            _terminal = terminal;
            Servidor = servidor;
            Empresa = empresa;
            _term = SiTef.IniciaTerminal(servidor, terminal, empresa);
            _disposeCallbacks = new List<Action<ITerminal>>();
            if (IntPtr.Zero == _term)
                throw new TerminalException("unable to initialize terminal");
            IniciaTransacao();
        }

        public void IniciaTransacao()
        {
            _transaction = SiTef.IniciaTransacao(_term);
            if (_transaction < 0)
                throw new TerminalException(DescricaoErro(_transaction));
        }

        public void GravaCampo(IntPtr id, string value)
        {
            int result = SiTef.GravaCampo(_term, id, value);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));

        }

        public void Executa(int acao)
        {
            int result = SiTef.Executa(_term, (IntPtr)acao);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));
        }

        public String LeCampo(int id, int length)
        {
            StringBuilder valor = new StringBuilder(length);
            int result = SiTef.LeCampo(_term, (IntPtr)id, valor);
            if (result < 0)
                throw new TerminalException(DescricaoErro(result));
            if (valor.Length < length) length = valor.Length;
            return valor.ToString().Substring(0, length);
        }

        public bool ExistemMaisElementos(int campo)
        {
            return SiTef.ExistemMaisElementos(_term, campo) == 1;
        }


        public string DescricaoErro(int erro)
        {
            StringBuilder descricao = new StringBuilder(127);
            SiTef.DescricaoErro(_term, (IntPtr)erro, descricao);
            return descricao.ToString();
        }

        public void FinalizaTerminal()
        {
            SiTef.FinalizaTerminal(_term);
        }


        public Task ReleaseAsync()
        {
            SiTef.FinalizaTerminal(_term);
            return Task.Run(() =>
            {
                foreach (var action in _disposeCallbacks)
                    action(this);
            });
            
        }

        public void Release() {
            SiTef.FinalizaTerminal(_term);
            foreach (var action in _disposeCallbacks)
                action(this);
        }

        public void AddDisposeCallback(Action<ITerminal> callback)
        {
            _disposeCallbacks.Add(callback);
        }
    }
}
