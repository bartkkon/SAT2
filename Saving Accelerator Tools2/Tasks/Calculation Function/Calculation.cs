using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;

namespace Saving_Accelerator_Tools2.Tasks.Calculation_Function
{
    public class Calculation : Functions
    {
        private readonly List<ItemModel> ItemList;
        private readonly bool ECCCCalc;
        private readonly decimal ECCCSecCost;
        private List<CalculationModels> Calculations;
        private readonly decimal ActionYear;
        private readonly List<string> PNC = new List<string>();
        private readonly List<string> ANC = new List<string>();

        public Calculation(List<ItemModel> ItemList, bool ECCCCalc, List<CalculationModels> calculations, decimal actionYear)
        {
            this.ItemList = ItemList;
            this.ECCCCalc = ECCCCalc;
            Calculations = calculations;
            ActionYear = actionYear;

            foreach (var Item in ItemList)
            {
                if (Item.Item.Length == 9 && (Item.Item.Remove(3) == "911" || Item.Item.Remove(3) == "999"!))
                {
                    PNC.Add(Item.Item);
                }
                else
                {
                    ANC.Add(Item.Item);
                }
            }
            if (ECCCCalc)
            {
                ECCCSecCost = Currency_Controller.Lead_Year(ActionYear).ECCC;
            }
        }

        public void Range(Approvals_DB Revision, int StartMonth, int FinishMonth, decimal QuantityPercent)
        {
            List<PNC_DB> PNCQuantity = new List<PNC_DB>();
            List<ANC_DB> ANCQuantity = new List<ANC_DB>();
            List<PNCTotality_DB> Platform = new List<PNCTotality_DB>();
            bool CarryOver = Revision.Year == ActionYear ? false : true;

            var CalculateRows = FindANDClear(0, 0, Revision.Revision, CarryOver);

            if (PNC.Count != 0)
            {
                PNCQuantity = PNC_Controller.SearchList(PNC, Revision.Year, Revision.Revision, StartMonth, FinishMonth).ToList();
            }
            if (ANC.Count != 0)
            {
                ANCQuantity = ANC_Controller.SearchList(ANC, Revision.Year, Revision.Revision, StartMonth, FinishMonth).ToList();
                Platform = PNCTotally_Controler.SearchList(Revision.Year, Revision.Revision, StartMonth, FinishMonth).ToList();
            }

            for (var CalcMonth = StartMonth; CalcMonth <= FinishMonth; CalcMonth++ )
            {
                foreach (var Item in ItemList)
                {
                    var CalculationRow = CalculateRows.Where(b => b.Item == Item.Item && b.Month == CalcMonth).FirstOrDefault();
                    if (CalculationRow == null)
                    {
                        CalculationRow = new CalculationModels()
                        {
                            Item = Item.Item,
                            Revision = Revision.Revision,
                            Month = CalcMonth,
                            CarryOver = CarryOver,
                        };
                        Calculations.Add(CalculationRow);
                    }
                    else
                    {
                        CalculationRow.ToRemove = false;
                        CalculationRow.Update = true;
                    }


                    if (PNCCheck(Item.Item))
                    {
                        var PNCQ = PNCQuantity.Where(b => b.Item == Item.Item && b.Month == CalcMonth).FirstOrDefault();
                        if (PNCQ != null)
                        {
                            CalculationRow.Quantity = Math.Round(PNCQ.Quantity * (QuantityPercent / 100), 0, MidpointRounding.AwayFromZero);
                        }
                    }
                    else
                    {
                        if (ANCCheck(Item.Item))
                        {
                            var ANCQ = ANCQuantity.Where(b => b.Item == Item.Item && b.Month == CalcMonth).FirstOrDefault();
                            if (ANCQ != null)
                            {
                                CalculationRow.Quantity = Math.Round(ANCQ.Quantity * (QuantityPercent / 100), 0, MidpointRounding.AwayFromZero);
                            }
                        }
                        else if (PlatformCheck(Item.Item))
                        {
                            var PlatformQuantity = new List<PNCTotality_DB>();
                            if (Item.Item == "ALL")
                            {
                                PlatformQuantity = Platform.Where(b => b.Month == CalcMonth).ToList();
                            }
                            else if (Item.Item == "DMD" || Item.Item == "D45")
                            {
                                PlatformQuantity = Platform.Where(b => b.Month == CalcMonth && b.Structure == Item.Item).ToList();
                            }
                            else
                            {
                                var Structure = Item.Item.Remove(3);
                                var Installation = Item.Item.Remove(0, 4);
                                PlatformQuantity = Platform.Where(b => b.Month == CalcMonth && b.Structure == Structure && b.Instalation == Installation).ToList();
                            }

                            if (PlatformQuantity != null)
                            {
                                CalculationRow.Quantity = Math.Round(PlatformQuantity.Sum(item => item.Quantity) * (QuantityPercent / 100), 0, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
                    //Nie zawsze ma być brana estymacja - jak zrobić aby wiedział kiedy dać estymacje a kiedy już finalną wartość???????????????????????????????????
                    CalculationRow.Savings = Math.Round(CalculationRow.Quantity * Item.Savings_Estymation, 4, MidpointRounding.AwayFromZero);
                    if (ECCCCalc)
                    {
                        CalculationRow.ECCC = Math.Round(CalculationRow.Quantity * Item.ECCC * ECCCSecCost, 4, MidpointRounding.AwayFromZero);
                    }

                    if (!Item.Plus)
                    {
                        CalculationRow.Quantity *= -1;
                        CalculationRow.Savings *= -1;
                        CalculationRow.ECCC *= -1;
                    }
                }
            }
        }

        private List<CalculationModels> FindANDClear(int StartMonth, int FinishMonth, string Revision, bool CarryOver)
        {
            List<CalculationModels> CalculateRows = new List<CalculationModels>();

            if (Revision == "EA4")
            {
                CalculateRows = Calculations.Where(b => b.Month >= StartMonth && b.Month <= FinishMonth && b.Revision == Revision && b.CarryOver == CarryOver).ToList();
            }
            else
            {
                CalculateRows = Calculations.Where(b => b.Revision == Revision && b.CarryOver == CarryOver).ToList();
            }

            foreach (var CalculateRow in CalculateRows)
            {
                CalculateRow.ToRemove = true;
                CalculateRow.Savings = 0;
                CalculateRow.Quantity = 0;
                CalculateRow.ECCC = 0;
            }

            return CalculateRows;
        }

        public List<CalculationModels> ReturnCalculation()
        {
            return Calculations;
        }
    }
}
