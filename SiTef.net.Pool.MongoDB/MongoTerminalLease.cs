using MongoDB.Bson;
using SiTef.net.Pool.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
