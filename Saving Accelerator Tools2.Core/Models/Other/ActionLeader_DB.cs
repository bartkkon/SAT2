using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other
{
    public class ActionLeader_DB
    {
        [Key]
        public int LeaderID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }

        public virtual List<ActionLeader_Plant_DB> Leader_Plant { get; set; } = new List<ActionLeader_Plant_DB>();
        public virtual List<ActionLeader_Devision_DB> Leader_Devision { get; set; } = new List<ActionLeader_Devision_DB>();
        public virtual List<Action_Leader_InterTable> Action_Leader { get; set; } = new List<Action_Leader_InterTable>();

    }
}
