using System.Net;
using System.Net.Mail;

namespace HappyFarmer.UI.Models
{
    public static class MailSender
    {
        //eger şifre sıfırlaması ise wherefrom gönderilmez ama kullanıcının verdiği ilan satılırsa kullanıcıya mail gider.
        public static void SendMail(string body, string email, bool? whereFrom = false)
        {
            string submit = "";
            var from = new MailAddress("mutluveysel02@gmail.com", "Happy Farmer (Mutlu Çiftçi)");
            var response = new MailAddress(email);

            if (whereFrom == true)
            {
                submit = "Happy Farmer (Mutlu Çiftçi) Sayesinde Ürününüz Satıldı :))";
            }
            else
            {
                submit = "Şifre Sıfırlama Talebi H.K.";
            }

            using (var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, "Veysel97.2")
            })
            {
                using (var message = new MailMessage(from, response) { Subject = submit, Body = body })
                {
                    smtpClient.Send(message);
                }
            }
        }
    }
}
