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
        [Required]
        public string ProjectTitle { get; set; }
        [Required]
        public string ProjectSummary { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
        public IFormFile ProjectIcon { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string Author { get; set; }
        public IFormFile ProjectFile { get; set; }
        public int ProjectCategoryId { get; set; }
        public SelectList selectCategory { get; set; }
    }
}
