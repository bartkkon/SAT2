using Saving_Accelerator_Tools2.Model.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.ActionService
{
    public interface ILoadedAction
    {
        public IObservable<ActionBase> Get();
    }
}
