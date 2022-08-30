using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectCategories = new HashSet<ProjectCategory>();
            ProjectComments = new HashSet<ProjectComment>();
            ProjectFiles = new HashSet<ProjectFile>();
        }

        public int ProjectId { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectSummary { get; set; }
        public string ProjectDescription { get; set; }
        public byte[] ProjectIcon { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string HyperLink { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Author { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ProjectCategory> ProjectCategories { get; set; }
        public virtual ICollection<ProjectComment> ProjectComments { get; set; }
        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }
    }
}
