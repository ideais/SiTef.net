using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiTef.net.Action.Model
{
    /// <summary>
    /// Define the input / output of a SiTef Action.
    /// </summary>
    public interface IActionModel
    {

        /// <summary>
        /// The fields defined for this action. These might be fields defined by user input, or read from the SiTef Service.
        /// </summary>
        /// <returns>
        ///     A list of Fields defined for the Action
        /// </returns>
        IList<Field> GetFields();

        /// <summary>
        /// Write the fields into the SiTef terminal. The terminal must be already initialized, and reset with
        /// the appropriated MKT_Inicia_Transacao call.
        /// </summary>
        /// <param name="terminal">The instance we are writing the fields into</param>
        void WriteTo(Terminal terminal);

    }
}
