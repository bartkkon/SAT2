using Microsoft.EntityFrameworkCore;
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
    public class DevisionServices : IDevisionServices
    {
        private ConnectionContext connection;
        public DevisionServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Add(Devision newDevision)
        {
            connection.Add(newDevision);
            connection.SaveChanges();
        }



        public ICollection<Devision> Get()
        {

            return connection.Devisions
                .Include(c => c.Factory)
                .ToList();
        }

        public ICollection<Devision> Get(bool active)
        {

            return active ? connection.Devisions
                .Where(f => f.Active == active)
                .Include(c => c.Factory)
                .ToList() : Get();
        }

        public ICollection<Devision> Get(Factories factories)
        {
            return connection.Devisions.Where(f => f.Factory == factories && f.Active).ToList();
        }

        public void Update(Devision updateDevison)
        {
            connection.Devisions.Update(updateDevison);
            connection.SaveChanges();
        }
    }
}
