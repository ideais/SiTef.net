using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class QueryCardTest
    {
        static Terminal term;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            term = new Terminal("10.166.58.10", "00000001", "00000000");
        }

        [TestMethod]
        public void ExecuteQueryActionTest()
        {
            term.IniciaTransacao();
            ConsultaCartaoAction action = new ConsultaCartaoAction(term);

            ConsultaCartaoResponse response = action.Execute(
                new ConsultaCartaoRequest(
                    //Rede.CIELO,
                    //new DataFiscal("13102014"),
                    //new HoraFiscal("101010"),
                    new NumeroDoCartao("4000000000000044"),
                    new DataDeVencimento("1215")
                )
            );
            foreach (var field in response.GetFields())
                System.Console.WriteLine(field);
        }
    }
}
