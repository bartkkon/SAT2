using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class PNC_Controller
    {
        /// <summary>
        /// Load PNC Quantity list according to range (if month is 0, load all range for year and revision) 
        /// </summary>
        /// <param name="year">value for Quantity year</param>
        /// <param name="revision">value for Revision range</param>
        /// <param name="month">value for specific Month</param>
        /// <returns>List all componets according to require range</returns>
        public static List<PNC_DB> LoadRange(decimal year, string revision, int month)
        {
            IEnumerable<PNC_DB> DataBase;
            var context = new DataBaseConnetionContext();

            DataBase = month != 0 ?
                context.PNC_Quantity.Where(record => record.Year == year && record.Revision == revision && record.Month == month).ToList()
                :
                context.PNC_Quantity.Where(record => record.Year == year && record.Revision == revision).ToList();

            return DataBase.ToList();
        }

        public static bool CheckIfExist(decimal year, string revision, int month)
        {
            bool Status = false;
            var context = new DataBaseConnetionContext();

            Status = month != 0 ?
                context.PNC_Quantity.Any(record => record.Year == year && record.Revision == revision && record.Month == month)
                :
                context.PNC_Quantity.Any(record => record.Year == year && record.Revision == revision);

            return Status;
        }

        public static bool RemoveRange(decimal year, string revision, int month)
        {
            IEnumerable<PNC_DB> DataBase;
            var context = new DataBaseConnetionContext();

            DataBase = month != 0 ?
                context.PNC_Quantity.Where(record => record.Year == year && record.Revision == revision && record.Month == month).ToList()
                :
                context.PNC_Quantity.Where(record => record.Year == year && record.Revision == revision).ToList();

            context.PNC_Quantity.RemoveRange(DataBase);
            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool AddRange(IEnumerable<PNC_DB> List)
        {
            var context = new DataBaseConnetionContext();

            context.PNC_Quantity.AddRange(List);
            try {
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public static IEnumerable<PNC_DB> SearchList(List<string> List, decimal year, string revision, int month)
        {
            var context = new DataBaseConnetionContext();
            var ItemsList = new List<PNC_DB>();

            foreach (var Item in List) {
                var ItemLiat = month != 0 ?
                        context.PNC_Quantity.Where(item => item.Item == Item && item.Year == year && item.Revision == revision && item.Month == month).ToList()
                        :
                        context.PNC_Quantity.Where(item => item.Item == Item && item.Year == year && item.Revision == revision).ToList();

                ItemsList.AddRange(ItemLiat);
            }

            return ItemsList;
        }

        public static IEnumerable<PNC_DB> SearchList(List<string> List, decimal year, string revision, int startMonth, int finishMonth)
        {
            var context = new DataBaseConnetionContext();
            var ItemsList = new List<PNC_DB>();

            foreach(var Item in List) {
                var ItemList = context.PNC_Quantity.Where(item => item.Item == Item && item.Year == year && item.Revision == revision && item.Month >= startMonth && item.Month <= finishMonth).ToList();

                ItemsList.AddRange(ItemList);
            }

            return ItemsList;
        }
    }
}
