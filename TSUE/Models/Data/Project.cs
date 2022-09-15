using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class Project
    {
        public Project()
        {
            ProjectComments = new HashSet<ProjectComment>();
            ProjectCountries = new HashSet<ProjectCountry>();
            ProjectDocuments = new HashSet<ProjectDocument>();
            ProjectLanguages = new HashSet<ProjectLanguage>();
            ProjectSectors = new HashSet<ProjectSector>();
        }

        public int ProjectId { get; set; }
        public string Authors { get; set; }
        public string Overview { get; set; }
        public string StudyTitle { get; set; }
        public byte[] ProjectIcon { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime? YearOfPublication { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<ProjectComment> ProjectComments { get; set; }
        public virtual ICollection<ProjectCountry> ProjectCountries { get; set; }
        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        public virtual ICollection<ProjectLanguage> ProjectLanguages { get; set; }
        public virtual ICollection<ProjectSector> ProjectSectors { get; set; }
    }
}
