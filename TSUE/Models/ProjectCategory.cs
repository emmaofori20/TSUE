using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class ProjectCategory
    {
        public int ProjectCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Project Project { get; set; }
    }
}
