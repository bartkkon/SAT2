using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Data;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData
{
    public class PNCsServices : IPNCsServices
    {
        private readonly ConnectionContext connection;
        private readonly IMessageBoxService messageBoxService;

        public PNCsServices(ConnectionContext connection, IMessageBoxService messageBoxService )
        {
            this.connection = connection;
            this.messageBoxService = messageBoxService;
        }
        public void Clear(decimal year, Revisions revisions)
        {
            var deleteDelement = connection.PNCs.Where(d => d.Year == year && d.Revision == revisions).ToList();

            if (deleteDelement.Count != 0)
            {
                connection.PNCs.RemoveRange(deleteDelement);
                connection.SaveChanges();
            }
        }

        public void Clear(decimal year, Revisions revisions, Months month)
        {
            var deleteDelement = connection.PNCs.Where(d => d.Year == year && d.Revision == revisions && d.Month == month).ToList();

            if (deleteDelement.Count != 0)
            {
                connection.PNCs.RemoveRange(deleteDelement);
                connection.SaveChanges();
            }
        }

        public ICollection<PNC> Get(decimal year, Revisions revision)
        {
            return connection.PNCs.Where(d => d.Year == year && d.Revision == revision).ToList();
        }

        public ICollection<PNC> Get(decimal year, Revisions revision, Months month)
        {
            return connection.PNCs.Where(d => d.Year == year && d.Revision == revision && d.Month == month).ToList();
        }

        public PNC Get(string item, Revisions revision, Months month)
        {
            return connection.PNCs.Where(d => d.Item == item && d.Revision == revision && d.Month == month).FirstOrDefault();
        }

        public ICollection<PNC> Get(ICollection<string> itemList, decimal year, Revisions revision)
        {
            var searchElements = new List<PNC>();
            
            foreach(var item in itemList)
            {
                var searchElement = connection.PNCs.Where(d => d.Item == item && d.Revision == revision).ToList();
                if(searchElement.Count !=0)
                {
                    searchElements.AddRange(searchElement);
                }
            }
            return searchElements;
        }

        public ICollection<PNC> Get(ICollection<string> itemList, decimal year, Revisions revision, Months month)
        {
            var searchElements = new List<PNC>();

            foreach (var item in itemList)
            {
                var searchElement = connection.PNCs.Where(d => d.Item == item && d.Revision == revision && d.Month == month).ToList();
                if (searchElement.Count != 0)
                {
                    searchElements.AddRange(searchElement);
                }
            }
            return searchElements;
        }

        public bool Set(ICollection<PNC> newPNCs, Months month, decimal year)
        {
            if(connection.PNCs.Where(c=>c.Revision == Revisions.EA4 && c.Month == month && c.Year == year).FirstOrDefault() != null)
            {
                MessageBoxResult Result = messageBoxService.Ask("Data Exist!", $"Data for:{Environment.NewLine}Revision => {Revisions.EA4}{Environment.NewLine}Month => {month}{Environment.NewLine}Year => {year}{Environment.NewLine}Exist!{Environment.NewLine}{Environment.NewLine}Do you wnat replace it?");
                if(Result == MessageBoxResult.Yes)
                {
                    connection.RemoveRange(connection.PNCs.Where(c => c.Revision == Revisions.EA4 && c.Month == month && c.Year == year).ToList());
                }
                else if(Result == MessageBoxResult.No)
                {
                    //Moze kiedyś zapytać czy dopisć dane
                    return false;
                }
                else
                {
                    return false;
                }
            }

            connection.PNCs.AddRange(newPNCs);
            connection.SaveChanges();
            messageBoxService.ShowConfirmation("Data Added!", $"Succefuly added {newPNCs.Count} to DataBase");
            return true;
        }

        public bool Set(ICollection<PNC> newPNCs, Revisions revision, decimal year)
        {
            if (connection.PNCs.Where(c => c.Revision == revision && c.Year == year).FirstOrDefault() != null)
            {
                MessageBoxResult Result = messageBoxService.Ask("Data Exist!", $"Data for:{Environment.NewLine}Revision => {Revisions.EA4}{Environment.NewLine}Year => {year}{Environment.NewLine}Exist!{Environment.NewLine}{Environment.NewLine}Do you wnat replace it?");
                if (Result == MessageBoxResult.Yes)
                {
                    connection.RemoveRange(connection.PNCs.Where(c => c.Revision == revision && c.Year == year).ToList());
                }
                else if (Result == MessageBoxResult.No)
                {
                    //Moze kiedyś zapytać czy dopisć dane
                    return false;
                }
                else
                {
                    return false;
                }
            }

            connection.PNCs.AddRange(newPNCs);
            connection.SaveChanges();
            messageBoxService.ShowConfirmation("Data Added!", $"Succefuly added {newPNCs.Count} to DataBase");
            return true; 
        }
    }
}
