using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface IConstatVariablesServices
    {
        public void Set(ConstantVariables constantVariables);
        public ICollection<ConstantVariables> Get(decimal year, Factories factory);
    }
}
