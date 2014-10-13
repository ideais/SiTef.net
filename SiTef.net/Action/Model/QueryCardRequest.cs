using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class QueryCardRequest: AbstractActionModel
    {

        public QueryCardRequest(params Field[] fields) : base( fields ) { }

    }
}
