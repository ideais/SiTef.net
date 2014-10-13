using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class SiTefSaleActionTest
    {

        static Terminal term;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            term = new Terminal("10.166.58.10", "00000001", "00000000");
        }

        [TestMethod]
        public void ExecuteSaleActionTest()
        {
            term.IniciaTransacao();
            SiTefSaleAction action = new SiTefSaleAction(term);
            SiTefSaleRequest request = new SiTefSaleRequest(
                new NumeroDeParcelas("1"),
                new TipoDeFinanciamento("2"),
                new NumeroDoCartao("4000000000000044"),
                new DataDeVencimento("1215"),
                new CodigoDeSeguranca("123"),
                new Valor("10000")
                );
            SiTefSaleResponse response = action.Execute(request);

            foreach (var field in response.GetFields())
                System.Console.WriteLine(field);

        }
    }
}
