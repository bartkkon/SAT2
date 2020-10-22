using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers
{
    public class RoleController
    {
        public static ICollection<Role_DB> LoadAllRole()
        {
            ICollection<Role_DB> AllRole;
            var context = new DataBaseConnetionContext();
            AllRole = context.Role.Where(u => u.Active == true).ToList();
            return AllRole;
        }
    }
}
