using System;
using System.Collections.Generic;
using System.Text;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Helpers;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using System.Collections.ObjectModel;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;

namespace Saving_Accelerator_Tools2.ViewModels.Statistic
{
    public class QuantityViewModel : INotifyProperty
    {

        public QuantityViewModel()
        {
            Search = new RelayCommand<object>(LoadNewData);
            Clear = new RelayCommand<object>(ClearData);

            Search_STK = new RelayCommand<object>(Load_STK);
            Clear_STK = new RelayCommand<object>(ClearData_STK);
        }
        private ObservableCollection<QuantityObject> ListNew = new ObservableCollection<QuantityObject>();
        private ObservableCollection<STK_DB> STKList = new ObservableCollection<STK_DB>();
        private decimal yearSTK = DateTime.UtcNow.Year;
        private decimal year = DateTime.UtcNow.Year;
        private bool pnc = true;
        private bool anc = false;
        private bool ea4 = true;
        private bool ea3 = false;
        private bool ea2 = false;
        private bool ea1 = false;
        private bool bu = false;
        private bool fullHistory = false;
        private string STK_Item = string.Empty;
        private string searchText = string.Empty;

        public decimal Year
        {
            get { return year; }
            set
            {
                year = value;
                RisePropoertyChanged();
            }
        }

