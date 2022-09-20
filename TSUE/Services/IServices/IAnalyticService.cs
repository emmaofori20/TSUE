using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.ViewModels;

namespace TSUE.Services.IServices
{
    public interface IAnalyticService
    {
        public Task AddMostVisitedProject(MostVistedPageViewModel model);

        public AnalysisViewModel GetAnalysisData();
    }
}
