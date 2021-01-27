using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Statistic
{
    public class StatisticViewModel : INotifyProperty
    {
        private decimal year = DateTime.UtcNow.Year;
        private DataTable productionTable = PrepareTable();

        public decimal Year
        {
            get { return year; }
            set
            {
                year = value;
                RisePropoertyChanged();
            }
        }
        public  DataTable ProductionTable
        {
            get { return productionTable; }
            set
            {
                productionTable = value;
                RisePropoertyChanged();
            }
        }

        private static DataTable PrepareTable()
        {
            DataTable Table = new DataTable();

            Table.Columns.Add("Revision");
            Table.Columns.Add("Quantity");
            Table.Columns.Add("BU");
            Table.Columns.Add("EA1");
            Table.Columns.Add("EA2");
            Table.Columns.Add("EA3");
            Table.Columns.Add("EA4");


            ProductionQuantity_CreateTable(Table);

            return Table;
        }

        private static void ProductionQuantity_CreateTable(DataTable table)
        {

        }
    }


}
