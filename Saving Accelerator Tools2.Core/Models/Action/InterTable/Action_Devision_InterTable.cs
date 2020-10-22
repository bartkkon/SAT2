using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_Devision_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int DevisionID { get; set; }
        public Devision_DB Devision { get; set; }
    }
}
