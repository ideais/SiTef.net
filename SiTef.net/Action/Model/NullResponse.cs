using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Para actions que não tem retorno
    /// </summary>
    public class NullResponse : IActionResponse
    {

        static IList<Type.IField> EMPTY = new List<Type.IField>();

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
            return EMPTY;
        }
    }
}
