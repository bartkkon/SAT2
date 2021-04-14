using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks.Calculation_Function;
using Saving_Accelerator_Tools2.ViewModels.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class Calc_PNC
    {
        #region Variables
        private List<PNCListData> _pNCList = new List<PNCListData>();
        private decimal _delta;
        private decimal _deltaEstymation;
        private bool _ECCC;
        private decimal _ECCCSek;
        #endregion

        #region Public Functions
        public IEnumerable<ItemModel> ItemList ()
        {
            List<ItemModel> itemModels = new List<ItemModel>();

            DeltaLoad();
            Mediator.Mediator.NotifyColleagues("PNC_Models", _pNCList);
            ECCCLoad();
            
            foreach(var PNC in _pNCList)
            {
                var newItem = new ItemModel()
                {
                    Item = PNC.PNC,
                    Plus = true,
                    Savings = _delta,
                    Savings_Estymation = _deltaEstymation,
                    ECCC = _ECCC ? _ECCCSek : 0,
                };
                itemModels.Add(newItem);
            }

            return itemModels;
        }
        private void ECCCLoad()
        {
            var ECCCModel = new ECCCModel();
            Mediator.Mediator.NotifyColleagues("Get_ECCCModel", ECCCModel);
            _ECCC = ECCCModel.ECCC;
            _ECCCSek = ECCCModel.ECCC_Value;
        }
        private void DeltaLoad()
        {
            var Delta = new List<decimal>();
            Mediator.Mediator.NotifyColleagues("Get_DetltaSum", Delta);
            _delta = Delta[0];
            _deltaEstymation = Delta[1];
        }
        #endregion
    }
}
