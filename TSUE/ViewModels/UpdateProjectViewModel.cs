using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TSUE.Models.Data;

namespace TSUE.ViewModels
{
    public class UpdateProjectViewModel
    {
         public int ProjectId { get; set; }
        public string ProjectOverview { get; set; }
        [Required]
        public string StudyTitle { get; set; }
        public DateTime? YearOfPublication { get; set; }
        public IFormFile ProjectIcon { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        public string Authors { get; set; }
 
        public int ProjectCategoryId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        public SelectList SelectDocumentType { get; set; }
        [Required]
        public List<ProjectCountry> ProjectCountryIds { get; set; }
        public List<int> CountryId{ get; set; }
        public SelectList SelectCountry { get; set; }
        public int LanguageId { get; set; }
        public SelectList SelectLanguage { get; set; }
   
        public byte[] ProjectIconByte { get; set; }

        public List<ProjectDocumentForUpdate> ProjectDocumentsForUpdate { get; set; }

    }

    public class ProjectDocumentForUpdate
    {
        public int ProjectDocumentId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectDocumentName { get; set; }
        public byte[] ProjectDocumentByte { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public IFormFile ProjectDocumentFile { get; set; }

    }
}