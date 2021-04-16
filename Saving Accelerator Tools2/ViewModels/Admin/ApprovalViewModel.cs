using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Admin
{
    public class ApprovalViewModel : INotifyProperty
    {
        #region Constructors
        public ApprovalViewModel()
        {
            LoadYear();
        }
        #endregion
        #region Private Variables
        //General Settings
        private List<Approvals2_DB> AprrovalsLoad;
        private decimal _Year = DateTime.UtcNow.Year;
        private List<string> _Plants = new List<string>() { "PLV", "ZM" };
        private int _Plants_SelectedIndex = 0;

        //To Open Settings
        private List<string> _RevOpen = new List<string>();
        private int _RevOpen_SelectedIndex = 0;
        private List<string> _MonthOpen = new List<string>();
        private int _MonthOpen_SelectedIndex = 0;

        //To Check Settings
        private List<string> _RevCheck = new List<string>();
        private int _RevCheck_SelectedIndex = -1;
        private List<string> _MonthCheck = new List<string>();
        private int _MonthCheck_SelectedIndex = -1;
        #endregion


        #region Public Variables
        //General Settings
        public decimal Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                LoadYear();
                RisePropoertyChanged();
            }
        }
        public List<string> Plants
        {
            get { return _Plants; }
            private set { }
        }
        public int Plants_SelectedIndex
        {
            get { return _Plants_SelectedIndex; }
            set
            {
                _Plants_SelectedIndex = value;
                RisePropoertyChanged();
            }
        }

        //ToOpen Settings
        public List<string> RevOpen
        {
            get { return _RevOpen; }
            set
            {
                _RevOpen = value;
                RisePropoertyChanged();
            }
        }
        public int RevOpen_SelectedIndex
        {
            get { return _RevOpen_SelectedIndex; }
            set
            {
                _RevOpen_SelectedIndex = value;
                RisePropoertyChanged();
            }
        }
        public List<string> MonthOpen
        {
            get { return _MonthOpen; }
            set
            {
                _MonthOpen = value;
                RisePropoertyChanged();
            }
        }
        public int MonthOpen_SelectedIndex
        {
            get { return _MonthOpen_SelectedIndex; }
            set
            {
                _MonthOpen_SelectedIndex = value;
                RisePropoertyChanged();
            }
        }

        //To Check Settings
        public List<string> RevCheck
        {
            get { return _RevCheck; }
            set
            {
                _RevCheck = value;
                RisePropoertyChanged();
            }
        }
        public int RevCheck_SelectedIndex
        {
            get { return _RevCheck_SelectedIndex; }
            set
            {
                _RevCheck_SelectedIndex = value;
                RisePropoertyChanged();
            }
        }
        public List<string> MonthCheck
        {
            get { return _MonthCheck; }
            set
            {
                _MonthCheck = value;
                RisePropoertyChanged();
            }
        }
        public int MonthCheck_SelectedIndex
        {
            get { return _MonthCheck_SelectedIndex; }
            set
            {
                _RevCheck_SelectedIndex = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Functions
        private void LoadYear()
        {
            _RevOpen = new List<string>();
            _RevCheck = new List<string>();
            _MonthOpen = new List<string>();
            _MonthCheck = new List<string>();

            AprrovalsLoad = Approvals2_Controller.Load_Year(_Year, _Plants[_Plants_SelectedIndex]);


            if (AprrovalsLoad.Any(record => record.Revision == "BU"))
                _RevCheck.Add("BU");
            else
                _RevOpen.Add("BU");
            if (AprrovalsLoad.Any(record => record.Revision == "EA1"))
                _RevCheck.Add("EA1");
            else
                _RevOpen.Add("EA1");
            if (AprrovalsLoad.Any(record => record.Revision == "EA2"))
                _RevCheck.Add("EA2");
            else
                _RevOpen.Add("EA2");
            if (AprrovalsLoad.Any(record => record.Revision == "EA3"))
                _RevCheck.Add("EA3");
            else
                _RevOpen.Add("EA3");

            for (int counter = 1; counter <= 12; counter++)
            {
                if (AprrovalsLoad.Any(record => record.Month == counter))
                    _MonthCheck.Add(counter.ToString());
                else
                    _MonthOpen.Add(counter.ToString());
            }

            RevOpen = _RevOpen;
            RevOpen_SelectedIndex = 0;
            RevCheck = _RevCheck;
            RevCheck_SelectedIndex = -1;
            MonthOpen = _MonthOpen;
            MonthOpen_SelectedIndex = 0;
            MonthCheck = _MonthCheck;
            MonthCheck_SelectedIndex = -1;
        }
        #endregion
    }
}
