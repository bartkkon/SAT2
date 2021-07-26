using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.Action.InterTables;
using Saving_Accelerator_Tools2.Model.Action.Sub;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class PNCViewModel : Base
    {
        private ActionBase loadedAction;
        private readonly IMessageBoxService messageBoxService;

        public PNCViewModel(ActionBase loadedAction,
            IMessageBoxService messageBoxService)
        {
            AddDataButton = new RelayCommand(AddData);
            this.loadedAction = loadedAction;
            this.messageBoxService = messageBoxService;
            loadedAction.PropertyChanged += LoadedAction_PropertyChanged;
        }

        private void LoadedAction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ActionBase.PNCs))
            {
                if(loadedAction.PNCs.Count != 0)
                {
                    UpdateData = true;
                }
                else
                {
                    NewData = true;
                }
            }
        }

        public ICommand AddDataButton { get; private set; }
        private Visibility addDataBoxVisibility = Visibility.Collapsed;
        private string addDataBox;
        private bool newData = true;
        private bool updateData;

        public ActionBase LoadedAction
        {
            get => loadedAction;
            set
            {
                loadedAction = value;
                OnPropertyChange();
            }
        }
        public bool UpdateData
        {
            get => updateData;
            set
            {
                updateData = value;
                newData = !value;
                OnPropertyChange(nameof(NewData));
                OnPropertyChange();
            }
        }

        public bool NewData
        {
            get => newData;
            set
            {
                newData = value;
                updateData = !value;
                OnPropertyChange(nameof(UpdateData));
                OnPropertyChange();
            }
        }


        public string AddDataBox
        {
            get => addDataBox;
            set
            {
                addDataBox = value;
                ConvertData();
                OnPropertyChange();
            }
        }
        public Visibility AddDataBoxVisibility { get => addDataBoxVisibility; set { addDataBoxVisibility = value; OnPropertyChange(); } }

        public void AddData()
        {
            AddDataBoxVisibility = Visibility.Visible;
        }

        public void ConvertData()
        {
            if(string.IsNullOrEmpty(addDataBox.Trim()))
            {
                AddDataBoxVisibility = Visibility.Collapsed;
                return;
            }
            if(newData)
            {
                if(loadedAction.PNCs.Count != 0)
                {
                    MessageBoxResult result = messageBoxService.Ask("Warning!", $"PNC list is not empty.{Environment.NewLine}Do you want remove and put new?");
                    if(result == MessageBoxResult.Yes)
                    {
                        loadedAction.PNCs.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            string[] Rows = addDataBox.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach(var row in Rows)
            {
                if(row.Trim().Length == 9 && row.Trim().Remove(3) == "911")
                {
                    PNC newRecord = new()
                    {
                        Item = row.Trim(),
                    };
                    PNC_InterTable newInter = new()
                    {
                        Action = loadedAction,
                        PNC = newRecord
                    };
                    if (updateData)
                    {
                        if(!loadedAction.PNCs.Any(item=>item.PNC.Item == newRecord.Item))
                        {
                            loadedAction.PNCs.Add(newInter);
                        }
                    }
                    else { loadedAction.PNCs.Add(newInter); }
                }
            }

            if(loadedAction.PNCs.Count != 0)
            {
                UpdateData = true;
            }
            else
            {
                NewData = true;
            }
            AddDataBoxVisibility = Visibility.Collapsed;
            addDataBox = string.Empty;
            OnPropertyChange(nameof(AddDataBox));
        }
    }
}
