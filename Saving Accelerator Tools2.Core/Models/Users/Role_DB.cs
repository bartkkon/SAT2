using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users
{
    public class Role_DB
    {
        [Key]
        public int RoleID { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public virtual List<User_Role_DB> User_Role { get; set; } = new List<User_Role_DB>();

        /* 1    Viewer
         * 2    Designer
         * 3    Menager
         * 4    MenagerPC
         * 5    Administrator
         */
    }
}
