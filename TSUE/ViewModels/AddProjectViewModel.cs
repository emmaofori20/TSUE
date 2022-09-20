using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TSUE.ViewModels
{
    public class AddProjectViewModel
    {
        public int ProjectId { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectSummary { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectOverview { get; set; }
        [Required]
        public string StudyTitle { get; set; }
        public DateTime YearOfPublication { get; set; }
        public IFormFile ProjectIcon { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        public string Author { get; set; }
        public List<IFormFile> ProjectFile { get; set; }
        public int ProjectCategoryId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        public SelectList SelectDocumentType { get; set; }
        [Required]
        public List<int> CountryId { get; set; }
        public SelectList SelectCountry { get; set; }
        public int LanguageId { get; set; }
        public SelectList SelectLanguage { get; set; }
    }
}
