using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace Domain.Concreate
{
    public class EmailSettings
    {
        public string AddressToMail = "orders@example.com";
        public string AddressFromMail = "cafebuhara@example.com";
        public bool SslUse = true;
        public string Username = "MynameUser";
        public string Password = "MyPassword";
        public string NameServer = "smtp.example.com";
        public int PortServer = 587;
        public bool WriteAsFile = true;
        public string LocationFile = @"c:\cafe_buhara_emails";
    }
    public class ProcessorOrderEmail : IProcessorOrder
    {
        private EmailSettings  emailSettings;
          public ProcessorOrderEmail (EmailSettings settings)
        {
            emailSettings = settings;
        }
public void ProcessOrder(Entities.Bascket bascket, DeliveryDetails deliveryDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.SslUse;
                smtpClient.Host = emailSettings.NameServer;
                smtpClient.Port = emailSettings.PortServer;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.LocationFile;
                    smtpClient.EnableSsl = false;

                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var line in bascket.Lines)
                {
                    var totalsub = line.Dish.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}", line.Quantity, line.Dish.Name, totalsub);
                }

                body.AppendFormat("Общая стоимость: {0:c}", bascket.CalculateTotalValue())
                .AppendLine("---")
                .AppendLine("Доставка:")
                .AppendLine(deliveryDetails.Name)
                .AppendLine(deliveryDetails.Address1)
                .AppendLine(deliveryDetails.Address2 ?? "")
                .AppendLine(deliveryDetails.Address3 ?? "")
                .AppendLine(deliveryDetails.City)
                .AppendLine(deliveryDetails.Country)
                .AppendLine("---")
                .AppendFormat("Подарочная упаковка: {0}", deliveryDetails.GiftPackaging ? "Да" : "Нет");
                MailMessage mailMessage = new MailMessage(
                    emailSettings.AddressFromMail,
                    emailSettings.AddressToMail,
                    "Новый заказ отправлен!",
                    body.ToString()
                    );
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }
                smtpClient.Send(mailMessage);
                }
            }
        }
    }

