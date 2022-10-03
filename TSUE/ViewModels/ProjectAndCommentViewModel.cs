using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models.Data;

namespace TSUE.ViewModels
{
    public class ProjectAndCommentViewModel
    {
        public int ProjectId { get; set; }
        public Project project { get; set; }
        public List<ProjectComment> ProjectComment { get; set; }
        public NewProjectComment AddComment { get; set; }
    }

    public class NewProjectComment
    {
        public int CommentId { get; set; }
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Message { get; set; }
        public int? Rate { get; set; }
    }
}
