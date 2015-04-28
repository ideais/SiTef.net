using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Pool;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class ConsutaTicketCulturaTest
    {
        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecuteConsultaValeCulturaTest()
        {
            var term = factory.NewInstance();
            ConsultaTicketCulturaAction action = new ConsultaTicketCulturaAction(term);

            ConsultaTicketCulturaResponse response = action.Execute(
                new ConsultaTicketCulturaRequest(new NumeroDoCartao("5899111111312697"))
                );
            foreach (var field in response.GetFields())
                System.Console.WriteLine(field);

        }
    }
}
