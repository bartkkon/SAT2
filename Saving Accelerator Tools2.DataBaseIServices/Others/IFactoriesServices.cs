using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Others
{
    public interface IFactoriesServices
    {
        public ICollection<Factories> Get();
        public ICollection<Factories> Get(bool active);
        public void Add(Factories newPlant);
        public void Update(Factories updatePlant);
    }
}
