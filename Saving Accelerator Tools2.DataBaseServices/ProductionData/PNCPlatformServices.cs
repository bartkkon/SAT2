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
    public class PNCPlatformServices : IPNCPlatformServices
    {
        private readonly ConnectionContext connection;

        public PNCPlatformServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Clear(decimal year, Revisions revision)
        {
            var deletedElement = connection.PNCPlatforms.Where(d => d.Year == year && d.Revision == revision).ToList();
            connection.PNCPlatforms.RemoveRange(deletedElement);
            connection.SaveChanges();
        }

        public void Clear(decimal year, Revisions revision, Months month)
        {
            var deletedElement = connection.PNCPlatforms.Where(d => d.Year == year && d.Revision == revision && d.Month == month).ToList();
            connection.PNCPlatforms.RemoveRange(deletedElement);
            connection.SaveChanges();
        }

        public ICollection<PNCPlatform> Get(decimal year, Revisions revision)
        {
            return connection.PNCPlatforms.Where(d => d.Year == year &&
            d.Revision == revision).ToList();
        }

        public ICollection<PNCPlatform> Get(decimal year, Revisions revision, Months month)
        {
            return connection.PNCPlatforms.Where(d => d.Year == year &&
             d.Revision == revision &&
             d.Month == month).ToList();
        }

        public ICollection<PNCPlatform> Get(decimal year, Revisions revision, Months month, Structure structure)
        {
            return connection.PNCPlatforms.Where(d => d.Year == year &&
             d.Revision == revision &&
             d.Month == month &&
             d.Structure == structure).ToList();
        }

        public ICollection<PNCPlatform> Get(decimal year, Revisions revision, Months month, Structure structure, Installation installation)
        {
            return connection.PNCPlatforms.Where(d => d.Year == year &&
             d.Revision == revision &&
             d.Month == month &&
             d.Structure == structure &&
             d.Installation == installation).ToList();
        }

        public void Set(ICollection<PNCPlatform> platform)
        {
            PNCPlatform checkDB = connection.PNCPlatforms.FirstOrDefault(c => c.Revision == platform.First().Revision && c.Month == platform.First().Month && c.Year == platform.First().Year);
            if (checkDB != null)
            {
                if (checkDB.Revision == Revisions.EA4)
                {
                    connection.PNCPlatforms.RemoveRange(connection.PNCPlatforms.Where(c => c.Revision == platform.First().Revision && c.Month == platform.First().Month && c.Year == platform.First().Year).ToList());
                }
                else
                {
                    connection.PNCPlatforms.RemoveRange(connection.PNCPlatforms.Where(c => c.Revision == platform.First().Revision && c.Year == platform.First().Year).ToList());
                }
            }

            connection.AddRange(platform);
            connection.SaveChanges();
        }
    }
}
