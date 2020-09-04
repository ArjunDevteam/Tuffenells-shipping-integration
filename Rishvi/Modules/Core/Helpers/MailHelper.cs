using Rishvi.Modules.Core.Models;
using Rishvi.Modules.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

/*
How to use code sample: 
=======================
	new MailHelper()
		.From("test@gmail.com")
		.To("mike.eaves004@gmail.com")
		.Subject("Hello Subject {%WebsiteUrl%}")
		.Body("Test Body {%Name%}")
		.Variables(new Dictionary<string, object>()
		{
			{"Name", "Mike Eaves"}
		}).Send();
*/

namespace Rishvi.Modules.Core.Helpers
{
    public class MailHelper
    {
        private readonly MailSetting _mailSetting;
        private readonly IList<MailAddress> _toAddresses;
        private readonly IList<MailAddress> _ccAddresses;
        private readonly IList<MailAddress> _bccAddresses;
        private string _subject;
        private string _body;

        private IDictionary<string, object> _variables;
        private MailAddress _from;

        public IList<Attachment> Attachments { get; set; }

        public MailHelper(MailSetting mailSetting)
        {
            _mailSetting = mailSetting;
            _toAddresses = new List<MailAddress>();
            _ccAddresses = new List<MailAddress>();
            _bccAddresses = new List<MailAddress>();
            _variables = new Dictionary<string, object>();
        }

        public MailHelper To(string email)
        {
            _toAddresses.Add(new MailAddress(email));

            return this;
        }

        public MailHelper To(string name, string email)
        {
            _toAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Cc(string email)
        {
            _ccAddresses.Add(new MailAddress(email));

            return this;
        }

        public MailHelper Cc(string name, string email)
        {
            _ccAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Bcc(string email)
        {
            _bccAddresses.Add(new MailAddress(email));

            return this;
        }

        public MailHelper Bcc(string name, string email)
        {
            _bccAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Subject(string subject)
        {
            _subject = subject;

            return this;
        }

        public MailHelper Body(string body)
        {
            _body = body;

            return this;
        }

        public MailHelper Variables(IDictionary<string, object> bodyValues)
        {
            _variables = bodyValues;

            return this;
        }

        public MailHelper AddVariables(string key, object value)
        {
            _variables.Add(key, value);

            return this;
        }

        public MailHelper From(string email)
        {
            _from = new MailAddress(email);

            return this;
        }

        public MailHelper From(string name, string email)
        {
            _from = new MailAddress(email, name);

            return this;
        }

        //public string ConcatenateEmails(List<EmailClass> emails)
        //{
        //    return emails != null && emails.Count > 0 ? emails.Select(s => s.Email).ToList().StringJoin(',') : string.Empty;
        //}

        public Result Send()
        {
            try
            {
                var message = new MailMessage();

                foreach (var toAddress in _toAddresses)
                    message.To.Add(toAddress);

                foreach (var ccAddress in _ccAddresses)
                    message.CC.Add(ccAddress);

                foreach (var bccAddress in _bccAddresses)
                    message.Bcc.Add(bccAddress);

                message.Subject = PrepareSubjectWithVariables();
                message.From = PrepareFrom();
                message.Body = PrepareBodyWithVariables();
                message.IsBodyHtml = true;

                if (Attachments != null)
                {
                    if (Attachments.Any())
                    {
                        foreach (var attachment in Attachments)
                            message.Attachments.Add(attachment);
                    }
                }

                GetSmtpClient()
                    .Send(message);

                return new Result().SetSuccess();
            }
            catch (Exception ex)
            {
                return new Result().SetError(ex.Message);
            }
        }

        private SmtpClient GetSmtpClient()
        {
            var smtpClient = new SmtpClient(_mailSetting.Host, _mailSetting.Port)
            {
                EnableSsl = _mailSetting.EnableSsl
            };

            if (_mailSetting.IsAuthentication)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_mailSetting.Username, _mailSetting.Password);
            }

            return smtpClient;
        }

        public MailAddress PrepareFrom()
        {
            if (_from != null)
                return _from;

            return !string.IsNullOrEmpty(_mailSetting.FromName)
                ? new MailAddress(_mailSetting.FromEmail, _mailSetting.FromName)
                : new MailAddress(_mailSetting.FromEmail);
        }

        public string PrepareSubjectWithVariables()
        {
            return !_variables.Any() ? _subject : ReplaceWithVariable(_subject);
        }

        public string PrepareBodyWithVariables()
        {
            return !_variables.Any() ? _body : ReplaceWithVariable(_body);
        }

        private string ReplaceWithVariable(string str)
        {
            return _variables.Aggregate(str,
                (current, value) => current.Replace("{%" + value.Key + "%}", Convert.ToString(value.Value)));
        }
    }
}