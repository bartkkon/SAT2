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
    public class ANCsServices : IANCsServices
    {
        private readonly ConnectionContext connection;

        public ANCsServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Clear(decimal year, Revisions revisions)
        {
            var deleteDelement = connection.ANCs.Where(d => d.Year == year && d.Revision == revisions).ToList();

            if (deleteDelement.Count != 0)
            {
                connection.ANCs.RemoveRange(deleteDelement);
                connection.SaveChanges();
            }
        }

        public void Clear(decimal year, Revisions revisions, Months month)
        {
            var deleteDelement = connection.ANCs.Where(d => d.Year == year && d.Revision == revisions && d.Month == month).ToList();

            if (deleteDelement.Count != 0)
            {
                connection.ANCs.RemoveRange(deleteDelement);
                connection.SaveChanges();
            }
        }

        public ICollection<ANC> Get(decimal year, Revisions revision)
        {
            return connection.ANCs.Where(d => d.Year == year && d.Revision == revision).ToList();
        }

        public ICollection<ANC> Get(decimal year, Revisions revision, Months month)
        {
            return connection.ANCs.Where(d => d.Year == year && d.Revision == revision && d.Month == month).ToList();
        }

        public ANC Get(string item, Revisions revision, Months month)
        {
            return connection.ANCs.Where(d => d.Item == item && d.Revision == revision && d.Month == month).FirstOrDefault();
        }

        public ICollection<ANC> Get(ICollection<string> itemList, decimal year, Revisions revision)
        {
            var searchElements = new List<ANC>();

            foreach (var item in itemList)
            {
                var searchElement = connection.ANCs.Where(d => d.Item == item && d.Revision == revision).ToList();
                if (searchElement.Count != 0)
                {
                    searchElements.AddRange(searchElement);
                }
            }
            return searchElements;
        }

        public ICollection<ANC> Get(ICollection<string> itemList, decimal year, Revisions revision, Months month)
        {
            var searchElements = new List<ANC>();

            foreach (var item in itemList)
            {
                var searchElement = connection.ANCs.Where(d => d.Item == item && d.Revision == revision && d.Month == month).ToList();
                if (searchElement.Count != 0)
                {
                    searchElements.AddRange(searchElement);
                }
            }
            return searchElements;
        }

        public void Set(ICollection<ANC> newANCs)
        {
            connection.AddRange(newANCs);
            connection.SaveChanges();
        }
    }
}
