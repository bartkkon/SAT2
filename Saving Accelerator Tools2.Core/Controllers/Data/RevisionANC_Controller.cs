using System;
using System.Collections.Generic;
using System.Text;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using System.Linq;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class RevisionANC_Controller
    {
        public static IEnumerable<RevisionANC_DB> Load (decimal Year, string Revision)
        {
            IEnumerable<RevisionANC_DB> ANCList;
            var context = new DataBaseConnetionContext();

            ANCList = context.ANC_Revision.Where(Base => Base.Year == Year && Base.Revision == Revision).ToList();

            return ANCList;
        }

        public static void Delete(IEnumerable<RevisionANC_DB> ANCList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var ANC in ANCList) {
                context.ANC_Revision.Remove(ANC);
            }

            context.SaveChanges();
        }

        public static void Add(IEnumerable<RevisionANC_DB> ANCList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var ANC in ANCList) {
                context.ANC_Revision.Add(ANC);
            }

            context.SaveChanges();
        }

        public static IEnumerable<RevisionANC_DB> Search(string[] ListANC, decimal Year, string Revision)
        {
            var context = new DataBaseConnetionContext();
            var ANCListFinal = new List<RevisionANC_DB>();


            foreach (string ANC in ListANC) {
                IEnumerable<RevisionANC_DB> ANCList;
                ANCList = context.ANC_Revision.Where(b => b.ANC == ANC && b.Year == Year && b.Revision == Revision).ToList();
                foreach (var ANCBase in ANCList) {
                    ANCListFinal.Add(ANCBase);
                }
            }

            return ANCListFinal;
        }
    }
}
