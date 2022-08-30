using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class Category
    {
        public Category()
        {
            ProjectCategories = new HashSet<ProjectCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProjectCategory> ProjectCategories { get; set; }
    }
}
