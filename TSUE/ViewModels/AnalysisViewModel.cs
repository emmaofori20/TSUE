using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;

namespace TSUE.ViewModels
{
    public class AnalysisViewModel
    {
        public int Totalprojects { get; set; }
        public int TotalCategories { get; set; }
        public int TotalVisits { get; set; }
        public List<AnalyticTypeResponse> Analysis { get; set; }
    }
}
