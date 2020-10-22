using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers
{
    public class PlantController
    {
        public static Dictionary<int, string> LoadForAction()
        {
            var PlantList = new Dictionary<int, string>();

            foreach (var UserPlant in User.User.Logged.User_Plant) {
                PlantList.Add(UserPlant.PlantID, UserPlant.Plant.Plant);
            }

            return PlantList;
        }

        public static ICollection<Plant_DB> LoadPlants()
        {
            var context = new DataBaseConnetionContext();
            return context.Plant.Where(u => u.Active == true).ToList();
        }
    }
}
