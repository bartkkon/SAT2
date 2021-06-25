using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Others
{
    public interface IDevisionServices
    {
        public ICollection<Devision> Get();
        public ICollection<Devision> Get(bool active);
        public void Add(Devision newDevision);
        public void Update(Devision updateDevison);
    }
}
