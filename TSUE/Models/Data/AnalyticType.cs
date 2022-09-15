using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class AnalyticType
    {
        public AnalyticType()
        {
            AnalyticTypeResponses = new HashSet<AnalyticTypeResponse>();
        }

        public int AnalyticTypeId { get; set; }
        public string AnalyticTypeName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<AnalyticTypeResponse> AnalyticTypeResponses { get; set; }
    }
}
