using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_Plant_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int PlantID { get; set; }
        public Plant_DB Plant { get; set; }
    }
}
