using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class TargetsViewModel : INotifyProperty
    {
        #region Constructors
        public TargetsViewModel()
        {
            Mediator.Mediator.Register("Tabels_LoadData", LoadData);
            Mediator.Mediator.Register("Tables_SaveData", SaveData);
            Mediator.Mediator.Register("Tables_Model", SentModels);
        }
        ~TargetsViewModel()
        {
            Mediator.Mediator.Unregister("Tabels_LoadData", LoadData);
            Mediator.Mediator.Unregister("Tables_SaveData", SaveData);
            Mediator.Mediator.Unregister("Tables_Model", SentModels);
        }
        #endregion

        #region Private Variables
        private List<TargetsModels> _SavingTables = PrepareEmptyTables();
        private List<TargetsModels> _QuantityTables = PrepareEmptyTables();
        private List<TargetsModels> _ECCCTables = PrepareEmptyTables();
        private List<CalculationModels> Data = new List<CalculationModels>();
        private bool _CarryOver = false;
        #endregion

        #region Public Variables
        public List<TargetsModels> SavingTables
        {
            get { return _SavingTables; }
            set
            {
                _SavingTables = value;
                RisePropoertyChanged();
            }
        }
        public List<TargetsModels> QuantityTables
        {
            get { return _QuantityTables; }
            set
            {
                _QuantityTables = value;
                RisePropoertyChanged();
            }
        }
        public List<TargetsModels> ECCCTables
        {
            get { return _ECCCTables; }
            set
            {
                _ECCCTables = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Functions
        private static List<TargetsModels> PrepareEmptyTables()
        {
            var NewList = new List<TargetsModels>
            {
                new TargetsModels() { Header = "EA4"},
                new TargetsModels() { Header = "Actual"},
                new TargetsModels() { Header = "EA3"},
                new TargetsModels() { Header = "EA2"},
                new TargetsModels() { Header = "EA1"},
                new TargetsModels() { Header = "BU"},
            };

            return NewList;
        }
        #endregion

        #region Mediators Functions
        private void LoadData(object NewData)
        {
            Data = NewData as List<CalculationModels>;

            _SavingTables = PrepareEmptyTables();
            _QuantityTables = PrepareEmptyTables();
            _ECCCTables = PrepareEmptyTables();

            AddValueToTable(Data.Where(d => d.Revision == "BU" && d.CarryOver == _CarryOver).ToList(), "BU");
            AddValueToTable(Data.Where(d => d.Revision == "EA1" && d.CarryOver == _CarryOver).ToList(), "EA1");
            AddValueToTable(Data.Where(d => d.Revision == "EA2" && d.CarryOver == _CarryOver).ToList(), "EA2");
            AddValueToTable(Data.Where(d => d.Revision == "EA3" && d.CarryOver == _CarryOver).ToList(), "EA3");
            AddValueToTable(Data.Where(d => d.Revision == "ACT" && d.CarryOver == _CarryOver).ToList(), "Actual");
            AddValueToTable(Data.Where(d => d.Revision == "EA4" && d.CarryOver == _CarryOver).ToList(), "EA4");

            SavingTables = SumTable(_SavingTables);
            QuantityTables = SumTable(_QuantityTables);
            ECCCTables = SumTable(_ECCCTables);
        }
        private void SaveData(object NewData)
        {
            Mediator.Mediator.NotifyColleagues("Tables_Save", Data);
        }
        private void SentModels(object Tables)
        {
            foreach(var DataItem in Data)
            {
                (Tables as List<CalculationModels>).Add(DataItem);
            }
        }
        #endregion

        #region Mediators SubFunctions
        private void AddValueToTable(List<CalculationModels> List, string Revision)
        {
            var Savings = _SavingTables.Where(b => b.Header == Revision).FirstOrDefault();
            var Quantity = _QuantityTables.Where(b => b.Header == Revision).FirstOrDefault();
            var ECCC = _ECCCTables.Where(b => b.Header == Revision).FirstOrDefault();

            if (List == null)
                return;

            if (Savings == null || Quantity == null || ECCC == null)
                return;

            foreach (var ListRecord in List)
            {
                switch (ListRecord.Month)
                {
                    case 1:
                        Savings.January += ListRecord.Savings;
                        Quantity.January += ListRecord.Quantity;
                        ECCC.January += ListRecord.ECCC;
                        break;
                    case 2:
                        Savings.February += ListRecord.Savings;
                        Quantity.February += ListRecord.Quantity;
                        ECCC.February += ListRecord.ECCC;
                        break;
                    case 3:
                        Savings.March += ListRecord.Savings;
                        Quantity.March += ListRecord.Quantity;
                        ECCC.March += ListRecord.ECCC;
                        break;
                    case 4:
                        Savings.April += ListRecord.Savings;
                        Quantity.April += ListRecord.Quantity;
                        ECCC.April += ListRecord.ECCC;
                        break;
                    case 5:
                        Savings.May += ListRecord.Savings;
                        Quantity.May += ListRecord.Quantity;
                        ECCC.May += ListRecord.ECCC;
                        break;
                    case 6:
                        Savings.June += ListRecord.Savings;
                        Quantity.June += ListRecord.Quantity;
                        ECCC.June += ListRecord.ECCC;
                        break;
                    case 7:
                        Savings.July += ListRecord.Savings;
                        Quantity.July += ListRecord.Quantity;
                        ECCC.July += ListRecord.ECCC;
                        break;
                    case 8:
                        Savings.August += ListRecord.Savings;
                        Quantity.August += ListRecord.Quantity;
                        ECCC.August += ListRecord.ECCC;
                        break;
                    case 9:
                        Savings.September += ListRecord.Savings;
                        Quantity.September += ListRecord.Quantity;
                        ECCC.September += ListRecord.ECCC;
                        break;
                    case 10:
                        Savings.October += ListRecord.Savings;
                        Quantity.October += ListRecord.Quantity;
                        ECCC.October += ListRecord.ECCC;
                        break;
                    case 11:
                        Savings.November += ListRecord.Savings;
                        Quantity.November += ListRecord.Quantity;
                        ECCC.November += ListRecord.ECCC;
                        break;
                    case 12:
                        Savings.December += ListRecord.Savings;
                        Quantity.December += ListRecord.Quantity;
                        ECCC.December += ListRecord.ECCC;
                        break;
                }

            }
        }
        private List<TargetsModels> SumTable(List<TargetsModels> Table)
        {
            var BURow = Table.Where(b => b.Header == "BU").FirstOrDefault();
            var EA1Row = Table.Where(b => b.Header == "EA1").FirstOrDefault();
            var EA2Row = Table.Where(b => b.Header == "EA2").FirstOrDefault();
            var EA3Row = Table.Where(b => b.Header == "EA3").FirstOrDefault();
            var EA4Row = Table.Where(b => b.Header == "EA4").FirstOrDefault();
            var ACTRow = Table.Where(b => b.Header == "Actual").FirstOrDefault();
            BURow.Sum = BURow.January + BURow.February + BURow.March + BURow.April + BURow.May + BURow.June + BURow.July + BURow.August + BURow.September + BURow.October + BURow.November + BURow.December;
            EA1Row.Sum = ACTRow.January + ACTRow.February + EA1Row.March + EA1Row.April + EA1Row.May + EA1Row.June + EA1Row.July + EA1Row.August + EA1Row.September + EA1Row.October + EA1Row.November + EA1Row.December;
            EA2Row.Sum = ACTRow.January + ACTRow.February + ACTRow.March + ACTRow.April + ACTRow.May + EA2Row.June + EA2Row.July + EA2Row.August + EA2Row.September + EA2Row.October + EA2Row.November + EA2Row.December;
            EA3Row.Sum = ACTRow.January + ACTRow.February + ACTRow.March + ACTRow.April + ACTRow.May + ACTRow.June + ACTRow.July + ACTRow.August + EA3Row.September + EA3Row.October + EA3Row.November + EA3Row.December;
            EA4Row.Sum = EA4Row.January + EA4Row.February + EA4Row.March + EA4Row.April + EA4Row.May + EA4Row.June + EA4Row.July + EA4Row.August + EA4Row.September + EA4Row.October + EA4Row.November + EA4Row.December;
            ACTRow.Sum = ACTRow.January + ACTRow.February + ACTRow.March + ACTRow.April + ACTRow.May + ACTRow.June + ACTRow.July + ACTRow.August + ACTRow.September + ACTRow.October + ACTRow.November + ACTRow.December;
            return Table;
        }
        #endregion
    }
}
