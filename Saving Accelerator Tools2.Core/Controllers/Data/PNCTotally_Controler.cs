using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class PNCTotally_Controler
    {
        public static IEnumerable<PNCTotality_DB> Load (decimal Year, string Revision, int Month)
        {
            IEnumerable<PNCTotality_DB> PNCSum;
            var context = new DataBaseConnetionContext();

            if(Month ==0) {
                PNCSum = context.PNC_Totality.Where(Base => Base.Year == Year && Base.Revision == Revision).ToList();
            }
            else {
                PNCSum = context.PNC_Totality.Where(Base => Base.Year == Year && Base.Revision == Revision && Base.Month == Month).ToList();
            }

            return PNCSum;
        }

        public static void Delete ( IEnumerable<PNCTotality_DB> PNCSumList)
        {
            var context = new DataBaseConnetionContext();

            foreach(var PNCRow in PNCSumList) {
                context.PNC_Totality.Remove(PNCRow);
            }

            context.SaveChanges();
        }

        public static void Add(IEnumerable<PNCTotality_DB> PNCSumList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var PNCRow in PNCSumList) {
                context.PNC_Totality.Add(PNCRow);
            }

            context.SaveChanges();
        }

        public static IEnumerable<PNCTotality_DB> SearchList (decimal year, string revision, int startMonth, int finishMonth)
        {
            var context = new DataBaseConnetionContext();
            var List = new List<PNCTotality_DB>();

            List = context.PNC_Totality.Where(item => item.Year == year && item.Revision == revision && item.Month >= startMonth && item.Month <= finishMonth).ToList();

            return List;
        }

        public static bool CheckIfExist(decimal year, string revision, int month)
        {
            bool Status = false;
            var context = new DataBaseConnetionContext();

            Status = month != 0 ?
                context.PNC_Totality.Any(record => record.Year == year && record.Revision == revision && record.Month == month)
                :
                context.PNC_Totality.Any(record => record.Year == year && record.Revision == revision);

            return Status;
        }

        public static bool RemoveRange( decimal year, string revision, int month)
        {
            IEnumerable<PNCTotality_DB> DataBase;
            var context = new DataBaseConnetionContext();

            DataBase = month != 0 ?
                context.PNC_Totality.Where(record => record.Year == year && record.Revision == revision && record.Month == month).ToList()
                :
                context.PNC_Totality.Where(record => record.Year == year && record.Revision == revision).ToList();

            context.PNC_Totality.RemoveRange(DataBase);
            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool AddRange(IEnumerable<PNCTotality_DB> List)
        {
            var context = new DataBaseConnetionContext();

            context.PNC_Totality.AddRange(List);
            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
