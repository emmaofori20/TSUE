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
        private static readonly HttpClient client = new HttpClient();

        public async Task AddMostVisitedProject(string ResponseValue, int ProjectId)
        {
            await GetVisitorDetail();
            var AddAnalytics = new AnalyticTypeResponse()
            {
                ResponseValue = ResponseValue,
                CreatedOn = DateTime.Now,
                CreatedBy = "VisitedUser",
                AnalyticTypeId = 1,
                ProjectId = ProjectId
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
                Analysis = birdTsueDBContext.AnalyticTypeResponses.ToList(),
            };

            return AnalysisData;
        }

        public static async Task GetVisitorDetail()
        {
            var getDetails = await client.GetAsync("ipinfo.io?token=852c5133673d5e");
            Console.WriteLine(getDetails);
        }
    }
}
