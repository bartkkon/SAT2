using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users
{
    public class UserPlant_DB
    {
        public int UserID { get; set; }
        public Users_DB User { get; set; }

        public int PlantID { get; set; }
        public Plant_DB Plant { get; set; }
    }
}
