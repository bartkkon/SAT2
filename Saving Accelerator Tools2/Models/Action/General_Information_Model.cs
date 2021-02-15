using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Models.Action
{
    public class General_Information_Model
    {
        public int ID { get; set; }
        public int ActionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal StartYear { get; set; } = DateTime.UtcNow.Year;
        public int Month { get; set; } = DateTime.UtcNow.Month;
        public bool Active { get; set; } = true;

        public Devision_DB Devision { get; set; }
        public Plant_DB Plant { get; set; }
        public ActionLeader_DB Leader { get; set; }
        public Tag_DB Tag { get; set; }

    }
}
