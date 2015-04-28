using MongoDB.Bson;
using MongoDB.Driver;
using SiTef.net.Pool.Model;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiTef.net.Pool.MongoDB
{
    public class MongoDBTerminalRepository : ITerminalRepository
    {
        public IMongoDatabase Database { get; set; }

        public string Collection { get; set; }



        public async Task<TerminalLease> LeaseAsync(string id)
        {
            var collection = Database.GetCollection<MongoTerminalLease>(Collection);
            var builder = Builders<MongoTerminalLease>.Update;
            var update = builder.Set(t => t.LeasedTo, id).Set(t => t.LeasedAt, DateTime.Now);
            var result = await collection.FindOneAndUpdateAsync(t => t.LeasedTo == null, update);
            return result;
        }

        public async Task ReleaseAsync(string terminalId)
        {
            var collection = Database.GetCollection<MongoTerminalLease>(Collection);
            var builder = Builders<MongoTerminalLease>.Update;
            var update = builder.Unset(t => t.LeasedTo);
            await collection.UpdateOneAsync(t => t.Id == ObjectId.Parse(terminalId), update);
        }


        public async Task<List<TerminalLease>> ReclaimAsync(string id)
        {
            var collection = Database.GetCollection<MongoTerminalLease>(Collection);
            var result = await collection.Find<MongoTerminalLease>(x => x.LeasedTo == id).ToListAsync();
            return result.Select<MongoTerminalLease,TerminalLease>(l => l as TerminalLease).ToList<TerminalLease>();
        }
    }
}
