using Saving_Accelerator_Tools2.DataBaseIServices.ActionService;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.ActionCriteria;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Action;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class MainFilterViewModel : Base
    {
        public MainFilterViewModel(ILoginUserService LoggedUser,
                                   IFactoriesServices factoriesServices,
                                   IDevisionServices devisionServices,
                                   ILeadersServices leadersServices,
                                   ITreeServices treeServices, 
                                   ActionBase LoadedAction, ILoadedAction actionService)
        {
            this.LoggedUser = LoggedUser;
            this.factoriesServices = factoriesServices;
            this.devisionServices = devisionServices;
            this.leadersServices = leadersServices;
            this.treeServices = treeServices;
            loadedAction = LoadedAction;
            this.actionService = actionService;
            NewActionButton = new RelayCommand(NewAction);

            InitialData();
        }

        private void InitialData()
        {
            Year = DateTime.UtcNow.Year;
            Statuses = (Condition[])Enum.GetValues(typeof(Condition));
            StatusSelected = Condition.Active;

            if (LoggedUser.Get().Type is AccountTypes.Administrator)
            {
                Factories = factoriesServices.Get(true);
                FactorySelected = LoggedUser.Get().Devision.Factory;
            }
            else
            {
                Factories.Add(LoggedUser.Get().Devision.Factory);
                FactorySelected = factories.First();
            }
            if (LoggedUser.Get().Type is AccountTypes.Administrator or AccountTypes.Menager)
            {
                Devisions = new ObservableCollection<Devision>(devisionServices.Get(factorySelected));
                DevisionSelected = devisions.First();
            }
            else
            {
                Devisions.Add(LoggedUser.Get().Devision);
                DevisionSelected = devisions.First();
            }

            ReloadLeader();
        }

        private ICollection<Condition> statuses;
        private ICollection<Factories> factories;
        private ObservableCollection<Devision> devisions = new();
        private ObservableCollection<Leaders> leaders = new();
        private ObservableCollection<ActionTree> treeData;
        private LeadersSearchCriteria criteria = new() { Active = true };
        private TreeCriteria treeCriteria = new();
        private Condition statusSelected;
        private Factories factorySelected;
        private Devision devisionSelected;
        private Leaders leaderSelected;
        private readonly ILoginUserService LoggedUser;
        private readonly IFactoriesServices factoriesServices;
        private readonly IDevisionServices devisionServices;
        private readonly ILeadersServices leadersServices;
        private readonly ITreeServices treeServices;
        private ActionBase loadedAction;
        private readonly ILoadedAction actionService;
        private decimal year;

        public ObservableCollection<ActionTree> TreeData
        {
            get => treeData;
            set { treeData = value; OnPropertyChange(); }
        }

        public decimal Year
        {
            get => year;
            set
            {
                year = value;
                treeCriteria.Year = year;
                ReloadTree();
                OnPropertyChange();
            }
        }


        public Leaders LeadersSelected
        {
            get => leaderSelected;
            set
            {
                leaderSelected = value;
                treeCriteria.Leader = leaderSelected;
                ReloadTree();
                OnPropertyChange();
            }
        }


        public Devision DevisionSelected
        {
            get => devisionSelected;
            set
            {
                devisionSelected = value;
                criteria.DevisionID = devisionSelected.ID;
                treeCriteria.Devisions = devisionSelected;
                ReloadLeader();
                OnPropertyChange();
            }
        }


        public Factories FactorySelected
        {
            get => factorySelected;
            set
            {
                factorySelected = value;
                criteria.FactoryID = factorySelected.ID;
                treeCriteria.Factory = factorySelected;
                Devisions = new ObservableCollection<Devision>(devisionServices.Get(factorySelected));
                DevisionSelected = devisions.First();
                OnPropertyChange();
            }
        }


        public Condition StatusSelected
        {
            get => statusSelected;
            set
            {
                statusSelected = value;
                treeCriteria.Conditions = statusSelected;
                ReloadTree();
                OnPropertyChange();
            }
        }

        public ObservableCollection<Leaders> Leaders
        {
            get => leaders;
            set { leaders = value; OnPropertyChange(); }
        }

        public ObservableCollection<Devision> Devisions
        {
            get => devisions;
            set { devisions = value; OnPropertyChange(); }
        }


        public ICollection<Factories> Factories
        {
            get => factories;
            set { factories = value; OnPropertyChange(); }
        }

        public ICollection<Condition> Statuses
        {
            get => statuses;
            set { statuses = value; OnPropertyChange(); }
        }

        public ICommand NewActionButton { get; private set; }

        private void NewAction()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            actionService.New();
            Mouse.OverrideCursor = default;
        }
        private void ReloadLeader()
        {
            if (leaders != null)
            {
                leaders.Clear();
            }
            leaders.Add(new() { Name = "All" });
            foreach (var leader in leadersServices.Get(criteria))
            {
                leaders.Add(leader);
            }
            OnPropertyChange(nameof(Leaders));
            LeadersSelected = leaders.First();
        }
        private void ReloadTree()
        {
            TreeData = treeServices.Load(treeCriteria);
        }
    }
}
