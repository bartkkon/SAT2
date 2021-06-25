using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class DevisionsViewModel : Base
    {
        public DevisionsViewModel(IMessageBoxService messageBoxService, IFactoriesServices factoriesServices, IDevisionServices devisionServices)
        {
            New = new RelayCommand(NewFactories);
            Add = new RelayCommand(AddFactory);
            Update = new RelayCommand(UpdateFactory);
            this.messageBoxService = messageBoxService;
            this.factoriesServices = factoriesServices;
            this.devisionServices = devisionServices;
            Factories = this.factoriesServices.Get(onlyActive);
            Devisions = this.devisionServices.Get(onlyActive);
        }

        private IEnumerable<Devision> devisions;
        private Devision devisonSelected;
        private bool onlyActive = true;
        private Visibility addButtonVisibility = Visibility.Collapsed;
        private Visibility updateButtonVisibility = Visibility.Visible;
        private readonly IMessageBoxService messageBoxService;
        private readonly IFactoriesServices factoriesServices;
        private readonly IDevisionServices devisionServices;
        private bool enabled_change = false;
        private IEnumerable<Factories> factories;


        public IEnumerable<Factories> Factories
        {
            get { return factories; }
            set { factories = value;
                OnPropertyChange();
            }
        }


        public bool Enabled_change
        {
            get { return enabled_change; }
            set
            {
                enabled_change = value;
                OnPropertyChange();
            }
        }
        public bool OnlyActive
        {
            get { return onlyActive; }
            set { onlyActive = value; 
                OnPropertyChange(); 
            }
        }

        public Devision DevisionSelected
        {
            get { return  devisonSelected; }
            set
            {
                devisonSelected = value;
                if(devisonSelected != null)
                {
                    Enabled_change = true;
                    
                    if (devisonSelected.ID != 0)
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

        public IEnumerable<Devision> Devisions
        {
            get { return devisions; }
            set { devisions = value;
                OnPropertyChange();
            }
        }

        public Visibility UpdateButtonVisibility
        {
            get { return updateButtonVisibility; }
            set
            {
                updateButtonVisibility = value;
                OnPropertyChange();
            }
        }

        public Visibility AddButtonVisibility
        {
            get { return addButtonVisibility; }
            set
            {
                addButtonVisibility = value;
                OnPropertyChange();
            }
        }

        public ICommand New { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Update { get; private set; }

        public void NewFactories()
        {
            AddButtonVisibility = Visibility.Visible;
            UpdateButtonVisibility = Visibility.Collapsed;
            DevisionSelected = new Devision() { ID = 0, Active = true };
            Enabled_change = true;
        }
        public void AddFactory()
        {
            if(!string.IsNullOrEmpty(devisonSelected.Name) && devisonSelected.Factory != null)
            {
                devisionServices.Add(devisonSelected);
                messageBoxService.ShowConfirmation("Done!", $"New Devison {devisonSelected.Name} for Factory {devisonSelected.Factory.Plant} Added!");
                DevisionSelected = null;
                Devisions = devisionServices.Get(onlyActive);
                AddButtonVisibility = Visibility.Collapsed;
                UpdateButtonVisibility = Visibility.Visible;
            }
        }
        public void UpdateFactory()
        {
            if (!string.IsNullOrEmpty(devisonSelected.Name) && devisonSelected.Factory != null)
            {
                devisionServices.Update(devisonSelected);
                messageBoxService.ShowConfirmation("Done!", $"Devison {devisonSelected.Name} for Factory {devisonSelected.Factory.Plant} Data Updated!");
                DevisionSelected = null;
                Devisions = devisionServices.Get(onlyActive);
            }
        }
    }
}
