using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.Others
{
    public class FactoriesServices : IFactoriesServices
    {
        private ConnectionContext connection;
        public FactoriesServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Add(Factories newPlant)
        {
            //if (newPlant.ID == 0)
            //    throw new DataMisalignedException();

            connection.Add(newPlant);
            connection.SaveChanges();


        }

        public ICollection<Factories> Get()
        {
            return connection.Factories.ToList();

        }

        public ICollection<Factories> Get(bool active)
        {
            return active ? connection.Factories.Where(f => f.Active == active).ToList() : Get();
        }

        public void Update(Factories updatePlant)
        {
            //if (updatePlant.ID == 0)
            //    throw new DataMisalignedException();

            var connection = new ConnectionContext();

            connection.Update(updatePlant);
            connection.SaveChanges();
        }
    }
}
