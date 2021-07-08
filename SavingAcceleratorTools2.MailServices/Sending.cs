using Saving_Accelerator_Tools2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace SavingAcceleratorTools2.MailServices
{
    public class Sending
    {
        public Sending()
        {

        }

        public void Mail(ICollection<User> recivers, string subject, string body)
        {
            if(recivers.Count() != 0)
            {
                MailMessage mail = new();
                SmtpClient smtpClient = new(Properties.Resources.SmtpClient, 25);
                mail.From = new MailAddress(Properties.Resources.Sender);

                string To = string.Empty;
                foreach (var reciver in recivers)
                {
                    To += reciver.Mail + ",";
                }
                To = To.Substring(0, To.Length - 1);

                mail.To.Add(To);
                mail.Subject = subject;
                mail.Body = body;
                smtpClient.Credentials = new System.Net.NetworkCredential(Properties.Resources.Sender, "");
                smtpClient.EnableSsl = false;

                smtpClient.Send(mail);
            }
        }
    }
}
