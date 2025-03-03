using MailKit.Net.Smtp;
using Marqdouj.Aspire.MailKit.Client;
using MimeKit;
using System.Net.Mail;

namespace AspireDemo.ApiService.EndPoints
{
    internal static class Newsletter
    {
        private const string emailDefault = "email.test@dummy.com";

        /// <summary>
        /// If email is empty, return the default email.(i.e. OpenAPI UI Testing)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static string GetEmail(string email) => string.IsNullOrWhiteSpace(email) ? emailDefault : email;

        public static void MapNewsletter(this WebApplication app)
        {
            app.MapGet("/newsletter/is-subscribed", (string email) =>
            {
                return email == emailDefault;
            })
            .WithName("Is-Subscribed");

            app.MapPost("/newsletter/subscribe",
                async (MailKitClientFactory factory, string email) =>
                {
                    email = GetEmail(email);
                    ISmtpClient client = await factory.GetSmtpClientAsync();

                    using var message = new MailMessage("newsletter@yourcompany.com", email)
                    {
                        Subject = "Welcome to our newsletter!",
                        Body = "Thank you for subscribing to our newsletter!"
                    };

                    await client.SendAsync(MimeMessage.CreateFromMailMessage(message));
                })
            .WithName("Subscribe");

            app.MapPost("/newsletter/unsubscribe",
                async (MailKitClientFactory factory, string email) =>
                {
                    email = GetEmail(email);
                    ISmtpClient client = await factory.GetSmtpClientAsync();

                    using var message = new MailMessage("newsletter@yourcompany.com", email)
                    {
                        Subject = "You are unsubscribed from our newsletter!",
                        Body = "Sorry to see you go. We hope you will come back soon!"
                    };

                    await client.SendAsync(MimeMessage.CreateFromMailMessage(message));
                })
            .WithName("Unsubscribe");
        }
    }
}
