using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Action
{
    public class Action_Controller
    {
        public static void Load(int ID,ref Action_DB _instance)
        {
            //Action_DB LoadedAction = new Action_DB();
            var context = new DataBaseConnetionContext();

            _instance = context.Action.Where(u => u.ID == ID).FirstOrDefault();

            //return LoadedAction;
        }
    }
}
