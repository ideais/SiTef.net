using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Pool;
using SiTef.net.Pool.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SiTef.net.Pool.Dynamic.Tests
{
    [TestClass]
    public class DynamicTerminalRepositoryTests
    {
        static DynamicTerminalRepository dynamicTerminalRepository;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            dynamicTerminalRepository = new DynamicTerminalRepository();
        }

        [TestMethod]
        public async Task TestLease()
        {
            var terms = new List<string>();

            for (int i = 0; i < 10000; i++)
            {
                var term = await dynamicTerminalRepository.LeaseAsync(string.Empty);
                bool test = terms.Contains(term.Terminal);
                Assert.IsFalse(test, string.Format("{0} quantidade", terms.Count));
                terms.Add(term.Terminal);
            }
        }
    }
}
