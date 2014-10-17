using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    public abstract class AbstractActionModel : IActionRequest
    {

        protected AbstractActionModel()
        {
            _fields = new List<Type.IField>();
        }

        protected AbstractActionModel( params Type.IField[] fields ){
            _fields = new List<Type.IField>(fields);
        }
        
        protected IList<Type.IField> _fields;
        public IList<Type.IField> GetFields()
        {
            return _fields;
        }
     
    }
}
