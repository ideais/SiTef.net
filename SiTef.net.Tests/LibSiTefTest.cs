using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SiTef.net.Tests
{
    [TestClass]
    public class LibSiTefTest
    {

        protected static IntPtr tef;
        
        [ClassInitialize]
        static public void Init(TestContext context)
        {
            tef = SiTef.IniciaTerminal("127.0.0.1", "AA999999", "00000000");
        }

        [TestMethod]
        public void IniciaTerminalTest()
        {
            Assert.IsTrue(IntPtr.Zero != tef);
        }

        [TestMethod]
        public void IniciaTransacaoTest()
        {
            int trn = SiTef.IniciaTransacao(tef);
            Assert.IsTrue(trn >= 0);
        }

        [TestMethod]
        public void GravaCampo()
        {
            int trn = SiTef.IniciaTransacao(tef);
            Assert.IsTrue(trn >= 0);
            int status = SiTef.GravaCampo(tef, (IntPtr)1, "   1");
            Assert.IsTrue(status >= 0);
        }
    }
}
