using Microsoft.Win32;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.Windows.ViewModels
{
    public class PNCSpecialNewDataViewModel : INotifyProperty
    {
        #region Constructors
        public PNCSpecialNewDataViewModel()
        {
            SaveButton = new RelayCommand<Window>(Save);
            TemplateButton = new RelayCommand<object>(Template);
            CancelButton = new RelayCommand<Window>(Cancel);
        }
        #endregion

        #region Private Variables
        private string _Data;
        private List<PNCSpecialModel> _DataModels = new List<PNCSpecialModel>();
        #endregion

        #region Public Variables
        public string TextBoxData
        {
            get { return _Data; }
            set
            {
                _Data = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand SaveButton { get; private set; }
        public ICommand TemplateButton { get; private set; }
        public ICommand CancelButton { get; private set; }

        public void Save(Window window)
        {
            ConvertStringToModels(window);
        }
        public void Template(object obj)
        {
            string Template = @"\\PLWS4031\Project\CAD\Work\bartkkon\EC_Accelerator_Data\PNCSpec_Template.xlsm";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "PNCSpec_Template",
                DefaultExt = "Xlsm",
                Filter = "Excel Files (*.xlsm)|*xlsm",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }

                File.Copy(Template, saveFileDialog.FileName);
            }
        }
        public void Cancel(Window window)
        {
            if (_Data != null)
            {
                var Results = MessageBox.Show("Now this windows will be close and adding data will be lose." + Environment.NewLine + "Do you want do this?", "Warning!", MessageBoxButton.YesNo);
                if (Results == MessageBoxResult.Yes)
                {
                    window.Close();
                }
                else if (Results == MessageBoxResult.No)
                {
                    return;
                }
            }
            else
            {
                window.Close();
            }
        }
        #endregion

        #region Functions
        private void ConvertStringToModels(Window window)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (_Data != null)
            {
                string[] DataLine = _Data.Split(Environment.NewLine);
                foreach (var NewRow in DataLine)
                {
                    if (NewRow != string.Empty)
                    {
                        var NewData = NewRow.Split(';');
                        if ((NewData.Length % 4) != 0)
                        {
                            MessageBox.Show("Some data are not correct." + Environment.NewLine + "Please correct data", "Warning!", MessageBoxButton.OK);
                            _DataModels = new List<PNCSpecialModel>();
                            return;
                        }

                        var NewPNCRecord = new PNCSpecialModel()
                        {
                            PNC = NewData[0],
                        };
                        if(decimal.TryParse(NewData[1], out decimal ECCCValue))
                        {
                            NewPNCRecord.ECCC = ECCCValue;
                        }

                        int OldComponents = 2;
                        int NewComponents = (NewData.Length / 2) + 1;

                        for (; OldComponents <= (NewData.Length / 2); OldComponents += 2, NewComponents += 2)
                        {
                            if (!(NewData[OldComponents] == string.Empty && NewData[NewComponents] == string.Empty))
                            {
                                var NewANC = new PNCSPecial_ANCChangeModel()
                                {
                                    Old_ANC = NewData[OldComponents],
                                    New_ANC = NewData[NewComponents],

                                };
                                NewANC.Old_Q = decimal.TryParse(NewData[OldComponents + 1], out decimal OldValue) ? OldValue : 0;
                                NewANC.New_Q = decimal.TryParse(NewData[NewComponents + 1], out decimal NewValue) ? NewValue : 0;
                                NewPNCRecord.ANCChange.Add(NewANC);
                            }
                        }

                        _DataModels.Add(NewPNCRecord);
                    }
                }
            }
            Mediator.Mediator.NotifyColleagues("PNCSpecial_LoadNewData", _DataModels);
            Mouse.OverrideCursor = null;
            window.Close();
        }
        #endregion
    }
}
