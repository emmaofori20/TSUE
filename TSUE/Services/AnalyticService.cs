using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Services
{
    public class AnalyticService: IAnalyticService
    {
        private readonly TSUEProjectDbContext _context;

        public AnalyticService(TSUEProjectDbContext context)
        {
            _context = context;
        }

        public void AddMostVisitedProject(string ResponseValue, int ProjectId)
        {
            var AddAnalytics = new AnalyticTypeResponse()
            {
                ResponseValue = ResponseValue,
                CreatedAt = DateTime.Now,
                CreatedBy = "VisitedUser",
                AnalyticTypeId = 101,
                ProjectId = ProjectId
            };

            _context.AnalyticTypeResponses.Add(AddAnalytics);
            _context.SaveChanges();

        }

        public AnalysisViewModel GetAnalysisData()
        {
            var AnalysisData = new AnalysisViewModel()
            {
                TotalCategories = _context.Categories.Where(x => x.IsDeleted == false).Count(),
                Totalprojects = _context.Projects.Where(x=>x.IsDeleted == false).Count(),
                TotalVisits = _context.AnalyticTypeResponses.Count(),
                Analysis = _context.AnalyticTypeResponses.ToList(),
            };

            return AnalysisData;
        }
    }
}
