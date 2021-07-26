using Saving_Accelerator_Tools2.Model.Action.Sub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Action.InterTables
{
    public class PNC_InterTable
    {
        public int ActionID { get; set; }
        public ActionBase Action { get; set; }

        public int PNCID { get; set; }
        public PNC PNC { get; set; }
    }
}
