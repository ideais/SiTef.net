using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class QueryCardResponse : AbstractActionModel
    {

        public QueryCardResponse(Terminal terminal)
        {
            _fields = new List<Field>{
                new Field(1,terminal),
                new Field(10,terminal),
                new Field(11,terminal),
                new Field(23,terminal),
                new Field(24,terminal),
                new Field(25,terminal),
                new Field(26,terminal),
                new Field(27,terminal),
                new Field(28,terminal),
                new Field(29,terminal),
                new Field(30,terminal),
                new Field(31,terminal),
                new Field(32,terminal),
                new Field(33,terminal),
                new Field(34,terminal),
                new Field(35,terminal),
                new Field(36,terminal),
                new Field(37,terminal),
                new Field(38,terminal),
                new Field(39,terminal),
                new Field(40,terminal),
                new Field(41,terminal),
                new Field(42,terminal),
                new Field(43,terminal),
                new Field(44,terminal),
                new Field(45,terminal),
                new Field(46,terminal),
                new Field(155,terminal),
                new Field(163,terminal),
                new Field(164,terminal),
                new Field(165,terminal),
                new Field(166,terminal),
                new Field(167,terminal),
                new Field(168,terminal),
                new Field(169,terminal),
                new Field(170,terminal),
                new Field(171,terminal),
                new Field(172,terminal),
                new Field(173,terminal),
                new Field(174,terminal),
                new Field(175,terminal),
                new Field(176,terminal),
                new Field(177,terminal),
                new Field(178,terminal),
                new Field(179,terminal),
                new Field(180,terminal),
                new Field(181,terminal),
                new Field(182,terminal),
                new Field(236,terminal),
                new Field(237,terminal),
                new Field(239,terminal),
                new Field(241,terminal),
                new Field(242,terminal),
                new Field(243,terminal),
                new Field(244,terminal),
                new Field(245,terminal),
                new Field(246,terminal),
                new Field(350,terminal),
                new Field(351,terminal),
                new Field(352,terminal),
                new Field(353,terminal),
                new Field(354,terminal),
                new Field(561,terminal),
                new Field(562,terminal),
                new Field(563,terminal),
                new Field(564,terminal),
                new Field(578,terminal),
                new Field(579,terminal),
            };
        }

    }
}
