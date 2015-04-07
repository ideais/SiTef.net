using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace SiTef.net.Pool.MongoDB.Tests
{
    [TestClass]
    public class MongoDBTerminalPoolTest
    {

        static TerminalPool pool;

        static string CREATE_TERMS =
@"
for( var i = 0; i <= 9; i++ ){
    var n = i + '';
    var terminal = n.length >= 8 ? n : new Array( 8 - n.length + 1).join('0') + n;
    db.terminais.insert({
        Terminal : terminal,
        Servidor : '127.0.0.1',
        Empresa : '00000000'
    });
};   
";

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var client = new MongoClient("mongodb://127.0.0.1");
            var db = client.GetDatabase("test");
            db.DropCollectionAsync("terminais");
            var collection = db.GetCollection<MongoTerminalLease>("terminais");

            for (int i = 0; i < 8; i++)
            {
                collection.InsertOneAsync(new MongoTerminalLease
                {
                    Empresa = "00000000",
                    Terminal = i.ToString(),
                    Servidor = "127.0.0.1"
                });
            }

            ITerminalRepository repo = new MongoDBTerminalRepository() { Database = db, Collection = "terminais" };
            pool = new TerminalPool("1") { Empresa = "00000001", Server = "0.0.0.0", Repository = repo };
        }

        [TestMethod]
        public async Task TestLease()
        {
            using (var term = await pool.GetTerminalAsync())
            {
                Assert.AreEqual("127.0.0.1", term.Servidor);
                Assert.AreEqual("00000000", term.Empresa);
            }

            using (var term = await pool.GetTerminalAsync())
            {
                Assert.AreEqual("127.0.0.1", term.Servidor);
                Assert.AreEqual("00000000", term.Empresa);
            }

            using (var term = await pool.GetTerminalAsync())
            {
                Assert.AreEqual("127.0.0.1", term.Servidor);
                Assert.AreEqual("00000000", term.Empresa);
            }
        }

        [TestMethod]
        public async Task TestReclaim()
        {
           await pool.ReclaimTerminalsAsync();
        }
    }
}
