using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Saving_Accelerator_Tools2.Core.Update
{
    public class STK_DataBaseUpdate
    {
        public STK_DataBaseUpdate()
        { }

        public int[] StandardUpdate()
        {
            string Link = PrepareLinktoSTK();
            if (Link == null) {
                return new int[] { -1, -1, -1 };
            }

            var Records = UpdateBase(Link);

            return Records;
        }

        private int[] UpdateBase(string link)
        {
            string[] STKFileUpdate = File.ReadAllLines(link);
            List<STK_DB> STK_Base;
            var context = new DataBaseConnetionContext();
            int CheckRecords = 0;
            int UpdatedRecords = 0;
            int AddRecords = 0;
            decimal UpdateYear;

            if(STKFileUpdate.Count() == 0)
                return new int[] { CheckRecords, UpdatedRecords, AddRecords };

            var helpLine = STKFileUpdate[0].Remove(0, 13);
            UpdateYear = decimal.Parse("20" + helpLine.Remove(2));

            STK_Base = STK_Controller.Load_Year(UpdateYear).ToList();


            foreach (string UpdateLine in STKFileUpdate) {
                CheckRecords++;

                var Line = UpdateLine.Remove(0, 2);
                var ANC = Line.Remove(9);
                Line = Line.Remove(0, 11);
                var Year = int.Parse("20" + Line.Remove(2));
                Line = Line.Remove(0, 2);
                var Month = int.Parse(Line.Remove(2));
                Line = Line.Remove(0, 2);
                var Day = int.Parse(Line.Remove(2));
                Line = Line.Remove(0, 2);
                var STK = Math.Round(decimal.Parse(Line.Remove(9)) / 10000,4,MidpointRounding.AwayFromZero);
                Line = Line.Remove(0, 154);
                var Description = Line.Remove(30).Trim();
                Line = Line.Remove(0, 31);
                var IDCO = Line.Remove(4);

                var RecordDate = new DateTime(Year, Month, Day);

                var DataBaseRecord = STK_Base.Where(u => u.ANC == ANC).FirstOrDefault();
                if (DataBaseRecord != null) {
                    var IfUpdated = false;
                    if (DataBaseRecord.STD != STK) {
                        DataBaseRecord.STD = STK;
                        DataBaseRecord.Date = RecordDate;
                        IfUpdated = true;
                    }

                    if (DataBaseRecord.IDCO != IDCO) {
                        DataBaseRecord.IDCO = IDCO;
                        IfUpdated = true;
                    }

                    if (DataBaseRecord.Description != Description) {
                        DataBaseRecord.Description = Description;
                        IfUpdated = true;
                    }

                    if(IfUpdated) {
                        UpdatedRecords++;
                    }
                }
                else {
                    DataBaseRecord = new STK_DB() {
                        ANC = ANC,
                        Description = Description,
                        IDCO = IDCO,
                        Year = (decimal)Year,
                        Date = RecordDate,
                        STD = STK,
                    };
                    STK_Base.Add(DataBaseRecord);
                    AddRecords++;
                }
            }
            STK_Controller.Update_Year(STK_Base);

            return  new int[] { CheckRecords, UpdatedRecords, AddRecords };
        }

        private string PrepareLinktoSTK()
        {
            string Link = @"\\PLWS4031\Project\raporty Copics\";

            for (int DayMinus = 0; DayMinus >= -30; DayMinus--) {
                var CheckDay = DateTime.UtcNow.AddDays(DayMinus);
                string LinkToCheck = Link + CheckDay.Year.ToString() + @"\" +
                    CheckDay.Year.ToString() + CheckDay.Month.ToString("d2") +
                    @"\" + CheckDay.Year.ToString() + CheckDay.Month.ToString("d2") +
                    CheckDay.Day.ToString("d2") + @"\stdcosts.txt";

                if (File.Exists(LinkToCheck)) {
                    return LinkToCheck;
                }
            }

            return null;
        }
    }
}
