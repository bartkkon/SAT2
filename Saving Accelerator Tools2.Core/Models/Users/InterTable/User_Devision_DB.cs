using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users.InterTable
{
    public class User_Devision_DB
    {
        public int UserID { get; set; }
        public Users_DB Users { get; set; }

        public int DevisionID { get; set; }
        public Devision_DB Devisions { get; set; }
    }
}
