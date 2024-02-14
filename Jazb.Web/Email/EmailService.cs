using Jazb.Utilities.Mail;

namespace Jazb.Web.Email
{
    public class EmailService
    {
        public static SendingMailResult Send(MailDocument doc, MailConfiguration config)
        {
            EMail.EnableSsl = config.EnableSsl;
            EMail.SmtpPort = config.SmtpPort;
            EMail.From = config.From;
            EMail.Password = config.Password;
            EMail.UserName = config.UserName;
            EMail.SmtpServer = config.SmtpServer;

            string error = string.Empty;
            var a1 = EMail.OtherSend(doc.ToEmail, doc.Subject, doc.Body, ref error);
            var a2 = EMail.Send(doc.ToEmail, doc.Subject, doc.Body, ref error);
            return a2;
        }
    }
}