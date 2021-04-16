using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Windows.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Admin
{
    public class AdminQuantityViewModel : INotifyProperty
    {
        #region Constructors
        public AdminQuantityViewModel()
        {
            Month_Button = new RelayCommand<object>(ButtonMonth);
            Revision_Button = new RelayCommand<object>(ButtonRevision);
        }
        #endregion

        #region Private Variables
        private bool pnc = true;
        private bool anc = false;
        private decimal year = DateTime.UtcNow.Month is 1 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
        private decimal month = DateTime.UtcNow.Month is 1 ? 12 : DateTime.UtcNow.Month - 1;
        private int revisionSelect = 0; // 0 = BU, 1 = EA1, 2 = EA2, 3 = EA4
        #endregion

        #region Public Variables
        public bool PNC_CheckBox
        {
            get { return pnc; }
            set
            {
                pnc = value;
                RisePropoertyChanged();
            }
        }
        public bool ANC_CheckBox
        {
            get { return anc; }
            set
            {
                anc = value;
                RisePropoertyChanged();
            }
        }
        public decimal Year_Num
        {
            get { return year; }
            set
            {
                year = value;
                RisePropoertyChanged();
            }
        }
        public decimal Month_Num
        {
            get { return month; }
            set
            {
                month = value;
                RisePropoertyChanged();
            }
        }
        public int Revison_selection
        {
            get { return revisionSelect; }
            set
            {
                revisionSelect = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand Month_Button
        {
            get; private set;
        }
        public ICommand Revision_Button
        {
            get; private set;
        }
        #endregion

        #region Private Functions
        private void ButtonMonth(object obj)
        {

            var AddDataWindow = anc ? new MonthlyQuantity("ANC", "EA4", year, Convert.ToInt32(month)) : new MonthlyQuantity("PNC", "EA4", year, Convert.ToInt32(month));
            AddDataWindow.Show();

        }

        private void ButtonRevision(object obj)
        {
            string revision = revisionSelect switch
            {
                0 => "BU",
                1 => "EA1",
                2 => "EA3",
                3 => "EA4",
                _ => string.Empty,
            };

            if(revision == string.Empty)
            {
                return;
            }
            

            var AddDatawindow = new RevisionQuantity("PNC", year, revision, Convert.ToInt32(month));
            AddDatawindow.Show();

        }
        #endregion
    }
}
