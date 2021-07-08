using Saving_Accelerator_Tools2.Model.Data;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface IPNCsServices
    {
        public void Set(ICollection<PNC> newANCs);
        public ICollection<PNC> Get(decimal year, Revisions revision);
        public ICollection<PNC> Get(decimal year, Revisions revision, Months month);
        public PNC Get(string item, Revisions revision, Months month);
        public ICollection<PNC> Get(ICollection<string> itemList, decimal year, Revisions revision);
        public ICollection<PNC> Get(ICollection<string> itemList, decimal year, Revisions revision, Months month);
        public void Clear(decimal year, Revisions revisions);
        public void Clear(decimal year, Revisions revisions, Months month);
    }
}
