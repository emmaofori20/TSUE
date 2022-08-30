using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class Comment
    {
        public Comment()
        {
            ProjectComments = new HashSet<ProjectComment>();
        }

        public int CommentId { get; set; }
        public string Comment1 { get; set; }
        public string CommenterName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ProjectComment> ProjectComments { get; set; }
    }
}
