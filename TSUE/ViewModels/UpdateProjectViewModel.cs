using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TSUE.ViewModels
{
    public class UpdateProjectViewModel
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
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string Author { get; set; }

        public byte[] ProjectIconByte { get; set; }
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }

        public List<ProjectFileForUpdate> ProjectFiles { get; set; }

    }

    public class ProjectFileForUpdate
    {
        public int ProjectFileId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectFileName { get; set; }
        public byte[] ProjectFileByte { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public IFormFile ProjectFile1 { get; set; }

    }
}
