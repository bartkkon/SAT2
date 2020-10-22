using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other
{
    public class Plant_DB
    {
        [Key]
        public int PlantID { get; set; }
        public string Plant { get; set; }
        public bool Active { get; set; }

        public virtual List<UserPlant_DB> User_Plant { get; set; } = new List<UserPlant_DB>();
        public virtual List<ActionLeader_Plant_DB> Leader_Plant { get; set; } = new List<ActionLeader_Plant_DB>();
        public virtual List<Action_Plant_InterTable> Action_Plant { get; set; } = new List<Action_Plant_InterTable>();


        /*1 PLV
         *2 ZM
         */
    }
}
