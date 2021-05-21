using Saving_Accelerator_Tools2.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class BottonBarViewModel
    {
        #region Constructors
        public BottonBarViewModel()
        {
            FirstYear = new RelayCommand<object>(First);
            SecondYear = new RelayCommand<object>(Second);
        }
        #endregion

        #region Buttons
        public ICommand FirstYear
        {
            get; private set;
        }
        public ICommand SecondYear
        {
            get; private set;
        }
        #endregion

        #region Buttons Functions
        private void First(object obj)
        {
            Mediator.Mediator.NotifyColleagues("Tables_ChangeDisplay", false);
        }
        private void Second(object obj)
        {
            Mediator.Mediator.NotifyColleagues("Tables_ChangeDisplay", true);
        }
        #endregion
    }
}
