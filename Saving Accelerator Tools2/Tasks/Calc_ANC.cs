using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks.Calculation_Function;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class Calc_ANC
    {
        #region Variables
        private bool _ECCC;
        private decimal _ECCCSek;
        private List<ANCChangeModel> ANCList = new List<ANCChangeModel>();
        #endregion

        #region Functions
        public IEnumerable<ItemModel> ItemList(bool Estymation)
        {
            List<ItemModel> itemModels = new List<ItemModel>();

            Mediator.Mediator.NotifyColleagues("Get_ANCChange_Object", ANCList);
            ECCCLoad();

            foreach(var ANC in ANCList)
            {
                var NewItem = new ItemModel()
                {
                    Item = Estymation ? ANC.OldANC : ANC.NewANC,
                    Plus = true,
                    Savings = ANC.Delta,
                    Savings_Estymation = ANC.UserEstymacja,
                    ECCC = _ECCC ? _ECCCSek : 0,
                };
                itemModels.Add(NewItem);
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
        #endregion
    }
}
