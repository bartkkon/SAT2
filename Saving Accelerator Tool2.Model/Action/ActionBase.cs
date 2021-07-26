using Saving_Accelerator_Tools2.Model.Action.InterTables;
using Saving_Accelerator_Tools2.Model.Action.Sub;
using Saving_Accelerator_Tools2.Model.Helpers;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Action;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Action
{
    public class ActionBase : Base
    {
        private int iD;
        private int actionID;
        private string name;
        private string description;
        private Tag tag;
        private Condition status = Condition.Active;
        private decimal year = DateTime.UtcNow.Year;
        private Months month = (Months)Enum.ToObject(typeof(Months), DateTime.UtcNow.Month);
        private Devision devision;
        private Leaders leader;
        private string changeBy;
        private DateTime changeTime;
        private bool active = true;
        private ICollection<Platform> platforms;
        private CalculationMethod calculation = CalculationMethod.ANC;
        private ObservableCollection<ANCChange> aNCChanges;
        private ObservableCollection<PNC_InterTable> pNCs = new ObservableCollection<PNC_InterTable>();

        public int ID { get => iD; set { iD = value; OnPropertyChange(); } }
        public int ActionID { get => actionID; set { actionID = value; OnPropertyChange(); } }
        public string Name { get => name; set { name = value; OnPropertyChange(); } }
        public string Description { get => description; set { description = value; OnPropertyChange(); } }
        public Tag Tag { get => tag; set { tag = value; OnPropertyChange(); } }
        public Condition Status { get => status; set { status = value; OnPropertyChange(); } }
        public decimal Year { get => year; set { year = value; OnPropertyChange(); } }
        public Months Month { get => month; set { month = value; OnPropertyChange(); } }
        public Devision Devision { get => devision; set { devision = value; OnPropertyChange(); } }
        public Leaders Leader { get => leader; set { leader = value; OnPropertyChange(); } }
        public string ChangeBy { get => changeBy; set { changeBy = value; OnPropertyChange(); } }
        public DateTime ChangeTime { get => changeTime; set { changeTime = value; OnPropertyChange(); } }
        public bool Active { get => active; set { active = value; OnPropertyChange(); } }
        public CalculationMethod Calculation { get => calculation; set { calculation = value; OnPropertyChange(); } }

        public ICollection<Platform> Platforms { get => platforms; set { platforms = value; OnPropertyChange(); } }
        public ObservableCollection<ANCChange> ANCChanges { get => aNCChanges;
            set { aNCChanges = value; OnPropertyChange(); } }

        public ObservableCollection<PNC_InterTable> PNCs
        {
            get => pNCs;
            set
            {
                pNCs = value;
                OnPropertyChange();
            }
        }
    }
}
