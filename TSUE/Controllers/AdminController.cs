using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;

namespace TSUE.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAnalyticService analyticService;

        public AdminController(IAnalyticService analyticService)
        {
            this.analyticService = analyticService;
        }
        public IActionResult Index()
        {
            var response = analyticService.GetAnalysisData();

            List<int> repartitions = new List<int>();
            var TopProjects = response.Analysis.Select(x => x.ProjectId ).Distinct();      

            foreach (var item in TopProjects )
            {
                repartitions.Add(response.Analysis.Count(x => x.ProjectId == item));
            }

            var rep = repartitions;
            ViewBag.Projects = TopProjects;
            ViewBag.Rep = repartitions.ToList();
            return View(response);
        }
    }
}
