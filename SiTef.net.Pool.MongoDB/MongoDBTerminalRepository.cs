using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiTef.net.Pool.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Pool.MongoDB
{
    public class MongoDBTerminalRepository : ITerminalRepository
    {

        public MongoDatabase Database { get; set; }

        public string Collection { get; set; }

        public TerminalLease Lease(string id)
        {
            var collection = Database.GetCollection(Collection);
            var query = Query.EQ("LeasedTo", BsonNull.Value);
            var update = Update.Set("LeasedTo", id).
                    Set("LeasedAt", DateTime.Now);
            var args = new FindAndModifyArgs { Query = query, Update = update };
            var result = collection.FindAndModify(args);
            return result.GetModifiedDocumentAs<MongoTerminalLease>();
        }

        public void Release(string terminalId)
        {
            var collection = Database.GetCollection(Collection);
            var query = Query.EQ("Id", terminalId);
            var update = Update.Set("LeasedTo", BsonNull.Value);
            var args = new FindAndModifyArgs { Query = query, Update = update };
            collection.FindAndModify(args);
        }
    }
}
