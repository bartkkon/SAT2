using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData.SubFunction
{
    public class StandardCostReport
    {
        public ICollection<StandardCost> Convert (string[] oryginalData, Factories factory)
        {
            List<StandardCost> NewData = new();

            foreach(string record in oryginalData)
            {
                StandardCost newRecord = new();
                DateTime dateTime;
                string recordwork;

                recordwork = record.Remove(0, 2);
                newRecord.Item = recordwork.Remove(9);
                recordwork = recordwork.Remove(0, 11);

                dateTime = new DateTime(year: int.Parse("20" + recordwork.Remove(2)),
                                        month: int.Parse(recordwork.Remove(0, 2).Remove(2)),
                                        day: int.Parse(recordwork.Remove(0, 4).Remove(2)));
                newRecord.Date = dateTime;
                newRecord.Year = dateTime.Year;

                recordwork = recordwork.Remove(0, 6);
                newRecord.STK3 = Math.Round(decimal.Parse(recordwork.Remove(9)) / 10000, 4, MidpointRounding.AwayFromZero);

                recordwork = recordwork.Remove(0, 154);
                newRecord.Description = recordwork.Remove(30).Trim();
                newRecord.IDCO = recordwork.Remove(0, 31).Remove(4);

                newRecord.Factory = factory;

                if(newRecord.STK3 != 0)
                {
                    NewData.Add(newRecord);
                }
            }

            return NewData;
        }
    }
}
