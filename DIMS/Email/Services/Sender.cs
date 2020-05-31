using DIMS.BL.Models;
using Email.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace DIMS.Email.Services
{
    public class Sender : ISender
    {

        private const string _layoutHtml =
            "<div style=\"margin-top: 20px;\">Best regards, Dev Incubator Inc.</div>" +
            "<div><img src=\"https://i.ibb.co/9tSLsd6/logo-name.png\" style=\"margin-top:26px; width:250px !important; height:100px !important;\"/>" +
            "</div>";

        public async Task<string> MessageToUserAsync(UserDTO user, string subject, string html)
        {
            /*
            var to = new EmailAddress(user.Email, $"{user.Name}");

            var htmlContent = "<div>" + html + _layoutHtml + "</div>";

            var msg = MailHelper.CreateSingleEmail(Email, to, subject, "Confirmation", htmlContent);

            await Client.SendEmailAsync(msg);
            */

            var htmlContent = "<div>" + html + _layoutHtml + "</div>";

            RestClient client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator =
            new HttpBasicAuthenticator("api",
                                       "ac2b658fb0738c0a54cf0de9263db7b2-aa4b0867-a38833e8")
            };

            RestRequest request = new RestRequest();
            request.AddParameter("domain", ConfigurationManager.AppSettings["domain"], ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxc4df07193b994302b7fc0816a4b6f4a9.mailgun.org>");
            request.AddParameter("to", $"{user.Email}");
            request.AddParameter("subject", $"Registration in DevIncubator");
            request.AddParameter("text", htmlContent);
            request.Method = Method.POST;

            return await Task.Run(() =>
            {
                return client.Execute(request).ToString();
            });
        }

        public async Task MessageToUserAsync(IEnumerable<UserDTO> users, string subject, string html)
        {
            foreach (var user in users)
            {
                await MessageToUserAsync(user, subject, html);
            }
        }
    }
}
