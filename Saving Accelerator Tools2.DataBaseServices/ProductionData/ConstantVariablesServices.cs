using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData
{
    public class ConstantVariablesServices : IConstatVariablesServices
    {
        private readonly ConnectionContext connection;

        public ConstantVariablesServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public ICollection<ConstantVariables> Get(decimal year, Factories factory)
        {
            return connection.Constants.Where(f => f.Year == year && f.Factory == factory).ToList();
        }

        public void Set(ConstantVariables constantVariables)
        {
            if(constantVariables.ID == 0)
            {
                connection.Constants.Add(constantVariables);
            }
            else
            {
                connection.Constants.Update(constantVariables);
            }
            connection.SaveChanges();
        }
    }
}
