using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;

namespace TSUE.Services.IServices
{
    public interface ICategoryService
    {
        public List<ProjectCategory> GetAllCategories(); 
    }
}
