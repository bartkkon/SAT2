using Saving_Accelerator_Tools2.Model.Action.InterTables;
using Saving_Accelerator_Tools2.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Action.Sub
{
    public class PNC : Base
    {
        public int ID { get => iD; set => iD = value; }
        public string Item { get => item; set => item = value; }
        public ObservableCollection<PNC_InterTable> PNCs
        {
            get => pNCs;
            set
            {
                pNCs = value;
                OnPropertyChange();
            }
        }

        public ObservableCollection<PNC_InterTable> pNCs = new ObservableCollection<PNC_InterTable>();
        private int iD;
        private string item;
    }
}
