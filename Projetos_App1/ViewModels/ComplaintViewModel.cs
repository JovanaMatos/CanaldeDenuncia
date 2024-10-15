using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Projetos_App1.Models;
using Projetos_App1.Models.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Projetos_App1.ViewModels
{
    public class ComplaintViewModel
    {




        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; } = null!;

        public IList<IFormFile> _files { get; set; } = new List<IFormFile>();



        [EmailAddress(ErrorMessage = "Fomato de email, invalido!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Phone]
        [Display(Name = "Telemovel")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{9,15}$", ErrorMessage = "Por favor digite um numero valido!")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "O Assunto deve ter no minimo {1} e no máximo {2} caracteres.")]
        public string ComplaintSubject { get; set; } = null!;

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        public string ComplaintDescription { get; set; } = null!;

        public int? CompanyRelationId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int categoryid { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int companyid { get; set; }

 


        public List<Category> listCategory { get; set; } = new List<Category> ();

        public List<Company> listCompany { get; set; } = new List<Company> ();

        public List<CompanyRelation> listRelation { get; set; } = new List<CompanyRelation>();

        


   









    }
}

