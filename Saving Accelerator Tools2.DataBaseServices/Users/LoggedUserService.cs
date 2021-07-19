using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Saving_Accelerator_Tools2.DataBaseServices.Users
{
    public class LoggedUserService : ILoginUserService
    {
        private readonly ConnectionContext connection;
        private readonly IMessageBoxService messageBoxService;
        private User loggedUser;

        public LoggedUserService(ConnectionContext connection, IMessageBoxService messageBoxService)
        {
            this.connection = connection;
            this.messageBoxService = messageBoxService;
        }
        public User Get()
        {
            if (loggedUser == null)
            {
                loggedUser = connection.Users.Include(d => d.Devision).FirstOrDefault(l => l.BIZLogin == Environment.UserName.ToLower());
                if (loggedUser == null)
                {
                    messageBoxService.ShowConfirmation("Login Faild!", $"You don't have or lost access to this application.{Environment.NewLine}Please contact to administrator.");
                    Application.Current.Shutdown();
                }
            }
            return loggedUser;
        }
    }
}
