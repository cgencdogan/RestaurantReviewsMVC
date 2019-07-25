using System.Net.Mail;

namespace RestaurantReviews.WebUI.Managers {
    public class MailManager {
        public void SendMail(string fromMail, string fromMailPassword, string toMail, string mailSubject, string mailBody) {
            MailMessage mail = new MailMessage(fromMail, toMail);
            mail.Subject = mailSubject;
            mail.Body = mailBody;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential() {
                UserName = fromMail,
                Password = fromMailPassword
            };

            smtpClient.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                        return true;
                    };
            smtpClient.Send(mail);
        }
    }
}