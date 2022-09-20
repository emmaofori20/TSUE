using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class AnalyticTypeResponse
    {
        public int AnalyticTypeResponseId { get; set; }
        public int AnalyticTypeId { get; set; }
        public int? ResponseValue { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Country { get; set; }
        public string StateOrCity { get; set; }

        public virtual AnalyticType AnalyticType { get; set; }
    }
}
