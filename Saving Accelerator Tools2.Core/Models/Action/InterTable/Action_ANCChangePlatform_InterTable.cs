using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_ANCChangePlatform_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int ChangeID { get; set; }
        public PlatformCalc_DB Platform{ get; set; }
    }
}
