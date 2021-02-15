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
        
        public static ICollection<Devision_DB> LoadForAction()
        {
            var DevisionList = new List<Devision_DB>();

            foreach (var Devision in User.User.Logged.User_Devisions) {
                DevisionList.Add(Devision.Devisions);
            }

            return DevisionList;
        }

        public static ICollection<Devision_DB> LoadDevisions()
        {
            var context = new DataBaseConnetionContext();
            return context.Devision.Where(u => u.Active == true).ToList();
        }

        public static Devision_DB LoadByName(string DevisonName)
        {
            var context = new DataBaseConnetionContext();
            return context.Devision.Where(u => u.Active == true && u.Devision == DevisonName).FirstOrDefault();
        }
    }
}
