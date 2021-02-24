using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Admin
{
    public class PlatformCalculationViewModel : INotifyProperty
    {
        #region Constructiors
        public PlatformCalculationViewModel()
        {
            AddVale = new RelayCommand<object>(Save);
        }
        #endregion

        #region Private Variables
        private bool _ActiveSearch = true;
        private string _Platform;
        private string _Installation;
        private bool _Active;
        private List<PlatformCalc_DB> _PlatformList = Load();
        private PlatformCalc_DB _SelectedItem;
        private int _SelectedIndex = -1;
        #endregion

        #region Public Variables
        public bool ActioveSearch
        {
            get { return _ActiveSearch; }
            set
            {
                _ActiveSearch = value;
                RisePropoertyChanged();
            }
        }
        public string Platform
        {
            get { return _Platform; }
            set
            {
                _Platform = value;
                RisePropoertyChanged();
            }
        }
        public string Installation
        {
            get { return _Installation; }
            set
            {
                _Installation = value;
                RisePropoertyChanged();
            }
        }
        public bool Active
        {
            get { return _Active; }
            set
            {
                _Active = value;
                RisePropoertyChanged();
            }
        }
        public List<PlatformCalc_DB> PlatformList
        {
            get { return _PlatformList; }
            set
            {
                _PlatformList = value;
                RisePropoertyChanged();
            }
        }
        public PlatformCalc_DB SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                LoadPlatform();
                RisePropoertyChanged();
            }
        }
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand AddVale
        {
            get; private set;
        }
        private void Save(object obj)
        {
            if (SelectedIndex == -1)
            {
                PlatformCalc_Controller.Add(_Platform, _Installation, _Active);
                PlatformList = Load_NonStatic();
            }
            else
            {
                PlatformCalc_Controller.Update(_SelectedItem.ID, _Platform, _Installation, _Active);
                PlatformList = Load_NonStatic();
            }
        }
        #endregion

        #region Functions
        private static List<PlatformCalc_DB> Load()
        {
            return PlatformCalc_Controller.LoadActive(true);
        }
        private List<PlatformCalc_DB> Load_NonStatic()
        {
            return PlatformCalc_Controller.LoadActive(_ActiveSearch);
        }
        private void LoadPlatform()
        {
            Platform = SelectedItem.Platform;
            Installation = SelectedItem.Installation;
            Active = SelectedItem.Active;
        }
        #endregion

    }
}
