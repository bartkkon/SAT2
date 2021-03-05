using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_PNCSpecial_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int PNCSpecID { get; set; }
        public PNCSpecial_PNC_DB PNCSpecial { get; set; }
    }
}
