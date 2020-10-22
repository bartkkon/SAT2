using Microsoft.EntityFrameworkCore.Update.Internal;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class Targets_Controller
    {
        public static Targets_DB Load (decimal Year, string Revision)
        {
            var LoadData = new Targets_DB();
            var context = new DataBaseConnetionContext();

            LoadData = context.Targets.Where(u => u.Year == Year && u.Revision == Revision).FirstOrDefault();

            if(LoadData == null) {
                LoadData = new Targets_DB() {
                    Year = Year,
                    Revision = Revision,
                };
                context.Targets.Add(LoadData);
                context.SaveChanges();
            }

            return LoadData;
        }

        public static bool Update(decimal Year, string Revision, decimal DM, decimal PC, decimal Electronic, decimal Mechanic, decimal NVR)
        {
            var context = new DataBaseConnetionContext();
            var UpdateRecord = context.Targets.Where(u => u.Year == Year && u.Revision == Revision).FirstOrDefault();

            if(UpdateRecord != null) {
                UpdateRecord.DM = DM;
                UpdateRecord.PC = PC;
                UpdateRecord.Electronic = Electronic;
                UpdateRecord.Mechanic = Mechanic;
                UpdateRecord.NVR = NVR;
                context.Targets.Update(UpdateRecord);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
