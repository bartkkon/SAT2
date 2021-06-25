using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class Approvals2_Controller
    {
        public static List<Approvals2_DB> Load_Year(decimal Year, string Plant)
        {
            List<Approvals2_DB> Data = new List<Approvals2_DB>();
            var context = new DataBaseConnetionContext();

            Data = context.Approvals2.Where(record => record.Year == Year && record.Plant == Plant)
                .Include(subrecord => subrecord.Devisions).ThenInclude(subrecord => subrecord.Devision)
                .ToList();

            return Data;
        }

        public static bool AddNewOpen(decimal Year, string Plant, string Revision, int? Month)
        {
            var NewRecord = new Approvals2_DB();
            var context = new DataBaseConnetionContext();

            NewRecord.Year = Year;
            NewRecord.Plant = Plant;
            NewRecord.Revision = Revision;
            NewRecord.Month = Month;

            try {
                context.Approvals2.Add(NewRecord);
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool Delete(decimal Year, string Plant, string Revision, int? Month)
        {
            var context = new DataBaseConnetionContext();

            var DeletedRecord = context.Approvals2.Where(item => item.Year == Year && item.Revision == Revision && item.Plant == Plant && item.Month == Month).Include(subrecord => subrecord.Devisions).ThenInclude(subrecord => subrecord.Devision).FirstOrDefault();

            if (DeletedRecord != null) {
                try {
                    context.Approvals2.Remove(DeletedRecord);
                    return true;
                }
                catch {
                    return false;
                }
            }
            return false;
        }

        public static List<Approvals2_DB> Load_Open(string ActionDevision, string Plant, decimal Year)
        {
            //Wyszukuje dla raoku akcji plis rok +1 jako Carry Over
            var context = new DataBaseConnetionContext();
            var Final = new List<Approvals2_DB>();
            var FindYear = context.Approvals2.Where(item => (item.Year == Year || item.Year == Year + 1) && item.Plant == Plant)
                .Include(subitem => subitem.Devisions.Where(b => b.Devision.Active == true)).ThenInclude(subitem => subitem.Devision).ToList();

            foreach(var CheckYear in FindYear) {
                if(!CheckYear.Devisions.Any(item => item.Devision.Devision == ActionDevision)) {
                    Final.Add(CheckYear);
                }
            }

            return Final;
        }
    }
}
