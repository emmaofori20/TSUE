using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;
using TSUE.ViewModels;
using TSUE.Models.Data;
using Microsoft.AspNetCore.Authorization;
using TSUE.Models;

namespace TSUE.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IAnalyticService analyticService;

        public ProjectController(IProjectService projectService, IAnalyticService analyticService)
        {
            this.projectService = projectService;
            this.analyticService = analyticService;
        }
        // GET: ProjectController
        public ActionResult Index(string? searchText)
        {
            var filterformAndProject = new FilterFormAndProjectViewModel
            {
                projects = projectService.GetAllProject().OrderByDescending(x => x.CreatedOn).ToList(),
                SelectCountry = projectService.SetProjectParametersToCreateProject().SelectCountry,
                SelectDocumentType = projectService.SetProjectParametersToCreateProject().SelectDocumentType,
                SelectLanguage = projectService.SetProjectParametersToCreateProject().SelectLanguage,
            };

            if (!string.IsNullOrEmpty(searchText))
            {
                var results = filterformAndProject.projects.Where(x => x.StudyTitle.ToLower().Contains(searchText.ToLower())
                             || x.Overview.ToLower().Contains(searchText.ToLower())).ToList();

                return View(results);
            }
            return View(filterformAndProject);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult FilterProjectsBySpecificParmeters(FilterFormAndProjectViewModel model)
        {
            var allProjects = projectService.GetAllProject();

            var filterformAndProject = new FilterFormAndProjectViewModel
            {
                projects = allProjects,
                SelectCountry = projectService.SetProjectParametersToCreateProject().SelectCountry,
                SelectDocumentType = projectService.SetProjectParametersToCreateProject().SelectDocumentType,
                SelectLanguage = projectService.SetProjectParametersToCreateProject().SelectLanguage,
            };

            if (model.DocumentTypeId != 0 || model.CountryId != 0 ||
                model.LanguageId != 0 || model.StudyTitle != null)
            {

                if (!string.IsNullOrEmpty(model.StudyTitle))
                {
                    var results = allProjects.FindAll(x => x.DocumentTypeId == model.DocumentTypeId
                                               || x.ProjectLanguages.FirstOrDefault().LanguageId == model.LanguageId
                                               || x.ProjectCountries.FirstOrDefault().CountryId == model.CountryId
                                               || x.StudyTitle.ToLower().Contains(model.StudyTitle.ToLower())).ToList();
                    filterformAndProject.projects = results;
                    return PartialView("_AllProjectsPartialView", filterformAndProject);

                }
                else
                {
                    var results = allProjects.FindAll(x => (model.DocumentTypeId == 0? true: x.DocumentTypeId == model.DocumentTypeId )
                                               && (model.LanguageId == 0? true: x.ProjectLanguages.FirstOrDefault().LanguageId == model.LanguageId)
                                               && (model.CountryId == 0?true: x.ProjectCountries.FirstOrDefault().CountryId == model.CountryId)).ToList();
                    filterformAndProject.projects = results;
                    return PartialView("_AllProjectsPartialView", filterformAndProject);

                }

            }


            return PartialView("_AllProjectsPartialView", filterformAndProject);
        }
        public IActionResult DownloadProjectDocument(int DocumentId)
        {
            var result = projectService.GetProjectDocument(DocumentId);

            var filedetails = result.DocumentFile;

            return File(filedetails, "application/pdf");
        }
        [HttpPost]
        public IActionResult AddComments(ProjectAndCommentViewModel model)
        {
            try
            {
                
                    if (ModelState.IsValid)
                    {
                        projectService.AddProjectComment(model);
                        return Json(model);
                        //return RedirectToAction("ViewProject", "Project", new { ProjectId = model.ProjectId });

                    }
                    var res = projectService.GetProject(model.ProjectId);
                    model.project = res;
                    model.ProjectComment = res.ProjectComments.ToList();
                    model.ProjectId = res.ProjectId;
                    return View("ViewProject", model);
                


            }
            catch (Exception err)
            {
                var ErrorMessage = new ErrorViewModel()
                {
                    RequestId = err.Message
                };
                return View("Error", ErrorMessage);
            }
        }

        // GET: ProjectController/Details/5
        [HttpGet]
        public IActionResult ViewProject(int ProjectId)
        {

            var res = projectService.GetProject(ProjectId);
            var projectAndComment = new ProjectAndCommentViewModel
            {
                project = res,
                ProjectComment = res.ProjectComments.OrderByDescending(x=>x.CreatedOn).ToList(),
                ProjectId = res.ProjectId
            };
            return View(projectAndComment);
        }

        public void MostVisitedProject(MostVistedPageViewModel model)
        {
            analyticService.AddMostVisitedProject(model);
        }

        // GET: ProjectController/Create
        public ActionResult CreatePost()
        {
            var res = projectService.SetProjectParametersToCreateProject();
            return View(res);
        }

        // POST: ProjectController/Create
        [HttpPost]
        [Authorize]
        public ActionResult CreatePost(AddProjectViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var res = projectService.AddProject(model);
                    return RedirectToAction("Index", "Project");

                }
                model.SelectCountry = projectService.SetProjectParametersToCreateProject().SelectCountry;
                model.SelectDocumentType = projectService.SetProjectParametersToCreateProject().SelectDocumentType;
                model.SelectLanguage = projectService.SetProjectParametersToCreateProject().SelectLanguage;
                return View(model);
            }
            catch(Exception err)
            {
                var ErrorMessage = new ErrorViewModel()
                {
                    RequestId = err.Message
                };
                return View("Error", ErrorMessage);
            }
        }

        // GET: ProjectController/Edit/5
        public async Task<IActionResult> UpdateProject(int ProjectId)
        {
            try
            {
                ViewBag.SelectCountry = projectService.SetProjectParametersToCreateProject().SelectCountry;
                ViewBag.SelectDocumentType = projectService.SetProjectParametersToCreateProject().SelectDocumentType;
                ViewBag.SelectLanguage = projectService.SetProjectParametersToCreateProject().SelectLanguage;

                var project = await projectService.GetProjectForUpdate(ProjectId);

                return View(project);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    RequestId = ex.Message
                };

                return View("Error", errorViewModel);
            }
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProject(UpdateProjectViewModel model)
        {
            try
            {
                var projectId = await projectService.UpdateProject(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    RequestId = ex.Message
                };

                return View("Error", errorViewModel);
            }
        }

        public IActionResult DeleteProject(int ProjectId) 
        {
            try
            {
                projectService.DeleteProject(ProjectId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    RequestId = ex.Message
                };

                return View("Error", errorViewModel);
            }

        }

    }
}
