using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Models.Action
{
    public class TargetsModels : INotifyProperty
    {
        private string _Header;
        private decimal _January;
        private decimal _February;
        private decimal _March;
        private decimal _April;
        private decimal _May;
        private decimal _June;
        private decimal _July;
        private decimal _August;
        private decimal _September;
        private decimal _October;
        private decimal _November;
        private decimal _December;
        private decimal _Sum;

        public string Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                RisePropoertyChanged();
            }
        }
        public decimal January
        {
            get { return _January; }
            set
            {
                _January = value;
                RisePropoertyChanged();
            }
        }
        public decimal February
        {
            get { return _February; }
            set
            {
                _February = value;
                RisePropoertyChanged();
            }
        }
        public decimal March
        {
            get { return _March; }
            set
            {
                _March = value;
                RisePropoertyChanged();
            }
        }
        public decimal April
        {
            get { return _April; }
            set
            {
                _April = value;
                RisePropoertyChanged();
            }
        }
        public decimal May
        {
            get { return _May; }
            set
            {
                _May = value;
                RisePropoertyChanged();
            }
        }
        public decimal June
        {
            get { return _June; }
            set
            {
                _June = value;
                RisePropoertyChanged();
            }
        }
        public decimal July
        {
            get { return _July; }
            set
            {
                _July = value;
                RisePropoertyChanged();
            }
        }
        public decimal August
        {
            get { return _August; }
            set
            {
                _August = value;
                RisePropoertyChanged();
            }
        }
        public decimal September
        {
            get { return _September; }
            set
            {
                _September = value;
                RisePropoertyChanged();
            }
        }
        public decimal October
        {
            get { return _October; }
            set
            {
                _October = value;
                RisePropoertyChanged();
            }
        }
        public decimal November
        {
            get { return _November; }
            set
            {
                _November = value;
                RisePropoertyChanged();
            }
        }
        public decimal December
        {
            get { return _December; }
            set
            {
                _December = value;
                RisePropoertyChanged();
            }
        }
        public decimal Sum
        {
            get { return _Sum; }
            set
            {
                _Sum = value;
                RisePropoertyChanged();
            }
        }
    }
}
