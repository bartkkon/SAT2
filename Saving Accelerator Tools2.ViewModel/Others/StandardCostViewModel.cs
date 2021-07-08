using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.File;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class StandardCostViewModel : Base
    {
        public StandardCostViewModel(IStandardCostServices standardCostServices,
                                     IMessageBoxService messageBoxService,
                                     IFactoriesServices factorierServices)
        {
            Report = new RelayCommand(LoadReport);
            FSDS = new RelayCommand(LoadReportFSDS);
            Special = new RelayCommand(LoadSpecial);
            Clear = new RelayCommand(ClearData);
            this.standardCostServices = standardCostServices;
            this.messageBoxService = messageBoxService;
            this.factorierServices = factorierServices;

            Factories = this.factorierServices.Get();
            FactorySelected = Factories.FirstOrDefault(d => d.Plant == "PLV");
        }

        private decimal specialYear = DateTime.UtcNow.Year + 1;
        private readonly IStandardCostServices standardCostServices;
        private readonly IMessageBoxService messageBoxService;
        private readonly IFactoriesServices factorierServices;

        private ICollection<Factories> factoriers;
        private Factories factorySelected;

        public Factories FactorySelected
        {
            get { return factorySelected; }
            set { factorySelected = value; OnPropertyChange(); }
        }


        public ICollection<Factories> Factories
        {
            get { return factoriers; }
            set { factoriers = value; OnPropertyChange(); }
        }


        public decimal SpecialYear
        {
            get { return specialYear; }
            set { specialYear = value; OnPropertyChange(); }
        }



        public ICommand Report { get; private set; }
        public ICommand FSDS { get; private set; }
        public ICommand Special { get; private set; }
        public ICommand Clear { get; private set; }

        private void LoadReport()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            standardCostServices.UpdateFromReport(factorySelected);
            Mouse.OverrideCursor = default;
        }
        private void LoadReportFSDS()
        {

        }
        private void LoadSpecial()
        {

        }
        private void ClearData()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            standardCostServices.Clear(specialYear);
            Mouse.OverrideCursor = default;
            messageBoxService.ShowConfirmation("Done!", $"Standard Cosr from year {specialYear}, has beed removed!");
        }
    }
}
