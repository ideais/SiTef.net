﻿using SiTef.net;
using SiTef.net.Action.Model;
using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public abstract class AbstractAction<M,N> : IAction<M,N> where M : IActionModel where N : IActionModel
    {
        protected Terminal _terminal;

        private short _action;

        public AbstractAction(short action, Terminal terminal)
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

            return ReadOutput();
        }

        protected abstract N ReadOutput();
    }
}
