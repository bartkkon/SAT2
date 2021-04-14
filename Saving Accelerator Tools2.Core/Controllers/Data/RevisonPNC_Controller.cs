using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;


namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class RevisonPNC_Controller
    {
        public static IEnumerable<RevisionPNC_DB> Load(decimal Year, string Revision)
        {
            IEnumerable<RevisionPNC_DB> PNCList;
            var context = new DataBaseConnetionContext();

            PNCList = context.PNC_Revision.Where(Base => Base.Year == Year && Base.Revision == Revision).ToList();

            return PNCList;
        }

        public static void Delete(IEnumerable<RevisionPNC_DB> PNCList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var PNC in PNCList) {
                context.PNC_Revision.Remove(PNC);
            }

            context.SaveChanges();
        }

        public static void Add(IEnumerable<RevisionPNC_DB> PNCList)
        {
            var context = new DataBaseConnetionContext();

            foreach (var PNC in PNCList) {
                context.PNC_Revision.Add(PNC);
            }

            context.SaveChanges();
        }

        public static IEnumerable<RevisionPNC_DB> Search (string[] ListPNC, decimal Year, string Revision)
        {
            var context = new DataBaseConnetionContext();
            var PNCListFinal = new List<RevisionPNC_DB>();


            foreach (string PNC in ListPNC) {
                IEnumerable<RevisionPNC_DB> PNCList;
                PNCList = context.PNC_Revision.Where(b => b.PNC == PNC && b.Year == Year && b.Revision == Revision).ToList();
                foreach (var PNCBase in PNCList) {
                    PNCListFinal.Add(PNCBase);
                }
            }

            return PNCListFinal;
        }

        public static IEnumerable<RevisionPNC_DB> Search(List<string> ListPNC, decimal Year, string Revision)
        {
            var context = new DataBaseConnetionContext();
            var PNCListFinal = new List<RevisionPNC_DB>();


            foreach (string PNC in ListPNC) {
                IEnumerable<RevisionPNC_DB> PNCList;
                PNCList = context.PNC_Revision.Where(b => b.PNC == PNC && b.Year == Year && b.Revision == Revision).ToList();
                foreach (var PNCBase in PNCList) {
                    PNCListFinal.Add(PNCBase);
                }
            }

            return PNCListFinal;
        }

        public static ICollection<QuantitySum> LoadSum_Data(decimal Year)
        {
            List<QuantitySum> Quantity;
            var context = new DataBaseConnetionContext();

            var DataBaseList = context.PNC_Revision.Where(u => u.Year == Year).ToList();
            if (DataBaseList.Count != 0) {
                //Quantity = CreateObjects();

                return Quantity = new List<QuantitySum>(); ;
            }

            return Quantity = new List<QuantitySum>();
        }

        private static List<QuantitySum> CreateObjects(decimal Year)
        {
            var QuantityList = new List<QuantitySum>();
            var context = new DataBaseConnetionContext();
            string[] Revisions = new string[5] { "BU", "EA1", "EA2", "EA3", "EA4" };
            string[] Instalation = new string[4] { "FS", "BI", "FI", "FSBU" };

            foreach(string Revision in Revisions) {
                int StartMonth = 1;
                if(Revision == "EA1") {
                    StartMonth = 3; 
                }
                else if(Revision == "EA2") {
                    StartMonth = 6;
                }
                else if(Revision == "EA3") {
                    StartMonth = 9;
                }
                for (; StartMonth <=12; StartMonth ++) {
                    if(Revision != "EA4") {
                        var DataBaseRevisionRecord = context.PNC_Revision.Where(u => u.Year == Year && u.Revision == Revision && u.Month == StartMonth).ToList();
                        if (DataBaseRevisionRecord.Count != 0) {

                        }
                    }
                }
            }

            return QuantityList;
        }
    }


    public class QuantitySum
    {
        public string Revision { get; set; }
        public int Month { get; set; }
        public string Structure { get; set; }
        public string Instalation { get; set; }
        public decimal Quantity { get; set; }
    }
}
