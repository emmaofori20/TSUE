using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class ProjectComment
    {
        public int ProjectCommentId { get; set; }
        public int CommentId { get; set; }
        public int ProjectId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Project Project { get; set; }
    }
}
