using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Windows
{
    public class RevisionQuantityViewModel : Base
    {
        public RevisionQuantityViewModel(decimal Year,
            Revisions Revision,
            Factories Factory,
            IANCsServices ANCsServices,
            IPNCsServices PNCsServices,
            IPNCPlatformServices pNCPlatformServices,
            IMessageBoxService messageBoxService)
        {
            Save = new RelayCommand<Window>(SaveData);
            year = Year;
            revision = Revision;
            factory = Factory;
            aNCsServices = ANCsServices;
            pNCsServices = PNCsServices;
            this.pNCPlatformServices = pNCPlatformServices;
            this.messageBoxService = messageBoxService;
            dataColumn = revision switch
            {
                Revisions.BU => 13,
                Revisions.EA1 => 11,
                Revisions.EA2 => 8,
                Revisions.EA3 => 5,
                Revisions.EA4 => 0,
                _ => 0,
            };

            addColumn = revision switch
            {
                Revisions.BU => 0,
                Revisions.EA1 => 2,
                Revisions.EA2 => 5,
                Revisions.EA3 => 8,
                Revisions.EA4 => 0,
                _ => 0,
            };

            PlatformPrepare();
        }



        private string insertData;
        private readonly decimal year;
        private readonly int dataColumn;
        private readonly int addColumn;
        private decimal sumAll;
        private List<PNC> pNCs;
        private List<ANC> aNCs;
        private List<PNCPlatform> pNCPlatforms;
        private List<RevisonsQuantityModel> revisionQuantityModel;
        private readonly Revisions revision;
        private readonly Factories factory;
        private readonly IANCsServices aNCsServices;
        private readonly IPNCsServices pNCsServices;
        private readonly IPNCPlatformServices pNCPlatformServices;
        private readonly IMessageBoxService messageBoxService;


        public decimal SumAll
        {
            get => sumAll;
            set { sumAll = value; OnPropertyChange(); }
        }
        public List<RevisonsQuantityModel> RevisionQuantityModel
        {
            get => revisionQuantityModel;
            set
            {
                revisionQuantityModel = value;
                OnPropertyChange();
            }
        }
        public string InsertData
        {
            get { return insertData; }
            set
            {
                insertData = value;
                RevisionQuantityModel = new List<RevisonsQuantityModel>();
                sumAll = 0;
                ConvertData();
                OnPropertyChange();
            }
        }

        private void ConvertData()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (string.IsNullOrEmpty(insertData))
            {
                Mouse.OverrideCursor = default;
                return;
            }

            string[] InsertRows = insertData.Split(new[] { Environment.NewLine }, StringSplitOptions.TrimEntries);
            if (InsertRows[0].Split("\t").Length != dataColumn)
            {
                messageBoxService.ShowConfirmation("Something is Wrong!", "Quantity of Data Colums are incorrect, check insertion Data!");
                Mouse.OverrideCursor = default;
                return;
            }

            foreach (string InsertRow in InsertRows)
            {
                string[] InsertColumns = InsertRow.Split("\t", StringSplitOptions.TrimEntries);

                if (string.IsNullOrWhiteSpace(InsertColumns[0]))
                {
                    continue;
                }

                bool ExistElement = revisionQuantityModel.Any(i => i.Item == InsertColumns[0]);

                RevisonsQuantityModel Record = ExistElement ? revisionQuantityModel.First(i => i.Item == InsertColumns[0]) : new RevisonsQuantityModel() { Item = InsertColumns[0] };
                if (!ExistElement)
                {
                    revisionQuantityModel.Add(Record);
                }

                for (int counter = 1; counter < InsertColumns.Length; counter++)
                {
                    int column = counter + addColumn;
                    if (decimal.TryParse(InsertColumns[counter], out decimal Value))
                    {
                        var element = Record.GetType().GetProperty(((Months)Enum.ToObject(typeof(Months), column)).ToString());
                        element.SetValue(Record, (decimal)element.GetValue(Record) + Value, null);
                        sumAll += Value;
                    }
                }
            }
            OnPropertyChange(nameof(RevisionQuantityModel));
            OnPropertyChange(nameof(SumAll));
            Mouse.OverrideCursor = default;
        }

        public ICommand Save { get; private set; }

        private void SaveData(Window window)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            pNCs = new();
            aNCs = new();

            foreach (var RevQuantityRow in revisionQuantityModel)
            {
                if (RevQuantityRow.Item.Remove(3) is "911" or "999")
                {
                    for (int counter = addColumn + 1; counter <= 12; counter++)
                    {
                        PNC NewRecord = new()
                        {
                            Item = RevQuantityRow.Item,
                            Revision = revision,
                            Month = (Months)Enum.ToObject(typeof(Months), counter),
                            Factory = factory,
                            Year = year,
                            Quantity = (decimal)RevQuantityRow.GetType().GetProperty(((Months)Enum.ToObject(typeof(Months), counter)).ToString()).GetValue(RevQuantityRow),
                        };

                        if (NewRecord.Quantity != 0)
                        {
                            Structure RecordStruc = RevQuantityRow.Item.Remove(3) is "999"
                                ? Structure.PROXY
                                : NewRecord.Item.Remove(4).Remove(0, 3) switch
                                {
                                    "5" => Structure.DMD,
                                    "0" => Structure.D45,
                                    _ => throw new NotImplementedException(),
                                };

                            Installation? RecordInst;

                            RecordInst = NewRecord.Item.Remove(5).Remove(0, 4) switch
                            {
                                "1" or "5" => Installation.FS,
                                "2" or "6" => Installation.BI,
                                "3" or "7" => Installation.FI,
                                "4" or "8" => Installation.FSBU,
                                _ => null,
                            };

                            var PlatformRecord = pNCPlatforms.First(i => i.Month == NewRecord.Month && i.Structure == RecordStruc && i.Installation == RecordInst);
                            PlatformRecord.Quantity += NewRecord.Quantity;
                            pNCs.Add(NewRecord);
                        }
                    }
                }
                else
                {
                    for (int counter = addColumn + 1; counter <= 12; counter++)
                    {
                        ANC NewRecord = new()
                        {
                            Item = RevQuantityRow.Item,
                            Revision = revision,
                            Month = (Months)Enum.ToObject(typeof(Months), counter),
                            Factory = factory,
                            Year = year,
                            Quantity = (decimal)RevQuantityRow.GetType().GetProperty(((Months)Enum.ToObject(typeof(Months), counter)).ToString()).GetValue(RevQuantityRow),
                        };
                        if (NewRecord.Quantity != 0)
                        {
                            aNCs.Add(NewRecord);
                        }
                    }
                }
            }

            if (pNCs.Count != 0)
            {
                if (pNCsServices.Set(pNCs, revision, year))
                {
                    pNCPlatformServices.Set(pNCPlatforms);
                }
            }
            else if (aNCs.Count != 0)
            {
                aNCsServices.Set(aNCs, revision, year);
            }
            window.Close();
            Mouse.OverrideCursor = default;
        }
        private void PlatformPrepare()
        {
            pNCPlatforms = new();
            for (int counter = addColumn + 1; counter <= 12; counter++)
            {
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.FS, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.FI, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.BI, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.FSBU, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.FS, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.FI, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.BI, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.FSBU, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
                pNCPlatforms.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.PROXY, Installation = null, Year = year, Factory = factory, Month = (Months)Enum.ToObject(typeof(Months), counter) });
            }
        }
    }
    public class RevisonsQuantityModel
    {
        public string Item { get; set; }
        public decimal January { get; set; }
        public decimal February { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }
    }

}
