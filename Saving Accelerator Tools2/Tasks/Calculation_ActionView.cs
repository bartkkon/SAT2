using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks.Calculation_Function;
using Saving_Accelerator_Tools2.Tasks.Calculation_Transfer_Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class Calculation_ActionView
    {
        #region Constructios
        public Calculation_ActionView()
        {
            Calculation();
        }
        ~Calculation_ActionView()
        {

        }
        #endregion

        #region Variables
        private List<Approvals2_DB> Revisions = new List<Approvals2_DB>();
        private List<Approvals2_DB> Months = new List<Approvals2_DB>();
        private List<Approvals_DB> Devisions = new List<Approvals_DB>();

        private List<CalculationModels> Calculations = new List<CalculationModels>();
        private decimal _Year;
        private decimal _Month;
        private bool _Active;
        private string _Devision;
        private Estimation_Percent_Transfer _Percent = new Estimation_Percent_Transfer();
        private CalculationBy_TransferClass _Group = new CalculationBy_TransferClass();
        private bool _Estymation;

        #endregion

        #region Function
        private void Calculation()
        {
            GeneralInformation();

            var LoadApprovals = new Approvals(_Devision, "PLV", _Year);

            EstimationAction();

            Revisions = LoadApprovals.RevisionOpen();
            Months = LoadApprovals.MonthOpen();

            if (_Active && (Revisions.Count() > 0 || Months.Count() > 0))
            {
                Mediator.Mediator.NotifyColleagues("Tables_Model", Calculations);
                Mediator.Mediator.NotifyColleagues("Get_Class", _Group);
                Mediator.Mediator.NotifyColleagues("QuantityPercent_Model", _Percent);

                List<ItemModel> ItemList = _Group.Grup switch
                {
                    1 => new Calc_ANC().ItemList(_Estymation).ToList(),
                    2 => new Calc_ANCSpec().ItemList().ToList(),
                    3 => new Calc_PNC().ItemList().ToList(),
                    4 => new Calc_PNCSpec().ItemList().ToList(),
                    _ => new List<ItemModel>(),
                };

                if (ItemList.Count == 0)
                {
                    return;
                }

                var Calculate = new Calculation(ItemList, true, Calculations, _Year);
                if (Revisions.Count > 0)
                {
                    foreach (var Revision in Revisions)
                    {
                        var MonthCalc = new MonthCalc(_Year, _Month, Revision);
                        Calculate.Range(Revision, MonthCalc.Start(), MonthCalc.Finish(), _Percent.Percent);
                    }
                }
                if (Months.Count > 0)
                {
                    foreach (var Month in Months)
                    {
                        Month.Revision = "EA4";
                        var MonthCalc = new MonthCalc(_Year, _Month, Month);
                        Calculate.Range(Month, MonthCalc.Start(), MonthCalc.Finish(), 100);
                    }
                }

                Calculations = Calculate.ReturnCalculation();

                Mediator.Mediator.NotifyColleagues("Tabels_LoadData", Calculations);
            }
        }

        private void GeneralInformation()
        {
            var GI_Transfer = new GeneralInformation_TransferClass();

            Mediator.Mediator.NotifyColleagues("Get_Calc", GI_Transfer);
            _Year = GI_Transfer.Year;
            _Month = GI_Transfer.Month;
            _Active = GI_Transfer.Active;
            _Devision = GI_Transfer.Devision;
        }
        private void EstimationAction()
        {
            if (_Year > DateTime.UtcNow.Year)
            {
                _Estymation = true;
                return;
            }
            else if (_Year == DateTime.UtcNow.Year)
            {
                if (_Month > DateTime.UtcNow.Month)
                {
                    _Estymation = true;
                }
                else
                {
                    _Estymation = false;
                }
                return;
            }
            _Estymation = false;
        }
        #endregion

        #region Mediators Functions

        #endregion
    }
}
