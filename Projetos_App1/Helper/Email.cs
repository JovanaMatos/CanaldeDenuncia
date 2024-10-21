using Projetos_App1.Helper;
using System.Net.Mail;
using System.Net;

public class Email : IEmail
{
    private readonly IConfiguration _configuration;

    public Email(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(string email, string subject, string message)
    {
        string userName = _configuration.GetValue<string>("EmailSettings:UsernameEmail");
        string password = _configuration.GetValue<string>("EmailSettings:UsernamePassword");
        string name = _configuration.GetValue<string>("EmailSettings:Name");

        try
        {
            string toEmail = email;

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(userName, name) 
            };

            mail.To.Add(new MailAddress(toEmail));
            mail.CC.Add(new MailAddress(_configuration.GetValue<string>("EmailSettings:CcEmail")));

            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

         
            using (SmtpClient smtp = new SmtpClient(
                _configuration.GetValue<string>("EmailSettings:PrimaryDomain"),
                _configuration.GetValue<int>("EmailSettings:PrimaryPort")))
            {
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.EnableSsl = true;  //  conexão segura 
                await smtp.SendMailAsync(mail);
            }
        }
        catch (SmtpException ex)
        {
            // Log da exceção SMTP para facilitar a depuração
            Console.WriteLine($"Erro SMTP: {ex.StatusCode} - {ex.Message}");
            throw;
        }
        
    
}
}
