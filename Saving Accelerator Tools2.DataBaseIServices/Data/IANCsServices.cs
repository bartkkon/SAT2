using Saving_Accelerator_Tools2.Model.Data;
using SavingAcceleratorTools2.ProjectModels.Data;
using System.Collections.Generic;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface IANCsServices
    {
        public void Set(ICollection<ANC> newANCs, Months month, decimal year);
        public void Set(ICollection<ANC> newANCs, Revisions revision, decimal year);
        public ICollection<ANC> Get(decimal year, Revisions revision);
        public ICollection<ANC> Get(decimal year, Revisions revision, Months month);
        public ANC Get(string item, Revisions revision, Months month);
        public ICollection<ANC> Get(ICollection<string> itemList, decimal year, Revisions revision);
        public ICollection<ANC> Get(ICollection<string> itemList, decimal year, Revisions revision, Months month);
        public void Clear(decimal year, Revisions revisions);
        public void Clear(decimal year, Revisions revisions, Months month);
    }
}
