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
    }
}
