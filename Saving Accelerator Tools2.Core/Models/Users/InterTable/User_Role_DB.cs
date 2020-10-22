using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users.InterTable
{
    public class User_Role_DB
    {
        public int UserID { get; set; }
        public Users_DB User { get; set; }

        public int RoleID { get; set; }
        public Role_DB Role { get; set; }
    }
}
