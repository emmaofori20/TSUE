using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.ViewModels;

namespace TSUE.Services.IServices
{
    public interface IAnalyticService
    {
        public void AddMostVisitedProject(string ResponseValue, int ProjectId);

        public AnalysisViewModel GetAnalysisData();
    }
}
