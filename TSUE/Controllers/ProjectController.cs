using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        // GET: ProjectController
        public ActionResult Index(string? searchText)
        {
            var res = projectService.GetAllProject().OrderByDescending(x=>x.CreatedOn);

            if (!string.IsNullOrEmpty(searchText))
            {
               var results = res.Where(x => x.ProjectTitle.ToLower().Contains(searchText.ToLower()) 
                            || x.ProjectSummary.ToLower().Contains(searchText.ToLower())).ToList();

                return View(results);
            }
            return View(res.ToList());
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
                    projectService.AddProjectComment(model);
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
            return View(res);
        }

        // GET: ProjectController/Create
        public ActionResult CreatePost()
        {
            var res = projectService.SetProjectForCreate();
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

                }
                return RedirectToAction("Index","Home");
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
            }
            catch
            {
                return View();
            }
        }
    }
}
