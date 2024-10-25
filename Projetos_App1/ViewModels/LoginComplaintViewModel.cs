using System.ComponentModel.DataAnnotations;

namespace Projetos_App1.ViewModels
{
    public class LoginComplaintViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string ComplaintId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string PassWord { get; set; } = null!;
    }
}
