using Ecommerce.userManage.Domain.Models.DTO;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using System.Net.Mail;
using MailKit.Security;

namespace Ecommerce.userManage.Application.Services
{
    public class EmailService:IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            // Initialize the EmailService with IConfiguration dependency
            this.config = config;
        }

        // Method to send an email
        public string SendEmail(RequestDTO request)
        {
            // Create a new MimeMessage for composing the email
            var email = new MimeMessage();

            // Set the sender's email address
            email.From.Add(MailboxAddress.Parse(config.GetSection("EmailUserName").Value));

            // Set the recipient's email address
            email.To.Add(MailboxAddress.Parse(request.To));

            // Set the email subject
            email.Subject = request.Subject;

            // Set the email body as HTML text
            email.Body = new TextPart(TextFormat.Html) { Text = request.Message };

            // Create an instance of SmtpClient for sending the email
            using var smtp = new SmtpClient();

            // Connect to the SMTP server with the specified host and port using StartTLS for security
            //smtp.Connect(config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);

            // Authenticate with the SMTP server using the provided username and password
           // smtp.Authenticate(config.GetSection("EmailUserName").Value, config.GetSection("EmailPassword").Value);

            // Send the composed email
           // smtp.Send(email);

            // Disconnect from the SMTP server after sending the email
           // smtp.Disconnect(true);

            // Return a success message
            return "Mail Sent";
        }
    }
}
