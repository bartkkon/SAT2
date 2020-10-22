using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class Currency_Controller
    {
        public static Currency_DB Lead_Year (decimal Year)
        {
            var LoadRecord = new Currency_DB();
            var context = new DataBaseConnetionContext();

            LoadRecord = context.Currency.Where(c => c.Year == Year).FirstOrDefault();

            if(LoadRecord == null) {
                var NewYear = new Currency_DB() {
                    Year = Year,
                };
                context.Currency.Add(NewYear);
                context.SaveChanges();
                LoadRecord = NewYear;
            }

            return LoadRecord;
        }

        public static bool Update_Year(decimal Year, decimal Eccc, decimal Euro, decimal Dolar, decimal Sek)
        {
            var UpdateYear = new Currency_DB();
            var context = new DataBaseConnetionContext();

            UpdateYear = context.Currency.Where(c => c.Year == Year).FirstOrDefault();

            if(UpdateYear != null) {
                UpdateYear.ECCC = Eccc;
                UpdateYear.Euro = Euro;
                UpdateYear.Dolar = Dolar;
                UpdateYear.Sek = Sek;

                context.Currency.Update(UpdateYear);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
