using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class MainFilterViewModel : Base
    {
        public MainFilterViewModel()
        {

        }
        private bool active;
        private bool idea;
        private decimal year;
        private bool electronic;
        private bool mechanic;
        private bool nVR;
        private bool pLV;
        private bool zM;
        private List<Leaders> leaders;
        private Leaders selectedLeader;

        public Leaders SelectedLeader
        {
            get { return selectedLeader; }
            set { selectedLeader = value; }
        }


        public List<Leaders> Leaders
        {
            get { return leaders; }
            set { leaders = value; }
        }


        public bool ZM
        {
            get { return zM; }
            set { zM = value; }
        }


        public bool PLV
        {
            get { return pLV; }
            set { pLV = value; }
        }


        public bool NVR
        {
            get { return nVR; }
            set { nVR = value; }
        }


        public bool Mechanic
        {
            get { return mechanic; }
            set { mechanic = value; }
        }


        public bool Electronic
        {
            get { return electronic; }
            set { electronic = value; }
        }


        public decimal Year
        {
            get { return year; }
            set { year = value; }
        }


        public bool Idea
        {
            get { return idea; }
            set { idea = value; }
        }


        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

    }
}
