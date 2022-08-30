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
        public AddProjectViewModel SetProjectForCreate();

        public Project AddProject(AddProjectViewModel model);

        public List<Project> GetAllProject();

        public Project GetProject(int projectId);
    }
}
