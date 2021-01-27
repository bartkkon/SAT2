using System;
using System.Collections.Generic;
using System.Text;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Data;
using System.Linq;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class PNCMonthly_Controller
    {
        public static IEnumerable<MonthlyPNC_DB> Load (decimal Year, int Month)
        {
            IEnumerable<MonthlyPNC_DB> PNCList;
            var context = new DataBaseConnetionContext();

            PNCList = context.PNC_Monthly.Where(Base => Base.Year == Year && Base.Month == Month).ToList();

            return PNCList;
        }

        public static void Delete(IEnumerable<MonthlyPNC_DB> PNCList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var PNC in PNCList) {
                context.PNC_Monthly.Remove(PNC);
            }

            context.SaveChanges();
        }

        public static void Add(IEnumerable<MonthlyPNC_DB> PNCList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var PNC in PNCList) {
                context.PNC_Monthly.Add(PNC);
            }

            context.SaveChanges();
        }

        public static IEnumerable<MonthlyPNC_DB> Search (string[] ListPNC, decimal Year)
        {
            var context = new DataBaseConnetionContext();
            var PNCListFinal = new List<MonthlyPNC_DB>();
            

            foreach (string PNC in ListPNC) {
                IEnumerable<MonthlyPNC_DB> PNCList;
                PNCList = context.PNC_Monthly.Where(b => b.PNC == PNC && b.Year == Year).ToList();
                foreach(var PNCBase in PNCList) {
                    PNCListFinal.Add(PNCBase);
                }
            }

            return PNCListFinal;
        }
    }
}
