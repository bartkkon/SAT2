using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users
{
    public class Users_DB
    {

        [Key]
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }


        public virtual List<UserPlant_DB> User_Plant { get; set; } = new List<UserPlant_DB>();
        public virtual List<User_Devision_DB> User_Devisions { get; set; } = new List<User_Devision_DB>();
        public virtual List<User_Pages_DB> User_Pages { get; set; } = new List<User_Pages_DB>();
        public virtual List<User_Role_DB> User_Role { get; set; } = new List<User_Role_DB>();

    }
}
