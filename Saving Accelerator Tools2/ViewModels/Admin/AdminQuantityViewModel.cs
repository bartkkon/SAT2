using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Admin
{
    public class AdminQuantityViewModel : INotifyProperty
    {
        private bool pnc = true;
        private bool anc = false;
        private decimal year = DateTime.UtcNow.Month is 1 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
        private decimal month = DateTime.UtcNow.Month is 1 ? 12 : DateTime.UtcNow.Month -1;
        private int revisionSelect = 0; // 0 = BU, 1 = EA1, 2 = EA2, 3 = EA4



        public bool PNC_CheckBox
        {
            get
            {
                return pnc;
            }
            set
            {
                pnc = value;
                RisePropoertyChanged();
            }
        }

        public bool ANC_CheckBox
        {
            get
            {
                return anc;
            }
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



    }
}
