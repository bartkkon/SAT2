using Saving_Accelerator_Tools2.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Action
{
    public class ActionBase : Base
    {
        private int iD;
        private string name;
        private string description;
        private string changeBy;
        private DateTime changeTime;
        private bool active;

        public int ID
        {
            get => iD; set
            {
                iD = value;
                OnPropertyChange();
            }
        }
        public string Name
        {
            get => name; set
            {
                name = value;
                Description = name;
                OnPropertyChange();
            }
        }
        public string Description
        {
            get => description; set
            {
                description = value;
                OnPropertyChange();
            }
        }
        public string ChangeBy
        {
            get => changeBy; set
            {
                changeBy = value;
                OnPropertyChange();
            }
        }
        public DateTime ChangeTime
        {
            get => changeTime; set
            {
                changeTime = value;
                OnPropertyChange();
            }
        }
        public bool Active
        {
            get => active; set
            {
                active = value;
                OnPropertyChange();
            }
        }

    }
}
