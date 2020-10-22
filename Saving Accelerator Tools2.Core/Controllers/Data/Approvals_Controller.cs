using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class Approvals_Controller
    {
        public static ICollection<Approvals_DB> LoadYear(decimal Year)
        {
            ICollection<Approvals_DB> YearList;
            var context = new DataBaseConnetionContext();

            YearList = context.Approvals.Where(u => u.Year == Year).ToList();

            if (YearList.Count == 0) {
                NewYear(context, Year);
                YearList = context.Approvals.Where(u => u.Year == Year).ToList();
            }

            return YearList;
        }

        public static void NewYear(DataBaseConnetionContext context, decimal Year)
        {
            var BU = new Approvals_DB() { Year = Year, Revision = "BU", Month = null, Devision = null, Status = 0 };
            var EA1 = new Approvals_DB() { Year = Year, Revision = "EA1", Month = null, Devision = null, Status = 0 };
            var EA2 = new Approvals_DB() { Year = Year, Revision = "EA2", Month = null, Devision = null, Status = 0 };
            var EA3 = new Approvals_DB() { Year = Year, Revision = "EA3", Month = null, Devision = null, Status = 0 };
            var Jan = new Approvals_DB() { Year = Year, Revision = null, Month = 1, Devision = null, Status = 0 };
            var Feb = new Approvals_DB() { Year = Year, Revision = null, Month = 2, Devision = null, Status = 0 };
            var Mar = new Approvals_DB() { Year = Year, Revision = null, Month = 3, Devision = null, Status = 0 };
            var Apr = new Approvals_DB() { Year = Year, Revision = null, Month = 4, Devision = null, Status = 0 };
            var May = new Approvals_DB() { Year = Year, Revision = null, Month = 5, Devision = null, Status = 0 };
            var Jun = new Approvals_DB() { Year = Year, Revision = null, Month = 6, Devision = null, Status = 0 };
            var Jul = new Approvals_DB() { Year = Year, Revision = null, Month = 7, Devision = null, Status = 0 };
            var Aug = new Approvals_DB() { Year = Year, Revision = null, Month = 8, Devision = null, Status = 0 };
            var Sep = new Approvals_DB() { Year = Year, Revision = null, Month = 9, Devision = null, Status = 0 };
            var Oct = new Approvals_DB() { Year = Year, Revision = null, Month = 10, Devision = null, Status = 0 };
            var Nov = new Approvals_DB() { Year = Year, Revision = null, Month = 11, Devision = null, Status = 0 };
            var Dec = new Approvals_DB() { Year = Year, Revision = null, Month = 12, Devision = null, Status = 0 };
            var Ele = new Approvals_DB() { Year = Year, Revision = null, Month = null, Devision = "Electronic", Status = 0 };
            var Mec = new Approvals_DB() { Year = Year, Revision = null, Month = null, Devision = "Mechanic", Status = 0 };
            var Nvr = new Approvals_DB() { Year = Year, Revision = null, Month = null, Devision = "NVR", Status = 0 };

            context.Approvals.Add(BU);
            context.Approvals.Add(EA1);
            context.Approvals.Add(EA2);
            context.Approvals.Add(EA3);
            context.Approvals.Add(Jan);
            context.Approvals.Add(Feb);
            context.Approvals.Add(Mar);
            context.Approvals.Add(Apr);
            context.Approvals.Add(May);
            context.Approvals.Add(Jun);
            context.Approvals.Add(Jul);
            context.Approvals.Add(Aug);
            context.Approvals.Add(Sep);
            context.Approvals.Add(Oct);
            context.Approvals.Add(Nov);
            context.Approvals.Add(Dec);
            context.Approvals.Add(Ele);
            context.Approvals.Add(Mec);
            context.Approvals.Add(Nvr);
            context.SaveChanges();
        }
    }
}
