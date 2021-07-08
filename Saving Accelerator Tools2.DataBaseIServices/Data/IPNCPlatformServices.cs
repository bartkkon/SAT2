using Saving_Accelerator_Tools2.Model.Data;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface IPNCPlatformServices
    {
        public void Set(ICollection<PNCPlatform> platform);
        public void Clear(decimal year, Revisions revision);
        public void Clear(decimal year, Revisions revision, Months month);
        public ICollection<PNCPlatform> Get(decimal year, Revisions revision);
        public ICollection<PNCPlatform> Get(decimal year, Revisions revision, Months month);
        public ICollection<PNCPlatform> Get(decimal year, Revisions revision, Months month, Structure structure);
        public ICollection<PNCPlatform> Get(decimal year, Revisions revision, Months month, Structure structure, Installation installation);
    }
}
