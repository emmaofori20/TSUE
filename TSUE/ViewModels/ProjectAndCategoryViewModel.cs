using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;

namespace TSUE.ViewModels
{
    public class ProjectAndCategoryViewModel
    {
       public List<Project> Projects { get; set; }

       public List<Category> _Categories { get; set; }
    }
}
