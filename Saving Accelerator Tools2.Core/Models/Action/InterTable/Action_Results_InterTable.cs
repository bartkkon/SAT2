using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_Results_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int ResultID { get; set; }
        public Calculation_DB Result { get; set; }
    }
}
