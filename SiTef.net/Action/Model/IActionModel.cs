using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    public interface IActionModel
    {
        IList<Field> GetFields();

    }
}
