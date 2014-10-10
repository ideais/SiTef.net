using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    public abstract class AbstractActionModel : IActionModel
    {

        protected AbstractActionModel()
        {
            _fields = new List<Type.Field>();
        }

        protected AbstractActionModel( params Type.Field[] fields ){
            _fields = new List<Type.Field>(fields);
        }
        
        protected IList<Type.Field> _fields;
        public IList<Type.Field> GetFields()
        {
            return _fields;
        }


        public void WriteTo(Terminal terminal)
        {
            if (_fields != null)
                foreach (var field in _fields)
                    terminal.GravaCampo(field);
        }
    }
}
