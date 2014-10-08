using SiTef.net.Action.Model;
using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    interface IAction<M> where M : IActionModel
    {
        M Execute(M model);
    }
}
