using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other.InterTable
{
    public class ActionLeader_Plant_DB
    {
        public int LeaderID { get; set; }
        public ActionLeader_DB  Leader { get; set; }

        public int FactoryID { get; set; }
        public Plant_DB Factory { get; set; }
    }
}
