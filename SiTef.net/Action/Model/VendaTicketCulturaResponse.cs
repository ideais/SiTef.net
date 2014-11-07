using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class VendaTicketCulturaResponse : AbstractActionModel, IActionResponse
    {

        public VendaTicketCulturaResponse(ITerminal terminal)
            : base()
        {

        }

        public bool Failure()
        {
            throw new NotImplementedException();
        }

        public string Message()
        {
            throw new NotImplementedException();
        }
    }
}
