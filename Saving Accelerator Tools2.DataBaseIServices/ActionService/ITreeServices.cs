using Saving_Accelerator_Tools2.Model.ActionCriteria;
using SavingAcceleratorTools2.ProjectModels.Action;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.ActionService
{
    public interface ITreeServices
    {
        public ObservableCollection<ActionTree> Load(TreeCriteria criteria);
    }
}
