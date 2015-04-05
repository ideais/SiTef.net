using MongoDB.Bson;
using SiTef.net.Pool.Model;

namespace SiTef.net.Pool.MongoDB
{
    public class MongoTerminalLease : TerminalLease
    {
        /// <summary>
        /// Identificação do objeto no MongoDB;
        /// </summary>
        public ObjectId Id { get; set; }
    }
}
