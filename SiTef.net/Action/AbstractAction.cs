﻿using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Action
{
    public abstract class AbstractAction<M, N> : IAction<M, N>
        where M : IActionRequest
        where N : IActionResponse
    {
        protected ITerminal _terminal;

        private short _action;

        public AbstractAction(short action, ITerminal terminal)
        {
            _action = action;
            _terminal = terminal;
        }

        public N Execute(M model)
        {
            _terminal.IniciaTransacao();

            foreach (IField field in model.GetFields())
            {
                if (field != null) field.WriteTo(_terminal);
            }

            _terminal.Executa(_action);

            N response = ReadOutput();

            return response;
        }

        protected abstract N ReadOutput();
    }
}
