using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAnalyticService analyticService;
        private readonly IProjectService projectService;

        public AdminController(IAnalyticService analyticService, IProjectService projectService)
        {
            this.analyticService = analyticService;
            this.projectService = projectService;
        }
        public IActionResult Index()
        {
            var response = analyticService.GetAnalysisData();

            List<MostvistedProjectGraph> repartitions = new List<MostvistedProjectGraph>();
            var AllProjects = response.Analysis.Where(x => x.AnalyticTypeId == 1).ToList();
            var TopProjects = AllProjects.Select(x => x.ResponseValue).Distinct();
            foreach (var item in TopProjects)
            {
                repartitions.Add(
                    new MostvistedProjectGraph { 
                        NumberOfVisits = response.Analysis.Count(x => x.ResponseValue == item),
                        projectName = projectService.GetProject((int)item).StudyTitle}
                    );
            }
            ViewBag.Rep = repartitions.ToList().OrderByDescending(x=>x.NumberOfVisits).Take(10);
            return View(response);
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult AddNewUser(AddUserViewModel model)    
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
