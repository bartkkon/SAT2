using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class ANCMonthly_Controller
    {
        public static IEnumerable<MonthlyANC_DB> Load (decimal Year, int Month)
        {
            IEnumerable<MonthlyANC_DB> ANCList;
            var context = new DataBaseConnetionContext();

            ANCList = context.ANC_Monthly.Where(Base => Base.Year == Year && Base.Month == Month).ToList();

            return ANCList;
        }

        public static void Delete (IEnumerable<MonthlyANC_DB> ANCList)
        {
            var context = new DataBaseConnetionContext();

            foreach(var ANC in ANCList) {
                context.ANC_Monthly.Remove(ANC);
            }

            context.SaveChanges();
        }

        public static void Add (IEnumerable<MonthlyANC_DB> ANCList)
        {
            var context = new DataBaseConnetionContext();

            foreach(var ANC in ANCList) {
                context.ANC_Monthly.Add(ANC);
            }

            context.SaveChanges();
        }

        public static IEnumerable<MonthlyANC_DB> Search(string[] ListANC, decimal Year)
        {
            var context = new DataBaseConnetionContext();
            var ANCListFinal = new List<MonthlyANC_DB>();


            foreach (string ANC in ListANC) {
                IEnumerable<MonthlyANC_DB> ANCList;
                ANCList = context.ANC_Monthly.Where(b => b.ANC == ANC && b.Year == Year).ToList();
                foreach (var ANCBase in ANCList) {
                    ANCListFinal.Add(ANCBase);
                }
            }

            return ANCListFinal;
        }
    }
}
