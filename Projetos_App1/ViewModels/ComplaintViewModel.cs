﻿using Microsoft.AspNetCore.Mvc;
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
        public int? companyRelationid { get; set; }


        public IEnumerable<Category> listCategory { get; set; }

        public IEnumerable<Company> listCompany { get; set; }

        public IEnumerable<CompanyRelation> listRelation { get; set; }

        public IEnumerable<AttachedFile> attachedFiles { get; set; }



        public Complaint ChangeTocomplaint(ComplaintViewModel complaintViewModel)
        {
            Complaint complaint = new Complaint()
            {
                ComplaintSubject = complaintViewModel.ComplaintSubject,
                ComplaintDescription = complaintViewModel.ComplaintDescription,
                CompanyRelationId = complaintViewModel.CompanyRelationId

            };

            return complaint;

        }

        public Whistleblowing ChangeToWhistleblowing(ComplaintViewModel complaintViewModel)
        {
            Whistleblowing whistleblowing = new Whistleblowing()
            {

                Name = complaintViewModel.Name,
                Email = complaintViewModel.Email,
                PhoneNumber = complaintViewModel.PhoneNumber

            };

            return whistleblowing;
        }

        public List<AttachedFile> UploadImg(IList<IFormFile> attachedFileVM)
        {
            List<AttachedFile> listFiles = new List<AttachedFile>();

            foreach (var file in attachedFileVM)
            {
                // para copiar o conteúdo do arquivo
                using (var ms = new MemoryStream())
                {
                    file.OpenReadStream().CopyTo(ms);

                    AttachedFile attachedFile = new AttachedFile()
                    {
                        FilesName = file.FileName,
                        ImgSize = file.Length,
                        Image = ms.ToArray(),
                        FileType = file.ContentType,
                        SubmissionDate = DateTime.Now
                    };

                    listFiles.Add(attachedFile);
                }
            }
          
            return listFiles;
        }








    }
}

