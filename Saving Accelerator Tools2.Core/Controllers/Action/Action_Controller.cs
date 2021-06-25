using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;

namespace Saving_Accelerator_Tools2.Core.Controllers.Action
{
    public class Action_Controller
    {
        public static Action_DB Load(int ID)
        {
            var LoadedAction = new Action_DB();
            var context = new DataBaseConnetionContext();

            LoadedAction = context.Action.Where(u => u.ActionID == ID)
                .Include(c => c.Action_Devision).ThenInclude(b => b.Devision)
                .Include(c => c.Action_Leader).ThenInclude(b => b.Leader)
                .Include(c => c.Action_Plant).ThenInclude(b => b.Plant)
                .Include(c => c.Action_Tag).ThenInclude(b => b.Tag)
                .Include(c => c.Action_ANCChange).ThenInclude(b => b.ANCChange)
                .Include(c => c.Action_PNC).ThenInclude(b => b.List)
                .Include(c => c.Action_ANCChange_Platform).ThenInclude(b => b.Platform)
                .Include(c => c.Action_ANCChange_Items).ThenInclude(b => b.Item)
                .Include(c => c.Action_PNCSpecial).ThenInclude(b => b.PNCSpecial)
                .Include(c => c.Action_Results).ThenInclude(b => b.Result)
                .FirstOrDefault();


            return LoadedAction;
        }

        public static Action_DB Load_Active(int ID)
        {
            var LoadedAction = new Action_DB();
            var context = new DataBaseConnetionContext();

            LoadedAction = context.Action.Where(u => u.ActionID == ID && u.ActiveAction == true)
                .Include(c => c.Action_Devision).ThenInclude(b => b.Devision)
                .Include(c => c.Action_Leader).ThenInclude(b => b.Leader)
                .Include(c => c.Action_Plant).ThenInclude(b => b.Plant)
                .Include(c => c.Action_Tag).ThenInclude(b => b.Tag)
                .Include(c => c.Action_ANCChange).ThenInclude(b => b.ANCChange)
                .Include(c => c.Action_PNC).ThenInclude(b => b.List)
                .Include(c => c.Action_ANCChange_Platform).ThenInclude(b => b.Platform)
                .Include(c => c.Action_ANCChange_Items).ThenInclude(b => b.Item)
                .Include(c => c.Action_PNCSpecial).ThenInclude(b => b.PNCSpecial)
                .Include(c => c.Action_Results).ThenInclude(b => b.Result)
                .FirstOrDefault();


            return LoadedAction;
        }

        public static Action_DB Load(int ID, DataBaseConnetionContext context)
        {
            var LoadedAction = new Action_DB();
            //var context = new DataBaseConnetionContext();

            LoadedAction = context.Action.Where(u => u.ActionID == ID)
                .Include(c => c.Action_Devision).ThenInclude(b => b.Devision)
                .Include(c => c.Action_Leader).ThenInclude(b => b.Leader)
                .Include(c => c.Action_Plant).ThenInclude(b => b.Plant)
                .Include(c => c.Action_Tag).ThenInclude(b => b.Tag)
                .Include(c => c.Action_ANCChange).ThenInclude(b => b.ANCChange)
                .Include(c => c.Action_PNC).ThenInclude(b => b.List)
                .Include(c => c.Action_ANCChange_Platform).ThenInclude(b => b.Platform)
                .Include(c => c.Action_ANCChange_Items).ThenInclude(b => b.Item)
                .FirstOrDefault();


            return LoadedAction;
        }

        public static int NewActionNumber()
        {
            int MaxNumber = 0;
            var context = new DataBaseConnetionContext();

            MaxNumber = context.Action.Max(b => b.ActionID);

            return MaxNumber + 1;
        }

        public static IEnumerable<Action_DB> LoadToTree(int Devision, decimal Year, bool PLVPlant, bool ZMPlant, string UserName, bool Active)
        {
            var context = new DataBaseConnetionContext();
            var ActionList = new List<Action_DB>();

            if (UserName == "All" && PLVPlant && ZMPlant) {
                ActionList = context.Action.Where(
                    b => b.StartYear == Year &&
                    b.Action_Devision.Any(c => c.DevisionID == Devision) &&
                    b.Active == Active &&
                    b.ActiveAction == true)
                    .ToList();
            }
            else if (UserName == "All") {
                if (PLVPlant) {
                    ActionList = context.Action.Where(
                        b => b.StartYear == Year &&
                        b.Action_Devision.Any(c => c.DevisionID == Devision) &&
                        b.Action_Plant.Any(c => c.PlantID == 1) &&
                        b.Active == Active &&
                        b.ActiveAction == true)
                        .ToList();
                }
                else if (ZMPlant) {
                    ActionList = context.Action.Where(
                        b => b.StartYear == Year &&
                        b.Action_Devision.Any(c => c.DevisionID == Devision) &&
                        b.Action_Plant.Any(c => c.PlantID == 2) &&
                        b.Active == Active &&
                        b.ActiveAction == true)
                        .ToList();
                }
            }
            else {
                if (PLVPlant && ZMPlant) {
                    ActionList = context.Action.Where(
                        b => b.StartYear == Year &&
                        b.Action_Devision.Any(c => c.DevisionID == Devision) &&
                        b.Action_Leader.Any(c => c.Leader.FullName == UserName) &&
                        b.Active == Active &&
                        b.ActiveAction == true)
                        .ToList();
                }
                else if (PLVPlant) {
                    ActionList = context.Action.Where(
                        b => b.StartYear == Year &&
                        b.Action_Devision.Any(c => c.DevisionID == Devision) &&
                        b.Action_Plant.Any(c => c.PlantID == 1) &&
                        b.Action_Leader.Any(c => c.Leader.FullName == UserName) &&
                        b.Active == Active &&
                        b.ActiveAction == true)
                        .ToList();
                }
                else if (ZMPlant) {
                    ActionList = context.Action.Where(
                        b => b.StartYear == Year &&
                        b.Action_Devision.Any(c => c.DevisionID == Devision) &&
                        b.Action_Plant.Any(c => c.PlantID == 2) &&
                        b.Action_Leader.Any(c => c.Leader.FullName == UserName) &&
                        b.Active == Active &&
                        b.ActiveAction == true).
                        ToList();
                }
            }

            return ActionList;
        }

        public static bool NewAction(Action_DB NewAction)
        {
            using var context = new DataBaseConnetionContext();
            context.Add(NewAction);
            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }

        }

        public static bool UpdateAction(Action_DB UpdateAction)
        {
            using var context = new DataBaseConnetionContext();
            context.Update(UpdateAction);
            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool Save(Action_DB NewAction, Action_DB OldAction)
        {
            var context = new DataBaseConnetionContext();

            context.Action.Add(NewAction);
            if(OldAction.ID != 0) {
                context.Action.Update(OldAction);
            }

            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public static void RemoveInterTable(Action_Leader_InterTable what, Action_Leader_InterTable NewDevision)
        {
            using var context = new DataBaseConnetionContext();
            context.Remove(what);

            context.SaveChanges();
        }
    }
}
