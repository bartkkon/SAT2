﻿using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.InterTable
{
    public class Action_Tag_InterTable
    {
        public int ActionID { get; set; }
        public Action_DB Action { get; set; }

        public int TagID { get; set; }
        public Tag_DB Tag { get; set; }
    }
}
