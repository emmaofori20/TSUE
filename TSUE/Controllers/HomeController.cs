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
using System.Globalization;
using TSUE.Models.Data;

namespace TSUE.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectService projectService;
        private readonly IDocumentTypeService documentTypeService;
        private readonly BirdTsueDBContext context;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService,
            IDocumentTypeService documentTypeService, BirdTsueDBContext context)
        {
            this.projectService = projectService;
            this.documentTypeService = documentTypeService;
            this.context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var resProject = projectService.GetAllProject();

            var results = new ProjectAndDocumentTypeViewModel()
            {
                Projects = resProject.OrderByDescending(i => i.CreatedOn).Take(6).ToList(),
                documentType = documentTypeService.GetAllDocumentType().Take(4).ToList()
            };
            return View(results);
        }

        public IActionResult getLanguage()
        {
            List<string> cultureList = new List<string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);

                if (!(cultureList.Contains(region.EnglishName)))
                {
                    context.Countries.Add(new Country { CountryName = region.EnglishName });
                    context.SaveChanges();
                    cultureList.Add(region.EnglishName);
                }
            }

            cultureList.Sort();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
