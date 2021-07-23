using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface IStandardCostServices
    {
        public void Set(ICollection<StandardCost> newList);
        public void UpdateFromReport(Factories factory);
        public StandardCost Get(string item, decimal year, Factories factory);
        public ICollection<StandardCost> Get(decimal year);
        public ICollection<StandardCost> Get(ICollection<string> items, decimal year);
        public void Clear(decimal year);

    }
}
