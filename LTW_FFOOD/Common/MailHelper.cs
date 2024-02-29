using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace LTW_FFOOD.Common
{
    public class MailHelper
    {
        public void SendMail(string toEmail, string subject, string content)
        {
            var FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var FromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var FromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var SMTPHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var SMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(FromEmailAddress, FromEmailDisplayName), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("thanhtroll0915@gmail.com", "zpycpcoezgkwsbdk"),
                EnableSsl = enabledSsl
            };

            client.Send(message);
        }
    }
}
