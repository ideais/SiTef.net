using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;
using SiTef.net.Pool;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class PreAutorizacaoTest
    {

        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecutePreAutorizacao()
        {
            using (var term = factory.NewInstance())
            {
                var action = new PreAutorizacaoAction(term);
                var response = action.Execute(new PreAutorizacaoRequest(
                    null,
                    new DataFiscal(new DateTime()),
                    null,
                    new NumeroDoCartao("4929208425739710"),
                    new DataDeVencimento(12,15),
                    new Valor(100.00),
                    null,
                    new CodigoDeSeguranca("123")
                ));
                foreach (var field in response.GetFields())
                    System.Console.WriteLine(field);
            }
        }
    }
}
