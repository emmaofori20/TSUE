using Microsoft.EntityFrameworkCore;
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

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int CategoryId)
        {
            return _context.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();
        }

        public List<ProjectCategory> GetProjectCategories(int CategoryId)
        {
            return _context.ProjectCategories.Include(x => x.Project).Where(x => x.CategoryId == CategoryId).ToList();
        }
    }
}
