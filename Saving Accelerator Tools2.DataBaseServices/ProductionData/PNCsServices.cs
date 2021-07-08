using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Data;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData
{
    public class PNCsServices : IPNCsServices
    {
        private readonly ConnectionContext connection;

        public PNCsServices(ConnectionContext connection)
        {
            this.connection = connection;
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

        public void Set(ICollection<PNC> newPNCs)
        {
            connection.AddRange(newPNCs);
            connection.SaveChanges();
        }
    }
}
