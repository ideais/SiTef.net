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
            tef = SiTefWrapper.IniciaTerminal("10.166.58.10", "AA999999", "00000000");
        }
        
        [TestMethod]
        public void IniciaTransacaoTest()
        {
            Int16 trn = SiTefWrapper.IniciaTransacao(tef);
            Assert.IsTrue(trn >= 0);
        }
    }
}
