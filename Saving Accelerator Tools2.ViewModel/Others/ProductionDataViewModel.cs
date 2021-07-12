using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.IServices.Windows;
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
    public class ProductionDataViewModel : Base
    {
        private readonly IFactoriesServices factoriesServices;
        private readonly IWindowsServices windowsServices;
        private readonly IANCsServices aNCsServices;
        private readonly IPNCsServices pNCsServices;
        private readonly IPNCPlatformServices pNCPlatformServices;
        private readonly IMessageBoxService messageBoxService;

        public ProductionDataViewModel(IFactoriesServices factoriesServices,
                                        IWindowsServices windowsServices,
                                        IANCsServices aNCsServices,
                                        IPNCsServices pNCsServices, 
                                        IPNCPlatformServices pNCPlatformServices,
                                        IMessageBoxService messageBoxService)
        {
            MonthlyButton = new RelayCommand(AddMonthyData);
            RevisionButton = new RelayCommand(AddRevisionData);

            this.factoriesServices = factoriesServices;
            this.windowsServices = windowsServices;
            this.aNCsServices = aNCsServices;
            this.pNCsServices = pNCsServices;
            this.pNCPlatformServices = pNCPlatformServices;
            this.messageBoxService = messageBoxService;
            Initialize();
        }

        private void Initialize()
        {
            Factories = factoriesServices.Get();
            Factory = factories.FirstOrDefault();
            Month = (Months[])Enum.GetValues(typeof(Months));
            RevisionsBox = (Revisions[])Enum.GetValues(typeof(Revisions));

            MonthSelected = DateTime.UtcNow.Month != 1 ? (Months)Enum.ToObject(typeof(Months), DateTime.UtcNow.Month - 1) : (Months)Enum.ToObject(typeof(Months), 12);

            MonthlyYear = DateTime.UtcNow.Month != 1 ? DateTime.UtcNow.Year : DateTime.UtcNow.Year - 1;

            RevisionSelected = DateTime.UtcNow.Month <= 3 ? Revisions.EA1 : DateTime.UtcNow.Month <= 6 ? Revisions.EA2 : DateTime.UtcNow.Month <= 9 ? Revisions.EA3 : Revisions.BU;

            RevisonYear = DateTime.UtcNow.Month <= 9 ? DateTime.UtcNow.Year : DateTime.UtcNow.Year + 1;
        }

        public ICollection<Revisions> RevisionsBox { get; private set; }
        private Revisions revisionSelected;
        public ICollection<Months> Month { get; private set; }
        private Months monthSelected;

        private ICollection<Factories> factories;
        private Factories factory;
        private decimal monthlyYear;
        private decimal revisionYear;

        public Revisions RevisionSelected
        {
            get => revisionSelected;
            set { revisionSelected = value; OnPropertyChange(); }
        }

        public decimal MonthlyYear
        {
            get => monthlyYear;
            set { monthlyYear = value; OnPropertyChange(); }
        }
        public decimal RevisonYear
        {
            get => revisionYear;
            set { revisionYear = value; OnPropertyChange(); }
        }

        public Months MonthSelected
        {
            get => monthSelected;
            set { monthSelected = value; OnPropertyChange(); }
        }
        public Factories Factory
        {
            get => factory;
            set { factory = value; OnPropertyChange(); }
        }
        public ICollection<Factories> Factories
        {
            get => factories;
            set { factories = value; OnPropertyChange(); }
        }

        public ICommand MonthlyButton { get; private set; }
        public ICommand RevisionButton { get; private set; }

        private void AddMonthyData()
        {
            windowsServices.MonthlyQuantityWindow(monthlyYear, monthSelected, factory, aNCsServices, pNCsServices, pNCPlatformServices);
        }
        private void AddRevisionData()
        {
            windowsServices.RevisionQuantityWindow(monthlyYear, revisionSelected, factory, aNCsServices, pNCsServices, pNCPlatformServices, messageBoxService);
        }
    }
}
