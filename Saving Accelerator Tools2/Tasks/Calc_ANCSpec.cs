using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks.Calculation_Function;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class Calc_ANCSpec
    {
        #region Variables
        private bool _ECCC;
        private decimal _ECCCSek;
        private decimal _delta;
        private decimal _deltaEstymation;
        private List<int> _platforms = new List<int>();
        private List<PlusMinusModel> _plusMinus = new List<PlusMinusModel>();
        #endregion

        #region Functions
        public IEnumerable<ItemModel> ItemList()
        {
            List<ItemModel> itemModel = new List<ItemModel>();

            ECCCLoad();
            DeltaLoad();
            Mediator.Mediator.NotifyColleagues("ANCSpecial_Platform", _platforms);
            Mediator.Mediator.NotifyColleagues("ANCSpecial_PlusMinus", _plusMinus);

            var ClearList = _plusMinus.Where(b => b.Active == false).ToList();
            if (ClearList.Count > 0)
            {
                foreach (var ClearItem in ClearList)
                {
                    _plusMinus.Remove(ClearItem);
                }
            }

            if (_platforms.Count > 0)
            {
                foreach (var PlatformItem in _platforms)
                {
                    var NewItem = new ItemModel()
                    {
                        Item = PlatformItem switch
                        {
                            1 => "ALL",
                            2 => "D45",
                            3 => "DMD",
                            4 => "DMD_FS",
                            5 => "DMD_FI",
                            6 => "DMD_BI",
                            7 => "DMD_FSBU",
                            8 => "D45_FS",
                            9 => "D45_FI",
                            10 => "D45_BI",
                            11 => "D45_FSBU",
                            _ => "",
                        },
                        Plus = true,
                        ECCC = _ECCC ? _ECCCSek : 0,
                        Savings = _delta,
                        Savings_Estymation = _deltaEstymation,
                    };
                    itemModel.Add(NewItem);
                }
            }

            if (_plusMinus.Count > 0)
            {
                foreach (var PlusMinusItem in _plusMinus)
                {
                    var newItem = new ItemModel()
                    {
                        Item = PlusMinusItem.Item,
                        Plus = PlusMinusItem.Plus,
                        ECCC = _ECCC ? _ECCCSek : 0,
                        Savings = _delta,
                        Savings_Estymation = _deltaEstymation,
                    };
                    itemModel.Add(newItem);
                }
            }

            return itemModel;
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
