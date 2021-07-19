using Saving_Accelerator_Tools2.DataBaseIServices.ActionService;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.Action
{
    public class LoadedActionServices : ILoadedAction
    {
        private readonly ConnectionContext connection;
        private IObservable<ActionBase> loaded;

        public LoadedActionServices(ConnectionContext connection)
        {
            this.connection = connection;
            //loaded = new 

        }
        public IObservable<ActionBase> Get()
        {
            return loaded;
        }
    }
}
