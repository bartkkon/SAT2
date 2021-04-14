using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks.Calculation_Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class Calc_PNCSpec
    {
        #region Variables
        private List<PNCSpecialModel> _PNCList = new List<PNCSpecialModel>();
        private bool _eCCCSpec;
        #endregion

        #region Function
        public IEnumerable<ItemModel> ItemList()
        {
            List<ItemModel> itemModels = new List<ItemModel>();

            Mediator.Mediator.NotifyColleagues("PNCSpecial_Save_Object", _PNCList);
            ECCCLoad();

            foreach (var PNC in _PNCList)
            {
                var NewItem = new ItemModel()
                {
                    Item = PNC.PNC,
                    Plus = true,
                    Savings = PNC.Delta,
                    Savings_Estymation = PNC.Delta,
                    ECCC = _eCCCSpec ? PNC.ECCC : 0,
                };
                itemModels.Add(NewItem);
            }

            return itemModels;
        }
        private void ECCCLoad()
        {
            var EcccModel = new ECCCModel();
            Mediator.Mediator.NotifyColleagues("Get_ECCCModel", EcccModel);
            _eCCCSpec = EcccModel.ECCCSpecial;
        }
        #endregion
    }
}
