using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Users
{
    public class ActionLeaderController
    {
        public static ICollection<string> Load_WithAll(bool Electronic, bool Mechanic, bool NVR, bool PLV, bool ZM)
        {
            var ActionList = new List<string>();
            var Devisions = new List<Devision_DB>();
            var Factory = new List<Plant_DB>();
            var context = new DataBaseConnetionContext();
            ActionList.Add("All");

            if (Electronic) {
                Devisions.Add(context.Devision.Where(u => u.DevisionID == 1 && u.Active == true).FirstOrDefault());
            }
            if (Mechanic) {
                Devisions.Add(context.Devision.Where(u => u.DevisionID == 2 && u.Active == true).FirstOrDefault());
            }
            if (NVR) {
                Devisions.Add(context.Devision.Where(u => u.DevisionID == 3 && u.Active == true).FirstOrDefault());
            }

            if (PLV) {
                Factory.Add(context.Plant.Where(u => u.PlantID == 1 && u.Active == true).FirstOrDefault());
            }
            if (ZM) {
                Factory.Add(context.Plant.Where(u => u.PlantID == 2 && u.Active == true).FirstOrDefault());
            }

            foreach (var Devision in Devisions) {
                foreach (var Plant in Factory) {
                    var Leaders = context.ActionLeader
                        .Where(u => u.Leader_Devision.Any(c => c.DevisionID == Devision.DevisionID)
                        && u.Leader_Plant.Any(c => c.FactoryID == Plant.PlantID)
                        && u.Active == true)
                        .ToList();

                    foreach (var Leader in Leaders) {
                        if (!ActionList.Any(w => w == Leader.FullName)) {
                            ActionList.Add(Leader.FullName);
                        }
                    }
                }
            }
            return ActionList;
        }

        public static List<ActionLeader_DB> ActionLoad(Devision_DB DevisionKey, Plant_DB PlantKey)
        {
            var context = new DataBaseConnetionContext();

            if (DevisionKey == null || PlantKey == null)
                return null;

            var LeadersBase = context.ActionLeader.Where(c => c.Leader_Devision.Any(u => u.DevisionID == DevisionKey.DevisionID) && c.Leader_Plant.Any(u => u.FactoryID == PlantKey.PlantID) && c.Active == true)
                .Include(u => u.Leader_Devision)
                    .ThenInclude(b => b.Devision)
                .Include(u => u.Leader_Plant)
                    .ThenInclude(b => b.Factory)
                    .ToList();

            return LeadersBase;
        }

        public static ICollection<ActionLeader_DB> ActionLeader_LeadAll()
        {
            ICollection<ActionLeader_DB> All_Leader;
            var context = new DataBaseConnetionContext();
            All_Leader = context.ActionLeader
                .Include(u => u.Leader_Devision)
                    .ThenInclude(b => b.Devision)
                .Include(u => u.Leader_Plant)
                    .ThenInclude(b => b.Factory)
                .ToList();
            return All_Leader;
        }

        public static ActionLeader_DB LoadSingle(string FullName)
        {
            var SingleLeader = new ActionLeader_DB();
            var context = new DataBaseConnetionContext();
            SingleLeader = context.ActionLeader
                .Where(u => u.FullName == FullName)
                .Include(u => u.Leader_Devision)
                    .ThenInclude(b => b.Devision)
                .Include(u => u.Leader_Plant)
                    .ThenInclude(b => b.Factory)
                .FirstOrDefault();

            return SingleLeader;
        }

        public static ICollection<ActionLeader_DB> LoadDevision(string Devision, DataBaseConnetionContext context)
        {
            ICollection<ActionLeader_DB> LoadLeader;
            LoadLeader = context.ActionLeader
                .Where(u => u.Leader_Devision.Any(c => c.Devision.Devision == Devision) && u.Active == true).ToList();


            return LoadLeader;
        }

        public static ICollection<ActionLeader_DB> Load_FilteredBY_DevisionPlant(ICollection<Devision_DB> devisions, ICollection<Plant_DB> plants)
        {
            var context = new DataBaseConnetionContext();
            var LoadList = new List<ActionLeader_DB>();

            foreach (var plant in plants) {
                foreach (var devision in devisions) {
                    var Leaders = context.ActionLeader
                        .Where(u => u.Leader_Devision.Any(c => c.DevisionID == devision.DevisionID)
                        && u.Leader_Plant.Any(c => c.FactoryID == plant.PlantID)
                        && u.Active == true)
                        .ToList();
                    foreach (var Leader in Leaders) {
                        if (!LoadList.Any(u => u.FullName == Leader.FullName)) {
                            LoadList.Add(Leader);
                        }
                    }
                }
            }
            return LoadList;
        }

        public static void Add_ActionLeader(DataBaseConnetionContext context, ActionLeader_DB NewLeader)
        {
            //var context = new DataBaseConnetionContext();
            context.ActionLeader.Add(NewLeader);
            context.SaveChanges();
        }

        public static void Update_Leader(ActionLeader_DB UpdateLeader)
        {
            var context = new DataBaseConnetionContext();
            context.ActionLeader.Update(UpdateLeader);
            context.SaveChanges();
        }
    }
}
