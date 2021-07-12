using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class CurrencyViewModel : Base
    {
        public CurrencyViewModel(ICurrenciesServices currenciesServices)
        {
            SaveButton = new RelayCommand(Save);

            this.currenciesServices = currenciesServices;

            Year = DateTime.UtcNow.Year;
        }

        private decimal year;
        private ICollection<Currencies> Currencies;
        private Currencies pLN;
        private Currencies eUR;
        private Currencies uSD;
        private Currencies sEK;
        private readonly ICurrenciesServices currenciesServices;

        public Currencies PLN
        {
            get => pLN;
            set{ pLN = value; OnPropertyChange(); }
        }
        public Currencies EUR
        {
            get => eUR;
            set { eUR = value; OnPropertyChange(); }
        }
        public Currencies USD
        {
            get => uSD;
            set { uSD = value; OnPropertyChange(); }
        }
        public Currencies SEK
        {
            get => sEK;
            set { sEK = value; OnPropertyChange(); }
        }
        public decimal Year
        {
            get { return year; }
            set
            {
                year = value;
                LoadCurriency();
                OnPropertyChange();
            }
        }

        public ICommand SaveButton { get; private set; }

        private void Save()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            currenciesServices.Set(Currencies);
            Mouse.OverrideCursor = default;
        }

        private void  LoadCurriency()
        {
            Currencies = currenciesServices.Get(year);
            if (Currencies.Count == 0)
            {
                Currencies.Add(new Currencies() { Symbol = "zł", Currency = Currency.PLN, Year = Year});
                Currencies.Add(new Currencies() { Symbol = "$", Currency = Currency.USD, Year = Year });
                Currencies.Add(new Currencies() { Symbol = "€", Currency = Currency.EUR, Year = Year });
                Currencies.Add(new Currencies() { Symbol = "kr", Currency = Currency.SEK, Year = Year });
            }
            PLN = Currencies.First(i => i.Currency == Currency.PLN);
            USD = Currencies.First(i => i.Currency == Currency.USD);
            EUR = Currencies.First(i => i.Currency == Currency.EUR);
            SEK = Currencies.First(i => i.Currency == Currency.SEK);
        }

    }
}
