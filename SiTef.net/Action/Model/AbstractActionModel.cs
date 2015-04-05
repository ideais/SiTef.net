using System.Collections.Generic;

namespace SiTef.net.Action.Model
{
    public abstract class AbstractActionModel : IActionRequest
    {
        protected IList<Type.IField> Fields;

        protected AbstractActionModel()
        {
            Fields = new List<Type.IField>();
        }

        protected AbstractActionModel(params Type.IField[] fields)
        {
            Fields = new List<Type.IField>(fields);
        }

        public IList<Type.IField> GetFields()
        {
            return Fields;
        }
    }
}
