using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers
{
    public class DevisionController
    {
        public static Dictionary<int,string> LoadForAction()
        {
            var DevisionList = new Dictionary<int, string>();

            foreach (var Devision in User.User.Logged.User_Devisions) {
                DevisionList.Add(Devision.DevisionID, Devision.Devisions.Devision);
            }

            return DevisionList;
        }

        public static ICollection<Devision_DB> LoadDevisions()
        {
            var context = new DataBaseConnetionContext();
            return context.Devision.Where(u => u.Active == true).ToList();
        }
    }
}
