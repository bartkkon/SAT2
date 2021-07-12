using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface ICurrenciesServices
    {
        public ICollection<Currencies> Get(decimal year);
        public Currencies Get(decimal year, Currency currency);
        public void Set(ICollection<Currencies> currencies);
    }
}
