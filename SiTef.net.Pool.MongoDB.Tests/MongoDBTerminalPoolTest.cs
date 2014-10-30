using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using SiTef.net.Pool.model;

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
            var db = client.GetServer().GetDatabase("test");
            db.DropCollection("terminais");
            db.Eval(new EvalArgs() { Code = CREATE_TERMS, Lock = true });
            ITerminalRepository repo = new MongoDBTerminalRepository() { Database = db, Collection = "terminais" };
            pool = new TerminalPool("1") { Empresa = "00000001", Server = "0.0.0.0", Repository = repo };
        }

        [TestMethod]
        public void TestLease()
        {
            using (ITerminal term = pool.GetTerminal())
            {
                Assert.AreEqual("127.0.0.1", term.Servidor);
                Assert.AreEqual("00000000", term.Empresa);
            }

            using (ITerminal term = pool.GetTerminal())
            {
                Assert.AreEqual("127.0.0.1", term.Servidor);
                Assert.AreEqual("00000000", term.Empresa);
            }
        }
    }
}
