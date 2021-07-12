using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData
{
    public class CurrienciesServices : ICurrenciesServices
    {
        private readonly ConnectionContext connection;
        private readonly IMessageBoxService messageBoxService;

        public CurrienciesServices(ConnectionContext connection, IMessageBoxService messageBoxService)
        {
            this.connection = connection;
            this.messageBoxService = messageBoxService;
        }
        public ICollection<Currencies> Get(decimal year)
        {
            return connection.Currencies.Where(i => i.Year == year).ToList();
        }

        public Currencies Get(decimal year, Currency currency)
        {
            return connection.Currencies.FirstOrDefault(i => i.Year == year && i.Currency == currency);
        }

        public void Set(ICollection<Currencies> currencies)
        {
            foreach (var currency in currencies)
            {
                if (currency.ID == 0)
                {
                    connection.Currencies.Add(currency);
                }
                else
                {
                    connection.Currencies.Update(currency);
                }
            }
            connection.SaveChanges();
            messageBoxService.ShowConfirmation("Curriencies Update!", $"Curriences for {currencies.First().Year} Updated!");
        }
    }
}
