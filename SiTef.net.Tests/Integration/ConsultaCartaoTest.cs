using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class ConsultaCartaoTest
    {
        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("10.166.58.10", "00000000");
        }

        [TestMethod]
        public void ExecuteConsultaCartaoTest()
        {
            using (var term = factory.NewInstance())
            {
                ConsultaCartaoAction action = new ConsultaCartaoAction(term);

                ConsultaCartaoResponse response = action.Execute(
                    new ConsultaCartaoRequest(
                        new NumeroDoCartao("4000000000000044"),
                        new DataDeVencimento("1215")
                    )
                );
                foreach (var field in response.GetFields())
                    System.Console.WriteLine(field);
            }
        }
    }
}
