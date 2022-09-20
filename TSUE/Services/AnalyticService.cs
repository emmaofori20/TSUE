using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TSUE.Models.Data;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Services
{
    public class AnalyticService: IAnalyticService
    {
        private readonly BirdTsueDBContext birdTsueDBContext;

        public AnalyticService(BirdTsueDBContext birdTsueDBContext)
        {
            this.birdTsueDBContext = birdTsueDBContext;
        }

        public async Task AddMostVisitedProject(MostVistedPageViewModel model)
        {
            var AddAnalytics = new AnalyticTypeResponse()
            {
                CreatedOn = DateTime.Now,
                CreatedBy = "VisitedUser",
                AnalyticTypeId = 1,
                ResponseValue = model.ProjectId,
                Country = model.Country,
                StateOrCity = model.StateOrCity
            };

            birdTsueDBContext.AnalyticTypeResponses.Add(AddAnalytics);
            await birdTsueDBContext.SaveChangesAsync();

        }

        public AnalysisViewModel GetAnalysisData()
        {
            var AnalysisData = new AnalysisViewModel()
            {
                TotalDocumentTypes = birdTsueDBContext.DocumentTypes.Where(x => x.IsDeleted == false).Count(),
                Totalprojects = birdTsueDBContext.Projects.Where(x=>x.IsDeleted == false).Count(),
                TotalVisits = birdTsueDBContext.AnalyticTypeResponses.Count(),
                MostVistedCountry = birdTsueDBContext.AnalyticTypeResponses.Where(x=>x.AnalyticTypeId ==1)
                                     .GroupBy(i => i.Country)
                                     .OrderByDescending(gp => gp.Count())
                                     .Take(1)
                                     .Select(x => x.Key)
                                     .FirstOrDefault(),
            Analysis = birdTsueDBContext.AnalyticTypeResponses.ToList(),
            };

            return AnalysisData;
        }

    }
}
