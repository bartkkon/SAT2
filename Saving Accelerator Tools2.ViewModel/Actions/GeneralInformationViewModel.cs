using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class GeneralInformationViewModel : Base
    {
        private ActionBase loadedAction;
        private readonly ILoginUserService loggedUser;
        private readonly IFactoriesServices factoriesServices;
        private readonly IDevisionServices devisionServices;
        private readonly ILeadersServices leadersServices;
        private readonly ITagService tagService;

        public GeneralInformationViewModel(ActionBase LoadedAction,
                                           ILoginUserService loggedUser,
                                           IFactoriesServices factoriesServices,
                                           IDevisionServices devisionServices,
                                           ILeadersServices leadersServices,
                                           ITagService tagService)
        {
            loadedAction = LoadedAction;
            this.loggedUser = loggedUser;
            this.factoriesServices = factoriesServices;
            this.devisionServices = devisionServices;
            this.leadersServices = leadersServices;
            this.tagService = tagService;
            InitialzieData();
            loadedAction.PropertyChanged += LoadedAction_PropertyChanged;
        }

        private void InitialzieData()
        {
            Months = (Months[])Enum.GetValues(typeof(Months));
            Statuses = (Condition[])Enum.GetValues(typeof(Condition));
            if (loggedUser.Get().Type != AccountTypes.Administrator)
            {
                Statuses.Remove(Condition.Cancelled);
                Plants.Add(loggedUser.Get().Devision.Factory);
                Devisions.Add(loggedUser.Get().Devision);
            }
            else
            {
                Plants = factoriesServices.Get(true);
            }
            PlantSelected = plants.First();
        }

        private ICollection<Tag> tags;
        private ICollection<Condition> statuses;
        private ICollection<Devision> devisions;
        private ICollection<Factories> plants;
        private Factories plantSelected;
        private ICollection<Months> months;
        private ICollection<Leaders> leader;

        public ICollection<Leaders> Leaders { get => leader; set { leader = value; OnPropertyChange(); } }
        public ICollection<Months> Months { get => months; set { months = value; OnPropertyChange(); } }
        public ICollection<Factories> Plants { get => plants; set { plants = value; OnPropertyChange(); } }
        public Factories PlantSelected
        {
            get => plantSelected;
            set
            {
                if (plantSelected != value)
                {
                    devisions = devisionServices.Get(value).ToList();
                    Devisions = devisions;
                }
                plantSelected = value;
                OnPropertyChange();
            }
        }
        public ICollection<Devision> Devisions { get => devisions; set { devisions = value; OnPropertyChange(); } }
        public ICollection<Condition> Statuses { get => statuses; set { statuses = value; OnPropertyChange(); } }
        public ICollection<Tag> Tags { get => tags; set { tags = value; OnPropertyChange(); } }
        public ActionBase LoadedAction { get => loadedAction; set { loadedAction = value; OnPropertyChange(); } }

        private void LoadedAction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(loadedAction.Year))
            {
                TagSearchCriteria Search = new()
                {
                    Active = true,
                    FactoryID = plantSelected.ID,
                    Until = (sender as ActionBase).Year,
                    From = (sender as ActionBase).Year,
                };
                Tags = tagService.Get(Search);
            }
            if (e.PropertyName == nameof(loadedAction.Devision))
            {
                LeadersSearchCriteria search = new()
                {
                    Active = true,
                    DevisionID = (sender as ActionBase).Devision.ID,
                    FactoryID = (sender as ActionBase).Devision.Factory.ID,
                };
                Leaders = leadersServices.Get(search);
                if ((sender as ActionBase).Leader == null)
                {
                    (sender as ActionBase).Leader = leader.FirstOrDefault(g => g.Name == loggedUser.Get().Name && g.Surname == loggedUser.Get().Surname);
                }
            }
        }
    }
}
