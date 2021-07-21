using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Others
{
    public interface ILeadersServices
    {
        public void Add(Leaders newLeader);
        public void Update(Leaders updateLeader);
        public ICollection<Leaders> Get();
        public ICollection<Leaders> Get(bool active);
        public ICollection<Leaders> Get(LeadersSearchCriteria criteria);
        public Leaders Get(string name, string surname);
    }
}
