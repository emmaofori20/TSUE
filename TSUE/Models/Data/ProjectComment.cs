using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class ProjectComment
    {
        public int ProjectCommentId { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int ProjectId { get; set; }
        public int? Rating { get; set; }

        public virtual Project Project { get; set; }
    }
}
