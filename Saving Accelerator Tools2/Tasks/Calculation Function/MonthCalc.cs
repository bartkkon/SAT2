using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks.Calculation_Function
{
    public class MonthCalc
    {
        #region Returned Variables
        private int StartMonth = 0;
        private int EndMonth = 0;
        #endregion

        #region Private Variables
        private bool _CarryOver;
        private readonly decimal _ActionYear;
        private readonly decimal _ActionMonthStart;
        private readonly Approvals_DB _Calculation;
        #endregion

        #region Constructors
        public MonthCalc(decimal ActionYear, decimal ActionMonthStart, Approvals_DB CalcRequest)
        {
            _ActionYear = ActionYear;
            _ActionMonthStart = ActionMonthStart;
            _Calculation = CalcRequest;

            CarryOverCheck();
            if(_Calculation.Month != null)
            {
                MonthCalculation();
            }
            else if(_Calculation.Revision != null)
            {
                RevisionCalculation();
            }
        }
        #endregion

        #region Returned Functions
        public int Start()
        {
            return StartMonth;
        }
        public int Finish()
        {
            return EndMonth;
        }
        #endregion

        #region Internal Functions
        private void CarryOverCheck()
        {
            if (_Calculation.Year == _ActionYear)
                _CarryOver = false;
            else if (_Calculation.Year == _ActionYear + 1)
                _CarryOver = true;
        }

        private void MonthCalculation()
        {
            if (!_CarryOver)
            {
                if ((int)_ActionMonthStart <= (int)_Calculation.Month)
                {
                    StartMonth = (int)_Calculation.Month;
                    EndMonth = (int)_Calculation.Month;
                }
            }
            else
            {
                if ((int)_ActionMonthStart > (int)_Calculation.Month)
                {
                    StartMonth = (int)_Calculation.Month;
                    EndMonth = (int)_Calculation.Month;
                }
            }
        }

        private void RevisionCalculation()
        {
            EndMonth = 12;

            StartMonth = _Calculation.Revision switch
            {
                "BU" => 1,
                "EA1" => 3,
                "EA2" => 6,
                "EA3" => 9,
                _ => 12,
            };

            if (!_CarryOver)
            {
                if ((int)_ActionMonthStart > StartMonth)
                {
                    StartMonth = (int)_ActionMonthStart;
                }
            }
            else
            {
                if ((int)_ActionMonthStart < EndMonth)
                {
                    EndMonth = (int)_ActionMonthStart;
                }
                if ((int)_ActionMonthStart == 1)
                {
                    EndMonth = 0;
                }
            }

        }
        #endregion
    }
}
