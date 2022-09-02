using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Services.IServices;

namespace TSUE.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly TSUEProjectDbContext _context;

        public CategoryService(TSUEProjectDbContext context)
        {
            this._context = context;
        }

        public List<ProjectCategory> GetAllCategories()
        {
            return _context.ProjectCategories.ToList();
        }
    }
}
