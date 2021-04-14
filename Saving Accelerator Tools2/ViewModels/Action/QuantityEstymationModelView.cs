using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class QuantityEstymationModelView : INotifyProperty
    {
        #region Constructors
        public QuantityEstymationModelView()
        {
            Mediator.Mediator.Register("Set_QuantityEstymation", Set_Value);
            Mediator.Mediator.Register("Get_QuantityEstimation", Get_Value);
        }
        ~ QuantityEstymationModelView()
        {
            Mediator.Mediator.Unregister("Set_QuantityEstymation", Set_Value);
            Mediator.Mediator.Unregister("Get_QuantityEstimation", Get_Value);
        }
        #endregion

        #region Private Variables
        private decimal _Value = 100;
        #endregion

        #region Public Variables
        public decimal Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Mediator Function
        private void Set_Value(object NewValue)
        {
            Value = (decimal)NewValue;
        }
        private void Get_Value(object obj)
        {
            Mediator.Mediator.NotifyColleagues("Q_Estymation_Save", Value);
        }
        #endregion

    }
}
