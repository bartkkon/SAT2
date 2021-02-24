using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_PNC_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int PNCID { get; set; }
        public CalcByPNC List { get; set; }

        
    }
}
