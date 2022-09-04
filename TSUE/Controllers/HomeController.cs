using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectService projectService;
        private readonly ICategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService,
            ICategoryService categoryService)
        {
            this.projectService = projectService;
            this.categoryService = categoryService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var resProject = projectService.GetAllProject();

            var results = new ProjectAndCategoryViewModel()
            {
                Projects = resProject.OrderByDescending(i => i.CreatedOn).Take(6).ToList(),
                _Categories = categoryService.GetAllCategories().Take(4).ToList()
            };
            return View(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
