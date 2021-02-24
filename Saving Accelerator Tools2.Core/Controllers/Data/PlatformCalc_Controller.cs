using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class PlatformCalc_Controller
    {
        public static void Add(string Platform, string Installation, bool Active)
        {
            var context = new DataBaseConnetionContext();
            var NewRecord = new PlatformCalc_DB() {
                Platform = Platform,
                Installation = Installation,
                Active = Active,
            };
            context.PlatformCalcs.Add(NewRecord);
            context.SaveChanges();
        }

        public static void Update(int ID, string Platform, string Instalation, bool Active)
        {
            bool Change = false;
            var context = new DataBaseConnetionContext();
            var Record = context.PlatformCalcs.Where(b => b.ID == ID).FirstOrDefault();

            if (Record.Platform != Platform) {
                Record.Platform = Platform;
                Change = true;
            }
            if (Record.Installation != Instalation) {
                Record.Installation = Instalation;
                Change = true;
            }
            if (Record.Active != Active) {
                Record.Active = Active;
                Change = true;
            }
            if (Change) {
                context.PlatformCalcs.Update(Record);
                context.SaveChanges();
            }
        }

        public static List<PlatformCalc_DB> LoadActive(bool Active)
        {
            var BaseRecords = new List<PlatformCalc_DB>();
            var context = new DataBaseConnetionContext();

            BaseRecords = context.PlatformCalcs.Where(b => b.Active == Active).ToList();

            return BaseRecords;
        }
    }
}
