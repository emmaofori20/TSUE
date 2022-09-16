using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;
using TSUE.ViewModels;
using TSUE.Models.Data;

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

        public IActionResult FilterProjectsBySpecificParmeters(FilterFormAndProjectViewModel model)
        {
            var allProjects = projectService.GetAllProject();

            if(model.DocumentTypeId != 0 || model.CountryId != 0 ||
                model.LanguageId != 0 || model.StudyTitle != null)
            {

                if (!string.IsNullOrEmpty(model.StudyTitle))
                {
                    var results = allProjects.Where(x => x.DocumentTypeId == model.DocumentTypeId
                                               || x.ProjectLanguages.FirstOrDefault().LanguageId == model.LanguageId
                                               || x.ProjectCountries.FirstOrDefault().CountryId == model.CountryId
                                               || x.StudyTitle.ToLower().Contains(model.StudyTitle.ToLower())).ToList();
                }
                else
                {
                    var results = allProjects.Where(x => x.DocumentTypeId == model.DocumentTypeId
                                               || x.ProjectLanguages.FirstOrDefault().LanguageId == model.LanguageId
                                               || x.ProjectCountries.FirstOrDefault().CountryId == model.CountryId ).ToList();
                }
               


            }

            return PartialView("_AllProjectsPartialView");
        }
        public IActionResult ProjectComments(int ProjectId)
        {
            var res = projectService.ProjectComments(ProjectId);
            return View(res);
        }

        [HttpPost]
        public IActionResult AddComments(ProjectCommentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //projectService.AddProjectComment(model);
                    return RedirectToAction("ProjectComments", "Project", new{ ProjectId = model.ProjectId });

                }
                return RedirectToAction("ProjectComments", "Project", new { ProjectId = model.ProjectId });

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: ProjectController/Details/5
        public ActionResult ViewProject(int ProjectId)
        {
            var res = projectService.GetProject(ProjectId);
            //analyticService.AddMostVisitedProject(res.StudyTitle, res.ProjectId);
            return View(res);
        }

        // GET: ProjectController/Create
        public ActionResult CreatePost()
        {
            var res = projectService.SetProjectParametersToCreateProject();
            return View(res);
        }

        // POST: ProjectController/Create
        [HttpPost]
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
                var message=err.Message;
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
            //work on this
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
            
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
                //work on this
            }
            catch
            {
                return View();
            }
        }
    }
}
