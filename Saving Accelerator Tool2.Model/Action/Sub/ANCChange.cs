using Saving_Accelerator_Tools2.Model.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Action.Sub
{
    public class ANCChange : Base
    {
        private int id;
        private string oldANC;
        private decimal oldANC_Q;
        private decimal oldANC_STD;
        private string newANC;
        private decimal newANC_Q;
        private decimal newANC_STD;

        private decimal delta;

        private decimal estymation;
        private decimal percent = 100;
        private decimal finalCalc;

        private Currency currency;
        private string nextANC1;
        private string nextANC2;

        public string NextANC2 { get => nextANC2; set { nextANC2 = value; OnPropertyChange(); } }


        public string NextANC1 { get => nextANC1; set { nextANC1 = value; OnPropertyChange(); } }


        public Currency Currency { get => currency; set { currency = value; OnPropertyChange(); } }

        public decimal FinalCalc { get => finalCalc; set { finalCalc = value; OnPropertyChange(); } }


        public decimal Percent
        {
            get => percent;
            set
            {
                percent = value;
                CalculationEstimation();
                OnPropertyChange();
            }
        }
        public decimal Estymation
        {
            get => estymation;
            set
            {
                estymation = value;
                CalculationEstimation();
                OnPropertyChange();
            }
        }


        public decimal Delta
        {
            get => delta;
            set
            {
                delta = value;
                CalculationEstimation();
                OnPropertyChange();
            }
        }

        public decimal NewANC_STD
        {
            get => newANC_STD;
            set
            {
                newANC_STD = value;
                CalculationDelta();
                OnPropertyChange();
            }
        }
        public decimal NewANC_Q { get => newANC_Q; set { newANC_Q = value; OnPropertyChange(); } }
        public string NewANC
        {
            get => newANC;
            set
            {
                newANC = value;
                NewANC_Q = string.IsNullOrEmpty(newANC) ? 0 : newANC_Q == 0 ? 1 : newANC_Q;
                OnPropertyChange();
            }
        }

        public decimal OldANC_STD
        {
            get => oldANC_STD;
            set
            {
                oldANC_STD = value;
                CalculationDelta();
                OnPropertyChange();
            }
        }
        public decimal OldANC_Q { get => oldANC_Q; set { oldANC_Q = value; OnPropertyChange(); } }
        public string OldANC
        {
            get => oldANC;
            set
            {
                oldANC = value;
                OldANC_Q = string.IsNullOrEmpty(oldANC) ? 0 : oldANC_Q == 0 ? 1 : oldANC_Q;
                OnPropertyChange();
            }
        }

        public int ID { get => id; set { id = value; OnPropertyChange(); } }


        private void CalculationDelta()
        {
            Delta = oldANC_STD - newANC_STD;
        }

        private void CalculationEstimation()
        {
            FinalCalc = estymation != 0 ? Math.Round(estymation * (percent / 100), 4, MidpointRounding.AwayFromZero) : Math.Round(delta * (percent / 100), 4, MidpointRounding.AwayFromZero);
        }

    }
}
