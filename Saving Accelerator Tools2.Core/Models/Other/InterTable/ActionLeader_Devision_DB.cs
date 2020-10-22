using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other.InterTable
{
    public class ActionLeader_Devision_DB
    {
        public int LeaderID { get; set; }
        public ActionLeader_DB Leader { get; set; }

        public int DevisionID { get; set; }
        public Devision_DB Devision { get; set; }
    }
}
