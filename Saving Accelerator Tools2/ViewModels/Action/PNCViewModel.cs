using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class PNCViewModel : INotifyProperty
    {
        #region Constructors
        public PNCViewModel()
        {
            Mediator.Mediator.Register("PNC_Visibilit", SetVisibility);
            Mediator.Mediator.Register("Set_PNC_Data", SetNewData);
            Mediator.Mediator.Register("Get_PNC_Data_Updated", GetData_Updated);
            Mediator.Mediator.Register("Get_PNC_Data", GetData);
            Mediator.Mediator.Register("PNC_Models", GetModels);
        }
        ~PNCViewModel()
        {
            Mediator.Mediator.Unregister("PNC_Visibilit", SetVisibility);
            Mediator.Mediator.Unregister("Set_PNC_Data", SetNewData);
            Mediator.Mediator.Unregister("Get_PNC_Data_Updated", GetData_Updated);
            Mediator.Mediator.Unregister("Get_PNC_Data", GetData);
            Mediator.Mediator.Unregister("PNC_Models", GetModels);
        }
        #endregion

        #region Private Varianbles
        private Visibility UserControlVisible = Visibility.Hidden;
        private ObservableCollection<PNCListData> _PNCList = new ObservableCollection<PNCListData>();
        #endregion

        #region Public Variables
        public Visibility PNCVisible
        {
            get { return UserControlVisible; }
            set
            {
                UserControlVisible = value;
                PNCList = new ObservableCollection<PNCListData>();
                RisePropoertyChanged();
            }
        }
        public ObservableCollection<PNCListData> PNCList
        {
            get { return _PNCList; }
            set
            {
                _PNCList = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Mediator Functions
        private void SetVisibility(object Visibile)
        {
            PNCVisible = (Visibility)Visibile;
        }
        private void SetNewData(object NewList)
        {
            PNCList = new ObservableCollection<PNCListData>(NewList as List<PNCListData>);
        }
        private void GetData_Updated(object obj)
        {
            Mediator.Mediator.NotifyColleagues("AddingPNCList", PNCList);
        }
        private void GetData(object obj)
        {
            Mediator.Mediator.NotifyColleagues("Set_PNCList_Save", PNCList);
        }
        private void GetModels(object PNCData)
        {
            foreach(var PNCItem in _PNCList)
            {
                (PNCData as List<PNCListData>).Add(PNCItem);
            }
        }
        #endregion

    }

    public class PNCListData
    {
        public int ID { get; set; }
        public string PNC { get; set; }
    }
}
