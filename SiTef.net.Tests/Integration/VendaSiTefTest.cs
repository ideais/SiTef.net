using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class VendaSiTefTest
    {

        static Terminal term;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            term = new Terminal("10.166.58.10", "00000001", "00000000");
        }

        [TestMethod]
        public void ExecuteVendaSiTefTest()
        {
            term.IniciaTransacao();
            VendaSiTefAction action = new VendaSiTefAction(term);
            VendaSiTefRequest request = new VendaSiTefRequest(
                new NumeroDeParcelas("1"),
                new TipoDeFinanciamento("2"),
                new NumeroDoCartao("4716615017626757"),
                new DataDeVencimento("1215"),
                new CodigoDeSeguranca("123"),
                new Valor("10000")
                );
            VendaSiTefResponse response = action.Execute(request);

            foreach (var field in response.GetFields())
                System.Console.WriteLine(field);
            
            term.FinalizaTerminal();
        }
    }
}
