using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SavingAcceleratorTools2.ProjectModels.Action
{
    public class ActionTree : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string actionName;
        private int actionID;
        private SolidColorBrush backgroundColor = new(Colors.Transparent);
        private SolidColorBrush foregroundColor = new(Colors.Black);
        private bool isSelected;
        private bool actualStart;
        private bool finish;
        private bool bold;

        public bool Bold
        {
            get => bold;
            set { bold = value; OnPropertyChange(); }
        }


        public bool Finish
        {
            get => finish;
            set { finish = value; OnPropertyChange(); }
        }


        public bool ActualStart
        {
            get => actualStart;
            set { actualStart = value; OnPropertyChange(); }
        }


        public ObservableCollection<ActionTree> Tree { get; } = new();

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    if (actionID != 0)
                    {
                        isSelected = value;
                        BackgroundColor = isSelected ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Transparent);
                        OnPropertyChange();
                    }
                }
            }
        }


        public SolidColorBrush ForegroundColor
        {
            get => foregroundColor;
            set { foregroundColor = value; OnPropertyChange(); }
        }


        public SolidColorBrush BackgroundColor
        {
            get => backgroundColor;
            set { backgroundColor = value; OnPropertyChange(); }
        }



        public int ActionID
        {
            get => actionID;
            set { actionID = value; OnPropertyChange(); }
        }


        public string ActionName
        {
            get => actionName;
            set { actionName = value; OnPropertyChange(); }
        }


        protected void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

