using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SiTef.net.Tests
{
    [TestClass]
    public class LibSiTefTest
    {

        protected static UIntPtr tef;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            tef = SiTef.IniciaTerminal("10.166.58.10", "AA999999", "00000000");
        }
        
        [TestMethod]
        public void IniciaTransacaoTest()
        {
            short trn = SiTef.IniciaTransacao(tef);
            Assert.IsTrue(trn >= 0);
        }

        [TestMethod]
        public void GravaCampo()
        {
            short trn = SiTef.IniciaTransacao(tef);
            Assert.IsTrue(trn >= 0);
            short status = SiTef.GravaCampo(tef, 1, "   1");
            Assert.IsTrue(status >= 0);
        }
    }
}
