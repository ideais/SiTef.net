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
        IList<IField> GetFields();

    }
}
