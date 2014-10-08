using SiTef.net;
using SiTef.net.Action.Model;
using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public abstract class BaseAction<M> : IAction<M> where M : IActionModel
    {
        private Terminal _terminal;

        private IList<Field> _input;
        public IList<Field> Fields { set { _input = value; } }

        private short _action;

        public BaseAction(short action, Terminal terminal)
        {
            _action = action;
            _terminal = terminal;
        }

        public M Execute(M model)
        {
            foreach (Field field in _input)
            {
                _terminal.GravaCampo(field);
            }

            _terminal.Executa(_action);

            return ReadOutput();
        }

        protected abstract M ReadOutput();
    }
}
