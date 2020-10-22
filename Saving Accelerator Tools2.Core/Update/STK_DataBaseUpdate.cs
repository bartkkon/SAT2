using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
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
            var context = new DataBaseConnetionContext();
            int CheckRecords = 0;
            int UpdatedRecords = 0;
            int AddRecords = 0;

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

                var DataBaseRecord = context.STK.Where(u => u.ANC == ANC && u.Year == (decimal)Year).FirstOrDefault();
                if(DataBaseRecord != null) {
                    var IfUpdate = false;
                    if (DataBaseRecord.STD != STK) {
                        DataBaseRecord.STD = STK;
                        IfUpdate = true;
                    }
                        
                    if (DataBaseRecord.IDCO != IDCO) {
                        DataBaseRecord.IDCO = IDCO;
                        IfUpdate = true;
                    }
                        
                    if (DataBaseRecord.Description != Description) {
                        DataBaseRecord.Description = Description;
                        IfUpdate = true;
                    }

                    if (IfUpdate) {
                        UpdatedRecords++;
                        context.STK.Update(DataBaseRecord);
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
                    context.STK.Add(DataBaseRecord);
                    AddRecords++;
                }
            }
            context.SaveChanges();

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
