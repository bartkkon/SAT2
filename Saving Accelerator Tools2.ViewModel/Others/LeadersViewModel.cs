using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class LeadersViewModel : Base
    {
        private readonly ILeadersServices leadersServices;
        private readonly IDevisionServices devisionServices;
        private readonly IFactoriesServices factoriesServices;
        private readonly IMessageBoxService messageBoxService;

        public LeadersViewModel(ILeadersServices leadersServices, IDevisionServices devisionServices, IFactoriesServices factoriesServices, IMessageBoxService messageBoxService)
        {
            this.leadersServices = leadersServices;
            this.devisionServices = devisionServices;
            this.factoriesServices = factoriesServices;
            this.messageBoxService = messageBoxService;
            New = new RelayCommand(NewLeader);
            Add = new RelayCommand(AddLeader);
            Update = new RelayCommand(UpdateLeader);
            Leaders = leadersServices.Get(onlyActive);
            FilteredDevisions.Add(new Devision { Name = "All", Factory = new Factories() });
            FilteredDevisions.AddRange(devisionServices.Get(true).ToList());
            FilteredDevision = filteredDevisions.First();
            FilteredFactories.Add(new Factories { Plant = "All" });
            FilteredFactories.AddRange(factoriesServices.Get(true).ToList());
            filteredFactory = filteredFactories.First();
            Devisions = devisionServices.Get(true).ToList();
            
        }

        private ICollection<Leaders> leaders;
        private Leaders leaderSelected;
        private bool onlyActive = true;
        private List<Devision> filteredDevisions = new List<Devision>();
        private Devision filteredDevision;
        private List<Factories> filteredFactories = new List<Factories>();
        private Factories filteredFactory;
        private bool enabled_Change;
        private ICollection<Devision> devisions;
        private Devision selectedDevision;
        private Visibility addButtonVisibility = Visibility.Collapsed;
        private Visibility updateButtonVisibility = Visibility.Visible;
        private LeadersSearchCriteria searchCriteria = new LeadersSearchCriteria();

        public Visibility UpdateButtonVisibility
        {
            get { return updateButtonVisibility; }
            set { updateButtonVisibility = value; OnPropertyChange(); }
        }
        public Visibility AddButtonVisibility
        {
            get { return addButtonVisibility; }
            set
            { addButtonVisibility = value; OnPropertyChange(); }
        }
        public Devision SelectedDevision
        {
            get { return selectedDevision; }
            set { selectedDevision = value; OnPropertyChange(); }
        }
        public ICollection<Devision> Devisions
        {
            get { return devisions; }
            set { devisions = value; OnPropertyChange(); }
        }
        public bool Enabled_change
        {
            get { return enabled_Change; }
            set { enabled_Change = value; OnPropertyChange(); }
        }
        public Factories FilteredFactory
        {
            get { return filteredFactory; }
            set { 
                filteredFactory = value;
                searchCriteria.FactoryID = filteredFactory.Plant == "All" ? null : filteredFactory.ID;
                OnPropertyChange();
                Leaders = leadersServices.Get(searchCriteria);
            }
        }
        public List<Factories> FilteredFactories
        {
            get { return filteredFactories; }
            set { filteredFactories = value; OnPropertyChange(); }
        }
        public Devision FilteredDevision
        {
            get { return filteredDevision; }
            set {
                filteredDevision = value;
                searchCriteria.DevisionID = filteredDevision.Name == "All" ? null : filteredDevision.ID;
                OnPropertyChange();
                Leaders = leadersServices.Get(searchCriteria);
            }
        }
        public List<Devision> FilteredDevisions
        {
            get { return filteredDevisions; }
            set { filteredDevisions = value; OnPropertyChange(); }
        }
        public bool OnlyActive
        {
            get { return onlyActive; }
            set { 
                onlyActive = value;
                searchCriteria.Active = onlyActive;
                OnPropertyChange();
                Leaders = leadersServices.Get(searchCriteria);
            }
        }
        public Leaders LeadersSelected
        {
            get { return leaderSelected; }
            set { 
                leaderSelected = value; 
                if(leaderSelected != null)
                {
                    Enabled_change = true;
                    if(leaderSelected.ID !=0)
                    {
                        AddButtonVisibility = Visibility.Hidden;
                        UpdateButtonVisibility = Visibility.Visible;
                    }
                }
                else
                {
                    Enabled_change = false;
                }
                OnPropertyChange(); 
            }
        }
        public ICollection<Leaders> Leaders
        {
            get { return leaders; }
            set { leaders = value; OnPropertyChange(); }
        }

        public ICommand New { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Update { get; private set; }

        private void UpdateLeader()
        {
            leadersServices.Update(leaderSelected);
            messageBoxService.ShowConfirmation("Done", $"Data for Leader {leaderSelected.FullName} Updated!");
            LeadersSelected = null;
        }

        private void AddLeader()
        {
            leadersServices.Add(leaderSelected);
            messageBoxService.ShowConfirmation("Done", $"New Leader {leaderSelected.FullName} Added!");
            LeadersSelected = null;
            Leaders = leadersServices.Get(searchCriteria);
            AddButtonVisibility = Visibility.Collapsed;
            UpdateButtonVisibility = Visibility.Visible;
        }

        private void NewLeader()
        {
            AddButtonVisibility = Visibility.Visible;
            UpdateButtonVisibility = Visibility.Collapsed;
            LeadersSelected = new Leaders() { ID = 0, Active = true };
            Enabled_change = true;

        }
    }
}
