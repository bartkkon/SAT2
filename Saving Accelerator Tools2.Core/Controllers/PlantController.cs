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
        public static List<Plant_DB> LoadForAction()
        {
            var PlantList = new List<Plant_DB>();

            foreach (var UserPlant in User.User.Logged.User_Plant) {
                PlantList.Add(UserPlant.Plant);
            }

            return PlantList;
        }

        public static ICollection<Plant_DB> LoadPlants()
        {
            var context = new DataBaseConnetionContext();
            return context.Plant.Where(u => u.Active == true).ToList();
        }

        public static Plant_DB LoadByName(string PlantName)
        {
            var context = new DataBaseConnetionContext();
            return context.Plant.Where(u => u.Active == true && u.Plant == PlantName).FirstOrDefault();
        }
    }
}
