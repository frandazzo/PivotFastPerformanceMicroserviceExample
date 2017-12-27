using System;
using System.Collections.Generic;

using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;
using System.Net.Mail;

namespace UilDBIscritti.Handlers.SecurityProviders
{
    public class MailProvider : IMailer

    {
        private string _smtpServer = "";
        private string _smtpAccount = "";
        private string _smtpPassword = "";
        private string _from = "";
        private bool _smtpEnableSSL;

        public MailProvider(string smtpServer, string smtpAccount, string smtpPassword, bool enableSSl, string from)
        {
            _smtpAccount = smtpAccount;
            _smtpEnableSSL = enableSSl;
            _smtpPassword = smtpPassword;
            _smtpServer = smtpServer;
            _from = from;
        }

        public void SendMail(string to, string subject, string body)
        {
           
            MailMessage mail = new MailMessage();
            mail.To.Add(to);

            mail.From = new MailAddress(_from);
            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _smtpServer;
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
                 (_smtpAccount, _smtpPassword);
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = false;
            smtp.Send(mail);
         
           

        }



        public void SendMail(string[] to, string subject, string body)
        {

            MailMessage mail = new MailMessage();

            foreach (string item in to)
            {
                 mail.To.Add(item);
            }
           

            mail.From = new MailAddress(_from);
            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _smtpServer;
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
                 (_smtpAccount, _smtpPassword);
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
       
    }
}
