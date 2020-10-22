using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Users;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other
{
    public class Devision_DB
    {
        [Key]
        public int DevisionID { get; set; }
        public string Devision { get; set; }
        public bool Active { get; set; }

        public virtual List<User_Devision_DB> User_Devisions { get; set; } = new List<User_Devision_DB>();
        public virtual List<ActionLeader_Devision_DB> Leader_Devisions { get; set; } = new List<ActionLeader_Devision_DB>();
        public virtual List<Action_Devision_InterTable> Action_Devision { get; set; } = new List<Action_Devision_InterTable>();


        /* 1    Electronic
         * 2    Mechanic
         * 3    NVR
         */
    }
}
