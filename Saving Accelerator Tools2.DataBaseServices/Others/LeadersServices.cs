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
    public class LeadersServices : ILeadersServices
    {
        private readonly ConnectionContext connection;

        public LeadersServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Add(Leaders newLeader)
        {
            connection.Leaders.Add(newLeader);
            connection.SaveChanges();
        }

        public ICollection<Leaders> Get()
        {
            return connection.Leaders.ToList();
        }

        public ICollection<Leaders> Get(bool active)
        {
            return active ? connection.Leaders.Where(a => a.Active == active).ToList() : Get();
        }

        public ICollection<Leaders> Get(LeadersSearchCriteria criteria)
        {
            IQueryable<Leaders> leaders = Get().AsQueryable();

            if(criteria.DevisionID.HasValue)
            {
                leaders = leaders.Where(l => l.DevisionID == criteria.DevisionID);
            }
            if(criteria.FactoryID.HasValue)
            {
                leaders = leaders.Where(l => l.Devision.FactoryID == criteria.FactoryID);
            }
            if(criteria.Active.HasValue)
            {
                leaders = leaders.Where(l => l.Active == criteria.Active);
            }

            return leaders.ToList();
        }

        public void Update(Leaders updateLeader)
        {
            connection.Leaders.Update(updateLeader);
            connection.SaveChanges();
        }
    }
}
