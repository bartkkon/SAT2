using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Update;
using Saving_Accelerator_Tools2.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Admin
{
    public class STKUpdateViewModel: INotifyProperty
    {
        public STKUpdateViewModel()
        {
            StandardUpdate = new RelayCommand<object>(Standard_Update);
            DelateYear = new RelayCommand<object>(Delate_Year);
            Manual = new RelayCommand<object>(ManualUpdate);
        }

        private decimal year = DateTime.UtcNow.Year;

        public decimal Year
        {
            get { return year; }
            set
            {
                year = value;
                RisePropoertyChanged();
            }
        }

        public ICommand StandardUpdate
        {
            get; private set;
        }

        public ICommand DelateYear
        {
            get; private set;
        }

        public ICommand Manual
        {
            get; private set;
        }

        public void ManualUpdate(object obj)
        {
            var AddData = new AddingData(year);
            AddData.Show();
        }

        public void Delate_Year(object obj)
        {
            var Results = MessageBox.Show($"Do you want remove {year}?" + Environment.NewLine + "All STK Data?", "Warning!!!!", MessageBoxButton.YesNo);
            if(Results == MessageBoxResult.Yes)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                STK_Controller.Delete_Year(year);
                Mouse.OverrideCursor = null;
            }
        }

        public void Standard_Update(object obj)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var Results = new STK_DataBaseUpdate().StandardUpdate();
            Mouse.OverrideCursor = null;
            if (Results[0] != -1)
                MessageBox.Show("Check Records: " + Results[0] + Environment.NewLine +
                    "Updated Records: " + Results[1] + Environment.NewLine +
                    "New Records: " + Results[2], "STK Updated");
            else
                MessageBox.Show("Problem with find file, more than 30 was not genereted!", "Error");
        }
    }
}
