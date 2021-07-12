using Saving_Accelerator_Tools2.DataBaseIServices.Data;
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
    public class MonthlyQuantityViewModel : Base
    {
        public MonthlyQuantityViewModel(decimal Year,
            Months Month,
            Factories Factory,
            IANCsServices ANCsServices,
            IPNCsServices PNCsServices,
            IPNCPlatformServices pNCPlatformServices)
        {
            Save = new RelayCommand<Window>(SaveData);
            year = Year;
            month = Month;
            factory = Factory;
            aNCsServices = ANCsServices;
            pNCsServices = PNCsServices;
            this.pNCPlatformServices = pNCPlatformServices;
        }

        private string insertData;

        private readonly decimal year;
        private readonly Months month;
        private readonly Factories factory;
        private readonly IANCsServices aNCsServices;
        private readonly IPNCsServices pNCsServices;
        private readonly IPNCPlatformServices pNCPlatformServices;
        private List<ANC> aNCData;
        private List<PNC> pNCData;
        private List<PNCPlatform> platform;
        private Visibility aNCVisibility = Visibility.Visible;
        private Visibility pNCVisibility = Visibility.Collapsed;
        private Visibility platformVisibility = Visibility.Collapsed;
        private decimal sum;

        public decimal Sum
        {
            get { return sum; }
            set { sum = value; OnPropertyChange(); }
        }


        public Visibility ANCVisibility
        {
            get => aNCVisibility;
            set { aNCVisibility = value; OnPropertyChange(); }
        }
        public Visibility PNCVisibility
        {
            get => pNCVisibility;
            set { pNCVisibility = value; OnPropertyChange(); }
        }

        public List<PNCPlatform> Platform
        {
            get => platform;
            set { platform = value; OnPropertyChange(); }
        }
        public List<PNC> PNCData
        {
            get => pNCData;
            set { pNCData = value; OnPropertyChange(); }
        }
        public List<ANC> ANCData
        {
            get => aNCData;
            set { aNCData = value; OnPropertyChange(); }
        }

        public Visibility PlatformVisibility
        {
            get => platformVisibility;
            set { platformVisibility = value; OnPropertyChange(); }
        }
        public string InsertData
        {
            get => insertData;
            set
            {
                insertData = value;
                ANCData = new List<ANC>();
                PNCData = new List<PNC>();
                Platform = new List<PNCPlatform>();
                sum = 0;
                PlatformPepare();
                Mouse.OverrideCursor = Cursors.Wait;
                ConvertData();
                Mouse.OverrideCursor = default;
                OnPropertyChange();
            }
        }

        public ICommand Save { get; private set; }
        private void SaveData(Window window)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if(pNCData.Count != 0)
            {
                if(pNCsServices.Set(pNCData, month, year))
                {
                    pNCPlatformServices.Set(platform);
                }
            }
            else if(aNCData.Count != 0)
            {
                aNCsServices.Set(aNCData, month, year);
            }
            window.Close();
            Mouse.OverrideCursor = default;
        }

        private void ConvertData()
        {

            if (!string.IsNullOrEmpty(insertData))
            {
                string[] insertDataRows = insertData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string insertDataRow in insertDataRows)
                {
                    string[] columnData = insertDataRow.Split("\t");
                    if (columnData.Length != 2)
                    {
                        continue;
                    }

                    if (decimal.TryParse(columnData[1], out decimal Quantity))
                    {
                        if (Quantity != 0)
                        {
                            if (columnData[0].Remove(3) is "911" or "999")
                            {
                                PNC newRecord = new()
                                {
                                    Item = columnData[0].Trim().Replace(" ", ""),
                                    Quantity = Quantity,
                                    Month = month,
                                    Year = year,
                                    Revision = Revisions.EA4,
                                    Factory = factory,
                                };
                                PNCData.Add(newRecord);
                                PlatformAdd(newRecord);
                            }
                            else
                            {
                                ANC newRecord = new()
                                {
                                    Item = columnData[0].Trim().Replace(" ", ""),
                                    Quantity = Quantity,
                                    Month = month,
                                    Year = year,
                                    Revision = Revisions.EA4,
                                    Factory = factory,
                                };
                                ANCData.Add(newRecord);
                            }
                            Sum += Quantity;
                        }
                    }

                }
                if (pNCData.Count != 0)
                {
                    Platform = platform;
                    PNCVisibility = Visibility.Visible;
                    ANCVisibility = Visibility.Collapsed;
                    PlatformVisibility = Visibility.Visible;
                }
                else
                {
                    PNCVisibility = Visibility.Collapsed;
                    ANCVisibility = Visibility.Visible;
                    PlatformVisibility = Visibility.Collapsed;
                }
            }
        }

        private void PlatformAdd(PNC Value)
        {
            if (Value.Item.Remove(3) is "999")
            {
                platform.First(s => s.Structure == Structure.PROXY).Quantity += Value.Quantity;
                return;
            }
            if (Value.Item.Remove(4).Remove(0, 3) == "5")
            {
                switch (Value.Item.Remove(5).Remove(0, 4))
                {
                    case "1":
                        platform.First(s => s.Structure == Structure.DMD && s.Installation == Installation.FS).Quantity += Value.Quantity;
                        break;
                    case "2":
                        platform.First(s => s.Structure == Structure.DMD && s.Installation == Installation.BI).Quantity += Value.Quantity;
                        break;
                    case "3":
                        platform.First(s => s.Structure == Structure.DMD && s.Installation == Installation.FI).Quantity += Value.Quantity;
                        break;
                    case "4":
                        platform.First(s => s.Structure == Structure.DMD && s.Installation == Installation.FSBU).Quantity += Value.Quantity;
                        break;
                    default:
                        break;
                }
            }
            else if(Value.Item.Remove(4).Remove(0, 3) is "0")
            {
                switch (Value.Item.Remove(5).Remove(0, 4))
                {
                    case "5":
                        platform.First(s => s.Structure == Structure.D45 && s.Installation == Installation.FS).Quantity += Value.Quantity;
                        break;
                    case "6":
                        platform.First(s => s.Structure == Structure.D45 && s.Installation == Installation.BI).Quantity += Value.Quantity;
                        break;
                    case "7":
                        platform.First(s => s.Structure == Structure.D45 && s.Installation == Installation.FI).Quantity += Value.Quantity;
                        break;
                    case "8":
                        platform.First(s => s.Structure == Structure.D45 && s.Installation == Installation.FSBU).Quantity += Value.Quantity;
                        break;
                    default:
                        break;
                }
            }
        }

        private void PlatformPepare()
        {
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.FS, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.FI, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.BI, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.DMD, Installation = Installation.FSBU, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.FS, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.FI, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.BI, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.D45, Installation = Installation.FSBU, Year = year, Factory = factory, Month = month });
            platform.Add(new PNCPlatform() { Revision = Revisions.EA4, Structure = Structure.PROXY, Installation = null, Year = year, Factory = factory, Month = month });
        }
    }
}
