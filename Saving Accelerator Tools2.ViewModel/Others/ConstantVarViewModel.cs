using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class ConstantVarViewModel : Base
    {
        private readonly IFactoriesServices factoriesServices;
        private readonly IConstatVariablesServices constatVariablesServices;
        private readonly IMessageBoxService messageBoxService;

        public ConstantVarViewModel(IFactoriesServices factoriesServices,
                                    IConstatVariablesServices constatVariablesServices,
                                    IMessageBoxService messageBoxService)
        {
            NewButton = new RelayCommand(New);
            SaveButton = new RelayCommand(Save);

            
            this.factoriesServices = factoriesServices;
            this.constatVariablesServices = constatVariablesServices;
            this.messageBoxService = messageBoxService;
            InitialData();
        }

        private void InitialData()
        {
            Year = DateTime.UtcNow.Year;
            Factories = factoriesServices.Get(true).ToList();
            FactorySelected = factories.First();
            ConstantEnumList = (Constant[])Enum.GetValues(typeof(Constant));
            Revisions = (Revisions[])Enum.GetValues(typeof(Revisions));
            Currencies = (Currency[])Enum.GetValues(typeof(Currency));
            ConstrantList = constatVariablesServices.Get(year, factorySelected);
        }

        private decimal year;
        private ICollection<Factories> factories;
        private Factories factorySelected;

        private ICollection<ConstantVariables> constrantList;
        private ConstantVariables constranstSelected = new();

        private ICollection<Constant> constantEnumList;
        private bool constantEnumEnable;

        private ICollection<Revisions> revisions;
        private ICollection<Currency> currencies;
        private bool revisionEnabled;


        public Constant ConstranstSelected_Name
        {
            get => ConstrantSelected.Name;
            set
            {
                //if (constranstSelected != null)
                //{
                    ConstrantSelected.Name = value;
                //}
                RevisionEnabled = constranstSelected.Name == Constant.DM;
                if (constranstSelected.Name == Constant.ECCC)
                {
                    constranstSelected.Revision = null;
                }
                OnPropertyChange();
            }
        }


        public bool RevisionEnabled
        {
            get => revisionEnabled;
            set { revisionEnabled = value; OnPropertyChange(); }
        }


        public ICollection<Currency> Currencies
        {
            get => currencies;
            set { currencies = value; OnPropertyChange(); }
        }


        public ICollection<Revisions> Revisions
        {
            get => revisions;
            set { revisions = value; OnPropertyChange(); }
        }


        public bool ConstantEnumEnable
        {
            get => constantEnumEnable;
            set { constantEnumEnable = value; OnPropertyChange(); }
        }


        public ICollection<Constant> ConstantEnumList
        {
            get => constantEnumList;
            set { constantEnumList = value; OnPropertyChange(); }
        }


        public ConstantVariables ConstrantSelected
        {
            get => constranstSelected;
            set
            {
                constranstSelected = value;
                ConstantEnumEnable = false;
                if (constranstSelected == null)
                {
                    constranstSelected = new() { Year = DateTime.UtcNow.Year };
                }
                ConstranstSelected_Name = constranstSelected.Name;
                OnPropertyChange();
            }
        }


        public ICollection<ConstantVariables> ConstrantList
        {
            get => constrantList;
            set { constrantList = value; OnPropertyChange(); }
        }


        public Factories FactorySelected
        {
            get => factorySelected;
            set { factorySelected = value; ConstrantList = constatVariablesServices.Get(year, factorySelected); OnPropertyChange(); }
        }


        public ICollection<Factories> Factories
        {
            get => factories;
            set { factories = value; OnPropertyChange(); }
        }

        public decimal Year
        {
            get => year;
            set { year = value; ConstrantList = constatVariablesServices.Get(year, factorySelected); OnPropertyChange(); }
        }

        public ICommand NewButton { get; private set; }
        public ICommand SaveButton { get; private set; }
        private void New()
        {
            ConstantVariables NewRecord = new()
            {
                ID = 0,
                Year = DateTime.UtcNow.Year,
            };

            ConstrantSelected = NewRecord;
            ConstantEnumEnable = true;
        }
        private void Save()
        {
            if (ConstrantSelected.ID == 0)
            {
                var BaseList = constatVariablesServices.Get(ConstrantSelected.Year, ConstrantSelected.Factory);

                if (BaseList.Any(f => f.Name == ConstrantSelected.Name && f.Revision == ConstrantSelected.Revision))
                {
                    messageBoxService.ShowConfirmation("Warning!", "Record with this data exist in Database.");
                    return;
                }
            }
            constatVariablesServices.Set(constranstSelected);
            ConstrantSelected = new() { Year = DateTime.UtcNow.Year };
            ConstrantList = constatVariablesServices.Get(year, factorySelected);
        }
    }
}
