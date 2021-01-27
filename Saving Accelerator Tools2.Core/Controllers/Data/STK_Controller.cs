using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class STK_Controller
    {
        public static IEnumerable<STK_DB> Load_Year (decimal Year)
        {
            IEnumerable<STK_DB> LoadedYear;
            var context = new DataBaseConnetionContext();

            LoadedYear = context.STK.Where(b => b.Year == Year).ToList();

            return LoadedYear;
        }

        public static void Delete_Year (decimal Year)
        {
            var context = new DataBaseConnetionContext();

            var LoadedYear = context.STK.Where(b => b.Year == Year).ToList();

            foreach(var OneRecord in LoadedYear) {
                context.STK.Remove(OneRecord);
            }

            context.SaveChanges();
        }

        public static void Update_Year (IEnumerable<STK_DB> UpdatedList)
        {
            var context = new DataBaseConnetionContext();

            foreach(var UpdateRecord in UpdatedList) {
                if (UpdateRecord.ID != 0) {
                    context.STK.Update(UpdateRecord);
                }
                else {
                    context.STK.Add(UpdateRecord);
                }
            }

            context.SaveChanges();
        }

        public static void Add_Year (IEnumerable<STK_DB> AddLists)
        {
            var context = new DataBaseConnetionContext();

            foreach(var AddRecord in AddLists) {
                context.STK.Add(AddRecord);
            }
            context.SaveChanges();
        }

        public static STK_DB FindItem(string Item, decimal Year)
        {
            var context = new DataBaseConnetionContext();

            var Results = context.STK.Where(b => b.ANC == Item && b.Year == Year).FirstOrDefault();

            return Results;
        }

        public static IEnumerable<STK_DB> FindItem_FullHistory(string Item)
        {
            var context = new DataBaseConnetionContext();

            var Results = context.STK.Where(b => b.ANC == Item).ToList();

            return Results;
        }
    }
}
