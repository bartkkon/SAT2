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
    public class FactoriesViewModel :Base
    {
        public FactoriesViewModel(IFactoriesServices factoriesServices, IMessageBoxService messageBoxService)
        {
            New = new RelayCommand(NewFactories);
            Add = new RelayCommand(AddFactory);
            Update = new RelayCommand(UpdateFactory);
            this.factoriesServices = factoriesServices;
            this.messageBoxService = messageBoxService;
            Factories = this.factoriesServices.Get(true);
        }

        private IEnumerable<Factories> factories;
        private Factories factorySelected;
        private bool onlyActive = true;
        private Visibility addButtonVisibility = Visibility.Collapsed;
        private Visibility updateButtonVisibility = Visibility.Visible;
        private readonly IFactoriesServices factoriesServices;
        private readonly IMessageBoxService messageBoxService;
        private bool enabled_change = false;

        public bool Enabled_change
        {
            get { return enabled_change; }
            set { enabled_change = value;
                OnPropertyChange();
            }
        }


        public Visibility UpdateButtonVisibility
        {
            get { return updateButtonVisibility; }
            set { updateButtonVisibility = value;
                OnPropertyChange();
            }
        }

        public Visibility AddButtonVisibility
        {
            get { return addButtonVisibility; }
            set { addButtonVisibility = value;
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

        public Factories FactoriesSelected
        {
            get { return factorySelected; }
            set { factorySelected = value;
                if (factorySelected != null)
                {
                    Enabled_change = true;
                    if (factorySelected.ID != 0)
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

        public IEnumerable<Factories> Factories
        {
            get { return factories; }
            set { factories = value;
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
            FactoriesSelected = new Factories() { ID = 0, Active = true, };
            Enabled_change = true;
        }
        public void AddFactory()
        {
            factoriesServices.Add(factorySelected);
            messageBoxService.ShowConfirmation("Done", $"New Factory {factorySelected.Plant} Added!");
            FactoriesSelected = null;
            Factories = factoriesServices.Get(onlyActive);
            AddButtonVisibility = Visibility.Collapsed;
            UpdateButtonVisibility = Visibility.Visible;
        }
        public void UpdateFactory()
        {
            factoriesServices.Update(factorySelected);
            messageBoxService.ShowConfirmation("Done", $" Factory {factorySelected.Plant} Data Updateded!");
            FactoriesSelected = null;
            Factories = factoriesServices.Get(onlyActive);
        }
    }
}
