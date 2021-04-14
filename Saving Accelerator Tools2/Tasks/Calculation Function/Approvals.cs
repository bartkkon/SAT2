using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks.Calculation_Function
{
    public class Approvals
    {
        private ICollection<Approvals_DB> DataBase;

        public ICollection<Approvals_DB> Check_Revisions(decimal ActionYear)
        {
            var Revisions = new List<Approvals_DB>();

            if (DataBase == null)
                Load();

            if (DataBase.Count != 0)
            {
                foreach (var DataBaseRecord in DataBase)
                {
                    if (DataBaseRecord.Revision != null)
                    {
                        if (DataBaseRecord.Year == ActionYear || DataBaseRecord.Year == ActionYear + 1)
                        {
                            Revisions.Add(DataBaseRecord);
                        }
                    }
                }
            }

            return Revisions;
        }
        public ICollection<Approvals_DB> Check_Month(decimal ActionYear)
        {
            var Month = new List<Approvals_DB>();

            if (DataBase == null)
                Load();

            if (DataBase.Count != 0)
            {
                foreach (var DataBaseRecord in DataBase)
                {
                    if (DataBaseRecord.Month != null)
                    {
                        if (DataBaseRecord.Year == ActionYear || DataBaseRecord.Year == ActionYear + 1)
                        {
                            Month.Add(DataBaseRecord);
                        }
                    }
                }
            }

            return Month;
        }
        public ICollection<Approvals_DB> Check_Devisions(string ActionDevision, decimal ActionYear)
        {
            var Devisions = new List<Approvals_DB>();

            if (DataBase == null)
                Load();

            if (DataBase.Count != 0)
            {
                foreach (var DataBaseRecord in DataBase)
                {
                    if (DataBaseRecord.Month == null && DataBaseRecord.Revision == null)
                    {
                        if (DataBaseRecord.Devision == ActionDevision)
                        {
                            if (DataBaseRecord.Year == ActionYear || DataBaseRecord.Year == ActionYear + 1)
                            {
                                Devisions.Add(DataBaseRecord);
                            }
                        }
                    }
                }
            }

            return Devisions;
        }

        private void Load()
        {
            DataBase = Approvals_Controller.LoadOpenYears();
        }
    }
}
