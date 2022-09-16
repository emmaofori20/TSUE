using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSUE.Models;
using TSUE.ViewModels;

namespace TSUE.Services.IServices
{
    public interface IProjectService
    {
        public AddProjectViewModel SetProjectForCreate();

        public Project AddProject(AddProjectViewModel model);

        public List<Project> GetAllProject();

        public Project GetProject(int projectId);

        public ProjectCommentViewModel ProjectComments(int ProjectId);

        public void AddProjectComment(ProjectCommentViewModel model);
        public  Task<UpdateProjectViewModel> GetProjectForUpdate(int projectId);
        public  Task<int> UpdateProject(UpdateProjectViewModel model);
        public SelectList GetCategoryList();

    }
}
