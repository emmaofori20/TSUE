using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class AnalyticTypeResponse
    {
        public int AnalyticTypeResponseId { get; set; }
        public int AnalyticTypeId { get; set; }
        public string ResponseValue { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int? ProjectId { get; set; }

        public virtual AnalyticType AnalyticType { get; set; }
    }
}
