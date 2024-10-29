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

    public async Task SendEmail(string email, string subject, string message)//enviar emal
    {
        string userName = _configuration.GetValue<string>("EmailSettings:UsernameEmail");//email cadastrado
        string password = _configuration.GetValue<string>("EmailSettings:UsernamePassword");//senha cadastrado
        string name = _configuration.GetValue<string>("EmailSettings:Name");//nome cadastrado

        try
        {
        

            MailMessage mymail = new MailMessage()
            {
                From = new MailAddress(userName, name) // criando formato de email
            };

            mymail.To.Add(new MailAddress(email));//add email do denunciante
            mymail.CC.Add(new MailAddress(_configuration.GetValue<string>("EmailSettings:CcEmail"))); //copia para um outro email (opcional)

            mymail.Subject = subject;// add assunto
            mymail.Body = message;// mensagem
            mymail.IsBodyHtml = true;
            mymail.Priority = MailPriority.High;// importante

            //protocolo SMTP (Simple Mail Transfer Protocol). para envio de e-mails entre servidores e clientes de e-mail.
            using (SmtpClient smtp = new SmtpClient(
                _configuration.GetValue<string>("EmailSettings:PrimaryDomain"),//dominio
                _configuration.GetValue<int>("EmailSettings:PrimaryPort")))//porta
            {
                smtp.Credentials = new NetworkCredential(userName, password);//email e senha de quem vai enviar
                smtp.EnableSsl = true;  //  conexão segura 
                await smtp.SendMailAsync(mymail);
            }
        }
        catch (SmtpException ex)
        {
            //futuro implementar erro
            Console.WriteLine($"Erro SMTP: {ex.StatusCode} - {ex.Message}");
            throw;
        }
        
    
}
}
