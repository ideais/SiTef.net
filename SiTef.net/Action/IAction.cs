using SiTef.net.Action.Model;
using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action
{
    public interface IAction<M,N> where M : IActionRequest where N : IActionResponse
    {
        N Execute(M model);
    }
}
