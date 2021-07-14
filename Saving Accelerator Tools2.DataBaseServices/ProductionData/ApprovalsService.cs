using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Approvals;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData
{
    public class ApprovalsService : IApprovalsService
    {
        private readonly ConnectionContext connection;

        public ApprovalsService(ConnectionContext connection)
        {
            this.connection = connection;
        }

        public ICollection<Approval> Get(Approval_SearchCriteria criteria)
        {
            IQueryable<Approval> approvals = connection.Approvals.Include(c => c.Teams).AsQueryable();

            if(criteria.Year.HasValue)
            {
                approvals = approvals.Where(c => c.Year == criteria.Year);
            }
            if(criteria.Status.HasValue)
            {
                approvals = approvals.Where(c => c.Status == criteria.Status);
            }
            if(criteria.Revision.HasValue)
            {
                approvals = approvals.Where(c => c.Revision == criteria.Revision);
            }
            if(criteria.Month.HasValue)
            {
                approvals = approvals.Where(c => c.Month == criteria.Month);
            }
            if(criteria.Factory.HasValue)
            {
                approvals = approvals.Where(c => c.Teams.First().Devision.Factory.ID == criteria.Factory);
            }
            return approvals.ToList();
        }

        public Approval GetOpen(Factories factories)
        {
            return connection.Approvals.Include(c => c.Teams).FirstOrDefault(c => c.Status == Status.Open && c.Teams.First().Devision.Factory == factories);
        }

        public void Set(Approval NewRecord)
        {
            connection.Add(NewRecord);
            connection.SaveChanges();
        }

        public void Update(Approval updateRecord)
        {
            connection.Update(updateRecord);
            connection.SaveChanges();
        }
    }
}