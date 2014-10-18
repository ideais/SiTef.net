using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;
using SiTef.net.Exceptions;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class VendaTest
    {

        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecuteVendaTest()
        {
            VendaResponse response;
            using (var term = factory.NewInstance())
            {
                term.IniciaTransacao();
                VendaAction action = new VendaAction(term);
                VendaRequest request = new VendaRequest(
                    null,//new DataFiscal(DateTime.Now.AddDays(2)),
                    null,//new HoraFiscal(DateTime.Now),
                    null,//new NumeroDeParcelas(1),
                    null,//new TipoDeFinanciamento(2),
                    new NumeroDoCartao("4024007122405250"),
                    new DataDeVencimento(12, 15),
                    new CodigoDeSeguranca("123"),
                    new Valor(100.00)
                    );
                
                response = action.Execute(request);

                foreach (var field in response.GetFields()) 
                    System.Console.WriteLine(field);
            }

            //Confirmando em outro terminal
            using (var term = factory.NewInstance())
            {
                if (!response.Failure())
                    new FinalizaTransacaoAction(term).Execute(new CancelaVenda(response.DadosDeConfirmacao));   
            }

            Assert.IsFalse(response.Failure()); 
        }
    }
}
