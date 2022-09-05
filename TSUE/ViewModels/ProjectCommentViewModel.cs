using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;

namespace TSUE.ViewModels
{
    public class ProjectCommentViewModel
    {
        public int ProjectId { get; set; }
        public List<ProjectComment> ProjectComment { get; set; }

        public NewProjectComment AddComment { get; set; }
    }

    public class NewProjectComment
    {
        public int CommentId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
    }
}
