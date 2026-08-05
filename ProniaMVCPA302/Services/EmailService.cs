using System.Net.Mail;

namespace ProniaMVCPA302.Services
{
    public class EmailService : IEmailService
    {
        public void Send()
        {
            Console.WriteLine("Mail sent successfully.");
        }
    }
}
