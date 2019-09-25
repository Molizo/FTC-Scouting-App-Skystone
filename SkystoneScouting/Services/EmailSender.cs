using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace SkystoneScouting.Services
{
    public class EmailSender : IEmailSender
    {
        #region Public Constructors

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        #endregion Public Constructors

        #region Public Properties

        public AuthMessageSenderOptions Options { get; }

        #endregion Public Properties

        //set only via Secret Manager

        #region Public Methods

        public Task ExecuteMailgun(string subject, string message, string email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.eu.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "key-5489b57d643818a966b73aeb43befe79");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "mg.qrobotics.eu", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "FTC Scouting App <noreply@qrobotics.eu>");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("html", message);
            request.Method = Method.POST;
            client.Execute(request);
            return Task.FromResult(0);
        }

        public Task ExecuteSendgrid(string subject, string message, string email)
        {
            var client = new SendGridClient(Options.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@qrobotics.eu", "FTC Scouting App"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //return ExecuteSendgrid(Options.SendGridKey, subject, message, email);
            return ExecuteMailgun(subject, message, email);
        }

        #endregion Public Methods
    }
}