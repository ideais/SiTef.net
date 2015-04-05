using System.Collections.Generic;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Para actions que não tem retorno
    /// </summary>
    public class NullResponse : IActionResponse
    {
        static readonly IList<Type.IField> Empty = new List<Type.IField>();

        public bool Failure()
        {
            return false;
        }

        public string Message()
        {
            return null;
        }

        public IList<Type.IField> GetFields()
        {
            return Empty;
        }
    }
}
