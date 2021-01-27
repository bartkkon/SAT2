using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Action
{
    public class Action_TreeList_Controller
    {
        public static List<TreeList_Action> Load(bool Electronic, bool Mechanic, bool NVR, bool PLV, bool ZM, decimal Year)
        {
            var ActionList = new List<TreeList_Action>();
            var context = new DataBaseConnetionContext();

            var Actions = context.Action.Where(u => u.StartYear == Year && u.StartYear == (Year - 1))
                                            .Include(Dev => Dev.Action_Devision).ThenInclude(b => b.Devision)
                                            .Include(Plant => Plant.Action_Plant).ThenInclude(b => b.Plant).ToList();

            foreach( var Action in Actions) {
                if (Electronic) {
                    //var 
                }
            }

            return ActionList;
        }
    }

    public class TreeList_Action
    {
        public int ID;
        public string ActionName;
        public string Devision;
        public string Plant;
        public bool CarryOver;
    }

}
