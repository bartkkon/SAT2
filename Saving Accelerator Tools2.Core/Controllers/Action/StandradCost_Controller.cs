using Saving_Accelerator_Tools2.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Action
{
    public class StandradCost_Controller
    {
        public static List<string> UpdateAction (List<string> ANCList, decimal Year)
        {
            var context = new DataBaseConnetionContext();
            var ListSTK = new List<string>();

            foreach (var ANC in ANCList) {
                string value = string.Empty;
                if (ANC != string.Empty) {
                    var STK = context.STK.Where(u => u.ANC == ANC && u.Year == Year).FirstOrDefault();
                    if(STK != null) {
                        value = STK.STD.ToString();
                    }
                }
                ListSTK.Add(value);
            }

            return ListSTK;
        }
    }
}
