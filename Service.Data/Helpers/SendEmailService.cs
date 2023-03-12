using Infrastructure.ViewModel.VM;
using Service.Interface.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service.Data.Helpers
{
    public class SendEmailService: ISendEmailService
    {
        private readonly Dictionary<string, string> emailSettings;

        public SendEmailService(Dictionary<string, string> emailSettings)
        {
            this.emailSettings = emailSettings;
        }

        public List<ResSystemSetting> EmailSettings { get; }

        public async Task<(bool, string)> SendEmail(ResEmail mail)
        {

            if (emailSettings == null)
                return (false, "invalid email settings");

            MailAddress fromAddress = new MailAddress(emailSettings["From"], emailSettings["FromTitle"]);
            

            MailMessage mailmsg = new MailMessage
            {
                From = fromAddress
            };
            foreach (var email in mail.EmailTo.Split(";"))
            {
                mailmsg.To.Add(email);
            }
            mailmsg.Body = mail.Body;
            mailmsg.Subject = mail.Subject;
            mailmsg.IsBodyHtml = true;
            
            SmtpClient smtp = new SmtpClient
            {
                Host = emailSettings["Host"],
                Port = int.Parse(emailSettings["Port"]),
                EnableSsl = bool.Parse(emailSettings["EnableSsl"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,

                Credentials = new NetworkCredential(fromAddress.Address, emailSettings["Password"]),
                //   Timeout = 100
            };

            try
            {
                await smtp.SendMailAsync(mailmsg);

                return (true, null);
            }
            catch (SmtpException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
