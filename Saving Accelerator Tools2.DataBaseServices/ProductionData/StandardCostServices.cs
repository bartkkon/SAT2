using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.ProductionData.SubFunction;
using Saving_Accelerator_Tools2.IServices.File;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.MailServices;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saving_Accelerator_Tools2.DataBaseServices.ProductionData
{
    public class StandardCostServices : IStandardCostServices
    {
        private readonly ConnectionContext connection;
        private readonly Sending sending;
        private readonly IFileService fileService;
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;
        private readonly IUserServices userServices;

        public StandardCostServices(ConnectionContext connection,
                                    Sending sending,
                                    IFileService fileService,
                                    IFileDialogService fileDialogService,
                                    IMessageBoxService messageBoxService,
                                    IUserServices userServices)
        {
            this.connection = connection;
            this.sending = sending;
            this.fileService = fileService;
            this.fileDialogService = fileDialogService;
            this.messageBoxService = messageBoxService;
            this.userServices = userServices;
        }
        public void Clear(decimal year)
        {
            var deleteElements = connection.StandardCosts.Where(d => d.Year == year).ToList();

            if (deleteElements.Count == 0)
            {
                return;
            }

            connection.StandardCosts.RemoveRange(deleteElements);
            connection.SaveChanges();
        }

        public StandardCost Get(string item, decimal year, Factories factory)
        {
            return connection.StandardCosts.Include(fac =>fac.Factory).FirstOrDefault(d => d.Item == item && d.Year == year);
        }

        public ICollection<StandardCost> Get(decimal year)
        {
            return connection.StandardCosts.Where(d => d.Year == year).ToList();
        }

        public ICollection<StandardCost> Get(ICollection<string> items, decimal year)
        {
            var searchElements = new List<StandardCost>();
            foreach (var item in items)
            {
                var searchElement = connection.StandardCosts.Where(d => d.Item == item && d.Year == year).FirstOrDefault();
                if (searchElement != null)
                {
                    searchElements.Add(searchElement);
                }
            }

            return searchElements;
        }

        public void Set(ICollection<StandardCost> newList)
        {
            var newElements = newList.Where(c => c.ID == 0).ToList();
            var updateElements = newList.Where(c => c.ID != 0).ToList();

            if (newElements.Count != 0)
            {
                connection.StandardCosts.AddRange(newElements);
            }
            if (updateElements.Count != 0)
            {
                connection.StandardCosts.UpdateRange(updateElements);
            }
            connection.SaveChanges();

        }

        public void UpdateFromReport(Factories factory)
        {
            bool status = false;
            OpenFileDialog openObject;
            string[] Data;
            string filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            string initialDirection = @$"\\PLWS4031\Project\raporty Copics\{DateTime.UtcNow.Year}\{DateTime.UtcNow.Year}{DateTime.UtcNow.Month:d2}\{DateTime.UtcNow.Year}{DateTime.UtcNow.Month:d2}{DateTime.UtcNow.Day:d2}";

            List<StandardCost> updatetedList;
            List<StandardCost> DataBaseList;
            string Updated = string.Empty;
            string Add = string.Empty;
 
            do
            {
                openObject = fileDialogService.OpenFile(filter, initialDirection);

                if (openObject == null)
                {
                    return;
                }

                if (openObject.SafeFileName != "stdcosts.txt")
                {
                    MessageBoxResult result = messageBoxService.Ask("Are you sure?!", $"File name are different than pattern.{Environment.NewLine}Do you want continue with selected file?");
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        status = true;
                    }
                }
            } while (status);

            Data = fileService.Read(openObject.FileName);

            if(Data == null)
            {
                messageBoxService.Error("Error!", "Something go wrong and Data in file is empty!");
                return;
            }

            updatetedList = new StandardCostReport().Convert(Data, factory).ToList();

            if(updatetedList.Count == 0)
            {
                return;
            }

            DataBaseList = Get(updatetedList[0].Year).ToList();

            foreach(var updatedrecord in updatetedList)
            {
                var DataBaseRecord = DataBaseList.Where(i => i.Item == updatedrecord.Item).FirstOrDefault();
                
                if(DataBaseRecord == null)
                {
                    DataBaseList.Add(updatedrecord);
                    Add += $"{updatedrecord.Item} => {updatedrecord.STK3}" + Environment.NewLine;
                }
                else
                {
                    if(DataBaseRecord.STK3 != updatedrecord.STK3)
                    {
                        Updated += $"{updatedrecord.Item} // {DataBaseRecord.STK3} => {updatedrecord.STK3}";
                        DataBaseRecord.STK3 = updatedrecord.STK3;
                        DataBaseRecord.Date = updatedrecord.Date;
                        
                    }
                    if(DataBaseRecord.IDCO != updatedrecord.IDCO)
                    {
                        DataBaseRecord.IDCO = updatedrecord.IDCO;
                    }
                    if(DataBaseRecord.Description != updatedrecord.Description)
                    {
                        DataBaseRecord.Description = updatedrecord.Description;
                    }
                    if(factory.Plant == "PLV")
                    {
                        DataBaseRecord.Currency = Currency.PLN;
                    }
                }
            }
            Set(DataBaseList);
            sending.Mail(userServices.MailSubscriptions(Subscription.StandardCost, factory), "Standard Cost Data Updated", $"New Record: {Environment.NewLine}{Add}{Environment.NewLine} Updated Record: {Environment.NewLine}{Updated}");
            
        }
    }
}
