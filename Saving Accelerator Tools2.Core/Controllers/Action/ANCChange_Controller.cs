
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Action
{
    public class ANCChange_Controller
    {
        public static bool Update(List<ANCChange_DB> ANCList, ref Action_DB Action, ref DataBaseConnetionContext context)
        {
            foreach (var ANCBase in Action.Action_ANCChange) {
                if (!ANCList.Any(b => b.ID == ANCBase.ANCChangeID))
                {
                    context.Remove(ANCBase);
                }
            }

            foreach (var ANC in ANCList) {
                if (ANC.ID == 0) {
                    var newRecord = new Action_ANCChage_InterTable() {
                        ActionID = Action.ID,
                        ANCChange = ANC,
                    };
                    Action.Action_ANCChange.Add(newRecord);
                }
                else {
                    var Base = context.ANCChange.Where(c => c.ID == ANC.ID).FirstOrDefault();
                    Base.OldANC = ANC.NewANC;
                    Base.NewANC = ANC.NewANC;
                    Base.OldANCQuantity = ANC.OldANCQuantity;
                    Base.NewANCQuantity = ANC.NewANCQuantity;
                    Base.OldSTK = ANC.OldSTK;
                    Base.NewSTK = ANC.NewSTK;
                    Base.Delta = ANC.Delta;
                    Base.UserEstymacja = ANC.UserEstymacja;
                    Base.Percent = ANC.Percent;
                    Base.Estymacja = ANC.Estymacja;
                    Base.NextANC1 = ANC.NextANC1;
                    Base.NextANC2 = ANC.NextANC2;
                    context.Update(Base);
                }
            }
            context.SaveChanges();

            return true;
        }

        public static void Update (List<ANCChange_DB> UpdatList)
        {
            var context = new DataBaseConnetionContext();

            foreach(var UpdateItem in UpdatList) {
                context.ANCChange.Update(UpdateItem);
            }
            context.SaveChanges();
        }

    }
}
