using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Projetos_App1.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        //public Task SendEmail(string email, string subject, string message)
        //{
        //    try
        //    {
        //        Execute(email, subject, message).Wait();
        //        return Task.FromResult(0);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public async Task SendEmail(string email, string subject, string message)
        {

            string userName = _configuration.GetValue<string>("EmailSettings:UserName");
            try
            {
                
                string toEmail = email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, "Jose Carlos Macoratti")
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_configuration.GetValue<string>("EmailSettings:CcEmail")));

                mail.Subject =  subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //PrimaryDomainPrimaryPort

                using (SmtpClient smtp = new SmtpClient(_configuration.GetValue<string>("EmailSettings:PrimaryDomain"), _configuration.GetValue<int>("EmailSettings:PrimaryPort"))) 
                {
                    smtp.Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailSettings:UserName"), _configuration.GetValue<string>("EmailSettings:UsernamePassword"));
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }


    }
}




//private readonly IConfiguration _configuration;

//public Email(IConfiguration configuration)
//{
//    _configuration = configuration;
//}

//public bool SendEmail(string email, string subject, string menssage)
//{
//    try
//    {
//        string host = _configuration.GetValue<string>("SMTP:Host");
//        string name = _configuration.GetValue<string>("SMTP:Name");
//        string userName = _configuration.GetValue<string>("SMTP:UserName");
//        string passWord = _configuration.GetValue<string>("SMTP:PassWord");
//        int port = _configuration.GetValue<int>("SMTP:Port");

//        MailMessage mail = new MailMessage()
//        {
//            From = new MailAddress(userName, name)

//        };

//        mail.To.Add(email);
//        mail.Subject = subject;
//        mail.Body = menssage;
//        mail.IsBodyHtml = true;
//        mail.Priority = MailPriority.High;

//        using (SmtpClient smtp = new SmtpClient(host, port) )
//        {
//            smtp.Credentials = new NetworkCredential(userName, passWord);
//            smtp.EnableSsl = true;

//            smtp.Send(mail);
//            return true;
//        }

//    }
//    catch (Exception ex) { 

//        return false;
//    }
//}