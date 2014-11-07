using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Pool;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class VendaTicketCulturaTest
    {

        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecuteVendaTicketCulturaTest()
        {
            using (var term = factory.NewInstance())
            {
                VendaTicketCulturaAction action = new VendaTicketCulturaAction(term);
                VendaTicketCulturaResponse response = action.Execute(
                    new VendaTicketCulturaRequest(
                        new NumeroDoCartao("4024007122405250"),
                        new DataDeVencimento(12, 15),
                        null,
                        null, //CodigoDoRoteamento
                        null, //CodigoDoProduto
                        null, //CodigoLinhaDeCredito
                        new Valor(100)
                        )    
                );
                foreach (var field in response.GetFields())
                    System.Console.WriteLine(field);
            }
        }
    }
}
