using BsnssX.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BsnssX.Core
{
    public class SendEmail
    {                
        public bool SendHtmlEmail(EmailConfiguration config, string from, List<string> recipients, string subject, string body)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"From : {from}");
            sb.AppendLine($"To : {string.Join(',', recipients)}");
            sb.AppendLine($"Subject : {subject}");
            sb.AppendLine($"Date : {DateTime.Now}");

            try
            {                
                if (!recipients.Any())
                {
                    sb.AppendLine($"Error sending email, no recipients !");
                    Info = sb.ToString();
                    return false;
                }
                    
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(config.server);

                mail.From = new MailAddress(from);
                recipients.ForEach(adr => mail.To.Add(adr));
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(config.login, config.pwd);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                sb.AppendLine($"Email was send successfully");
                Info = sb.ToString();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
                sb.AppendLine($"Error sending email")
                   .AppendLine($"Error: {ErrorMsg}");
                Info = sb.ToString();
                return false;
            }
        }         
        public string ErrorMsg { get; private set; }

        public string Info { get; private set; }
    }    
}
