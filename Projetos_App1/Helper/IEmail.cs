namespace Projetos_App1.Helper
{
    public interface IEmail
    {
        Task SendEmail(string email, string subject, string menssage );
    }
}
