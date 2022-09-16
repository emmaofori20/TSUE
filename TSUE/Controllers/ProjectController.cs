using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;
using TSUE.ViewModels;
using TSUE.Models.Data;
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
            var res = projectService.GetAllProject().OrderByDescending(x=>x.CreatedOn);

            if (!string.IsNullOrEmpty(searchText))
            {
               var results = res.Where(x => x.StudyTitle.ToLower().Contains(searchText.ToLower()) 
                            || x.Overview.ToLower().Contains(searchText.ToLower())).ToList();

                return View(results);
            }
            return View(res.ToList());
        }

        public IActionResult ProjectComments(int ProjectId)
        {
            //var res = projectService.ProjectComments(ProjectId);
            return View();
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
            analyticService.AddMostVisitedProject(res.ProjectTitle, res.ProjectId);
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
        public async Task<IActionResult> UpdateProject(int ProjectId)
        {
            ViewBag.SelectCountry = projectService.SetProjectParametersToCreateProject().SelectCountry;
            ViewBag.SelectDocumentType = projectService.SetProjectParametersToCreateProject().SelectDocumentType;
            ViewBag.SelectLanguage = projectService.SetProjectParametersToCreateProject().SelectLanguage;
            var project = await projectService.GetProjectForUpdate(ProjectId);
            return View(project);
            //work on this
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProject(UpdateProjectViewModel model,int ProjectId )
        {
            try
            {

                var resultId = await projectService.UpdateProject(model);


                return RedirectToAction(nameof(Index), new { Id = resultId });
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