        public bool PNC
        {
            get { return pnc; }
            set
            {
                pnc = value;
                RisePropoertyChanged();
            }
        }
        public bool ANC
        {
            get { return anc; }
            set
            {
                anc = value;
                RisePropoertyChanged();
            }
        }
        public bool EA4
        {
            get { return ea4; }
            set
            {
                ea4 = value;
                RisePropoertyChanged();
            }
        }
        public bool EA3
        {
            get { return ea3; }
            set
            {
                ea3 = value;
                RisePropoertyChanged();
            }
        }
        public bool EA2
        {
            get { return ea2; }
            set
            {
                ea2 = value;
                RisePropoertyChanged();
            }
        }
        public bool EA1
        {
            get { return ea1; }
            set
            {
                ea1 = value;
                RisePropoertyChanged();
            }
        }
        public bool BU
        {
            get { return bu; }
            set
            {
                bu = value;
                RisePropoertyChanged();
            }
        }
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RisePropoertyChanged();
            }
        }
        public string SearchText_STK
        {
            get { return STK_Item; }
            set
            {
                STK_Item = value;
                RisePropoertyChanged();
            }
        }
        public bool FullHistory
        {
            get { return fullHistory; }
            set
            {
                fullHistory = value;
                RisePropoertyChanged();
            }
        }
        public decimal Year_STK
        {
            get { return yearSTK; }
            set
            {
                yearSTK = value;
                RisePropoertyChanged();
            }
        }


        public ObservableCollection<QuantityObject> ItemList
        {
            get { return ListNew; }
            set
            {
                ListNew = value;
                RisePropoertyChanged();
            }
        }
        public ObservableCollection<STK_DB> ListSTK
        {
            get { return STKList; }
            set
            {
                STKList = value;
                RisePropoertyChanged();
            }
        }

        public ICommand Search
        {
            get; private set;
        }
        public ICommand Clear
        {
            get; private set;
        }

        public ICommand Search_STK
        {
            get; private set;
        }
        public ICommand Clear_STK
        {
            get; private set;
        }

        private void ClearData(object obj)
        {
            ItemList = new ObservableCollection<QuantityObject>();
        }

        private void LoadNewData(object obj)
        {
            ObservableCollection<QuantityObject> NewTable = new ObservableCollection<QuantityObject>();
            List<string> DataToFind = new List<string>();
            string revision = string.Empty;

            if (searchText == string.Empty)
                return;

            if (ea3)
                revision = "EA3";
            else if (ea2)
                revision = "EA2";
            else if (ea1)
                revision = "EA1";
            else if (bu)
                revision = "BU";

            if (!ea4 && revision == string.Empty)
            {
                ItemList.Clear();
                return;
            }

            if (!pnc && !anc)
            {
                ItemList.Clear();
                return;
            }


            foreach (var datarecord in searchText.Split(Environment.NewLine))
            {
                if(!string.IsNullOrEmpty(datarecord) && !string.IsNullOrWhiteSpace(datarecord))
                {
                    DataToFind.Add(datarecord);
                }
            }

            if (pnc)
            {

                List<PNC_DB> PNClist = PNC_Controller.SearchList(DataToFind, Year, revision, 0).ToList();

                foreach (string SinglePNC in DataToFind)
                {
                    IEnumerable<PNC_DB> SinglePNCBase = PNClist.Where(b => b.Item == SinglePNC).ToList();

                    QuantityObject NewRecord = new QuantityObject()
                    {
                        Item = SinglePNC,
                    };

                    foreach (var PNCMonthly in SinglePNCBase)
                    {
                        AddQuantity(ref NewRecord, PNCMonthly.Month, PNCMonthly.Quantity);
                    }

                    NewTable.Add(NewRecord);
                }

            }
            else if (anc)
            {

                List<ANC_DB> ANClist = ANC_Controller.SearchList(DataToFind, Year, revision, 0).ToList();

                foreach (string SingleANC in DataToFind)
                {
                    IEnumerable<ANC_DB> SingleANCBase = ANClist.Where(b => b.Item == SingleANC).ToList();

                    QuantityObject NewRecord = new QuantityObject()
                    {
                        Item = SingleANC,
                    };

                    foreach (var ANCMonthly in SingleANCBase)
                    {
                        AddQuantity(ref NewRecord, ANCMonthly.Month, ANCMonthly.Quantity);
                    }

                    NewTable.Add(NewRecord);
                }

            }
            ItemList = NewTable;
        }
        private void AddQuantity(ref QuantityObject NewRecord, decimal Month, decimal Quantity)
        {
            string[] QuantityCheck = Quantity.ToString().Split(',');
            if (QuantityCheck[1] == "0000")
                Quantity = Math.Round(Quantity, 0, MidpointRounding.AwayFromZero);

            NewRecord.Sum += Quantity;
            switch (Month)
            {
                case 1:
                    NewRecord.I = Quantity;
                    break;
                case 2:
                    NewRecord.II = Quantity;
                    break;
                case 3:
                    NewRecord.III = Quantity;
                    break;
                case 4:
                    NewRecord.IV = Quantity;
                    break;
                case 5:
                    NewRecord.V = Quantity;
                    break;
                case 6:
                    NewRecord.VI = Quantity;
                    break;
                case 7:
                    NewRecord.VII = Quantity;
                    break;
                case 8:
                    NewRecord.VIII = Quantity;
                    break;
                case 9:
                    NewRecord.IX = Quantity;
                    break;
                case 10:
                    NewRecord.X = Quantity;
                    break;
                case 11:
                    NewRecord.XI = Quantity;
                    break;
                case 12:
                    NewRecord.XII = Quantity;
                    break;
            }
        }

        private void Load_STK(object obj)
        {
            ObservableCollection<STK_DB> NewList = new ObservableCollection<STK_DB>();
            if (STK_Item == string.Empty)
                return;

            if (fullHistory)
            {
                var FindElements = STK_Controller.FindItem_FullHistory(STK_Item);

                foreach (var FindElement in FindElements)
                {
                    NewList.Add(FindElement);
                }
            }
            else
            {
                var FindElement = STK_Controller.FindItem(STK_Item, yearSTK);
                if (FindElement != null)
                    NewList.Add(FindElement);
            }
            ListSTK = NewList;
        }

        private void ClearData_STK(object obj)
        {
            ListSTK = new ObservableCollection<STK_DB>();
        }
    }

    public class QuantityObject : Observable
    {
        public string Item { get; set; }
        public decimal I { get; set; }
        public decimal II { get; set; }
        public decimal III { get; set; }
        public decimal IV { get; set; }
        public decimal V { get; set; }
        public decimal VI { get; set; }
        public decimal VII { get; set; }
        public decimal VIII { get; set; }
        public decimal IX { get; set; }
        public decimal X { get; set; }
        public decimal XI { get; set; }
        public decimal XII { get; set; }
        public decimal Sum { get; set; }
    }
}
