using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.Action.Sub;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class PlatformViewModel : Base
    {
        private readonly ActionBase loadedAction;

        public PlatformViewModel(ActionBase LoadedAction)
        {
            loadedAction = LoadedAction;
            loadedAction.PropertyChanged += LoadedAction_PropertyChanged;
        }

        private void LoadedAction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ICollection<Platform> platforms = (sender as ActionBase).Platforms;
            if (platforms == null)
            {
                return;
            }
            DMD_FS = platforms.Any(s => s.Structure == Structure.DMD && s.Installation == Installation.FS);
            DMD_FI = platforms.Any(s => s.Structure == Structure.DMD && s.Installation == Installation.FI);
            DMD_BI = platforms.Any(s => s.Structure == Structure.DMD && s.Installation == Installation.BI);
            DMD_FSBU = platforms.Any(s => s.Structure == Structure.DMD && s.Installation == Installation.FSBU);
            D45_FS = platforms.Any(s => s.Structure == Structure.D45 && s.Installation == Installation.FS);
            D45_FI = platforms.Any(s => s.Structure == Structure.D45 && s.Installation == Installation.FI);
            D45_BI = platforms.Any(s => s.Structure == Structure.D45 && s.Installation == Installation.BI);
            D45_FSBU = platforms.Any(s => s.Structure == Structure.DMD && s.Installation == Installation.FSBU);
        }

        private bool dmd;
        private bool d45;
        private bool dmd_fs;
        private bool dmd_fi;
        private bool dmd_bi;
        private bool dmd_fsbu;
        private bool d45_fs;
        private bool d45_fi;
        private bool d45_bi;
        private bool d45_fsbu;

        public bool D45_FSBU
        {
            get => d45_fsbu;
            set
            {
                d45_fsbu = value;
                Check(value, Structure.D45, Installation.FSBU);
                OnPropertyChange();
            }
        }
        public bool D45_BI
        {
            get => d45_bi;
            set
            {
                d45_bi = value;
                Check(value, Structure.D45, Installation.BI);
                OnPropertyChange();
            }
        }
        public bool D45_FI
        {
            get => d45_fi;
            set
            {
                d45_fi = value;
                Check(value, Structure.D45, Installation.FI);
                OnPropertyChange();
            }
        }

        public bool D45_FS
        {
            get => d45_fs;
            set
            {
                d45_fs = value;
                Check(value, Structure.D45, Installation.FS);
                OnPropertyChange();
            }
        }

        public bool DMD_FSBU
        {
            get => dmd_fsbu;
            set
            {
                dmd_fsbu = value;
                Check(value, Structure.DMD, Installation.FSBU);
                OnPropertyChange();
            }
        }
        public bool DMD_BI
        {
            get => dmd_bi;
            set
            {
                dmd_bi = value;
                Check(value, Structure.DMD, Installation.BI);
                OnPropertyChange();
            }
        }
        public bool DMD_FI
        {
            get => dmd_fi;
            set
            {
                dmd_fi = value;
                Check(value, Structure.DMD, Installation.FI);
                OnPropertyChange();
            }
        }

        public bool DMD_FS
        {
            get => dmd_fs;
            set
            {
                dmd_fs = value;
                Check(value, Structure.DMD, Installation.FS);
                OnPropertyChange();
            }
        }


        public bool D45
        {
            get => d45;
            set
            {
                if (d45 != value)
                {
                    D45Check(value);
                }
                d45 = value;

                OnPropertyChange();
            }
        }

        public bool DMD
        {
            get => dmd;
            set
            {
                if (dmd != value)
                {
                    DMDCheck(value);
                }
                dmd = value;
                OnPropertyChange();
            }
        }

        private void DMDCheck(bool dmd)
        {
            DMD_FS = dmd;
            DMD_FI = dmd;
            DMD_BI = dmd;
            DMD_FSBU = dmd;
        }
        private void D45Check(bool d45)
        {
            D45_FS = d45;
            D45_FI = d45;
            D45_BI = d45;
            D45_FSBU = d45;
        }
        private void Check(bool set, Structure structure, Installation installation)
        {
            if (structure == Structure.DMD)
            {

                dmd = dmd_fi && dmd_fs && dmd_bi && dmd_fsbu;
                OnPropertyChange(nameof(DMD));
            }
            else
            {
                d45 = d45_fi && d45_fs && d45_bi && d45_fsbu;
                OnPropertyChange(nameof(D45));
            }

            if(loadedAction.Platforms == null)
            {
                loadedAction.Platforms = new List<Platform>();
            }

            if (loadedAction.Platforms.Any(p => p.Structure == structure && p.Installation == installation))
            {
                if (!set)
                {
                    loadedAction.Platforms.Remove(loadedAction.Platforms.First(p => p.Structure == structure && p.Installation == installation));
                }
            }
            else
            {
                if (set)
                {
                    loadedAction.Platforms.Add(new() { Structure = structure, Installation = installation });
                }
            }
        }
    }
}
