using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models.Data;
using TSUE.ViewModels;

namespace TSUE.Services.IServices
{
    public interface IProjectService
    {
        //public AddProjectViewModel SetProjectForCreate();
        public AddProjectViewModel SetProjectParametersToCreateProject();
        public Project AddProject(AddProjectViewModel model);

        public List<Project> GetAllProject();

        public Project GetProject(int projectId);

        public ProjectCommentViewModel ProjectComments(int ProjectId);

        //public void AddProjectComment(ProjectCommentViewModel model);
    }
}
