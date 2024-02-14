using System;
using System.Net.Mail;
using System.Web.Helpers;

namespace Jazb.Utilities.Mail
{
    public enum SendingMailResult
    {
        Successful,
        Faild
    }

    public static class EMail
    {
        public static string SmtpServer { get; set; }
        public static int SmtpPort { get; set; }
        public static bool EnableSsl { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string From { get; set; }

        public static SendingMailResult Send(string to, string subject, string body, ref string sendingResultError)
        {
            WebMail.SmtpServer = SmtpServer;
            WebMail.SmtpPort = SmtpPort;
            WebMail.EnableSsl = EnableSsl;
            WebMail.UserName = UserName;
            WebMail.Password = Password;
            WebMail.SmtpUseDefaultCredentials = true;
            WebMail.From = From;

            try
            {
                WebMail.Send(to, subject, body, From);
            }
            catch (Exception ex)
            {
                sendingResultError = ex.Message;
                return SendingMailResult.Faild;
            }
            return SendingMailResult.Successful;
        }

        public static SendingMailResult OtherSend(string to, string subject, string body, ref string sendingResultError)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = EnableSsl;
            client.Host = SmtpServer;
            client.Port = SmtpPort;

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(UserName, Password);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(From);
            msg.To.Add(new MailAddress(to));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                sendingResultError = ex.Message;
                return SendingMailResult.Faild;
            }
            return SendingMailResult.Successful;
        }

    }
}