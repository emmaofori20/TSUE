using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.ViewModels;

namespace TSUE.Services.IServices
{
    public interface IProjectService
    {
        //public AddProjectViewModel SetProjectForCreate();
        public AddProjectViewModel SetProjectParametersToCreateProject();
        public Models.Data.Project AddProject(AddProjectViewModel model);

        public List<Models.Data.Project> GetAllProject();

        public Project GetProject(int projectId);

        public Task<UpdateProjectViewModel> GetProjectForUpdate(int projectId);
        public Task<int> UpdateProject(UpdateProjectViewModel model);

        //public ProjectCommentViewModel ProjectComments(int ProjectId);

        //public void AddProjectComment(ProjectCommentViewModel model);
    }
}
