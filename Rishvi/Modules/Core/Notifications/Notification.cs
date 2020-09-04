using Microsoft.Extensions.Configuration;
using Rishvi.Modules.Core;
using Rishvi.Modules.Core.Helpers;
using Rishvi.Modules.Core.Models;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Aim.Web.Modules.Core.Notifications
{
    public class Notification
    {
        private const string _countryCode = "962";
        protected MailHelper MailHelper;
        private MailSetting _mailSetting;
        private IConfiguration _configuration;
        private readonly SiteSetting _siteSetting;

        public Notification(IConfiguration configuration)
        {
            var mailSettings = new MailSetting();
            _configuration = configuration;

            mailSettings.Enabled = Convert.ToBoolean(_configuration.GetSection("MailSetting").GetSection("Enabled").Value);
            mailSettings.FromName = _configuration.GetSection("MailSetting").GetSection("FromName").Value;
            mailSettings.FromEmail = _configuration.GetSection("MailSetting").GetSection("FromEmail").Value;
            mailSettings.Host = _configuration.GetSection("MailSetting").GetSection("Host").Value;
            mailSettings.Port = Convert.ToInt32(_configuration.GetSection("MailSetting").GetSection("Port").Value);
            mailSettings.EnableSsl = Convert.ToBoolean(_configuration.GetSection("MailSetting").GetSection("EnableSsl").Value);
            mailSettings.IsAuthentication = Convert.ToBoolean(_configuration.GetSection("MailSetting").GetSection("IsAuthentication").Value);
            mailSettings.Username = _configuration.GetSection("MailSetting").GetSection("Username").Value;
            mailSettings.Password = _configuration.GetSection("MailSetting").GetSection("Password").Value;

            MailHelper = new MailHelper(mailSettings);

            _siteSetting = new SiteSetting()
            {
                SiteTitle = _configuration.GetSection("SiteSetting").GetSection("SiteTitle").Value,
                WebsiteUrl = _configuration.GetSection("SiteSetting").GetSection("WebsiteUrl").Value
            };

            _mailSetting = mailSettings;
        }

        public Result Send()
        {
            return MailHelper.Send();
        }
        public Result SendEmailToUser()
        {

            //_mailHelper.To(restaurant.Name, restaurant.Email)
            //.Subject("Registraion completed")
            //.Body($"Dear {restaurant.Name}, \n \n \t your registration reuquest is completed, your request is under review. feel free to setup your menu and you will be able to download qr code after approval of the request \n \n System generated notification");
            return MailHelper.Send();
        }
    }
}
