using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly TSUEProjectDbContext _context;

        public CategoryService(TSUEProjectDbContext context)
        {
            this._context = context;
        }

        public void AddCategory(CategoryViewModel model)
        {
            var newCategory = new Category() { 
                CategoryImage = UploadImage(model.CategoryImage),
                CategoryDescription = model.CategoryDescription,
                CategoryName = model.CategoryName,
                IsDeleted = false
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
        }

        public void DeleteCategory(int CategoryId)
        {
            var res = _context.Categories.Include(x=>x.ProjectCategories).FirstOrDefault(x=>x.CategoryId == CategoryId);
            res.IsDeleted = true;

            _context.Categories.Update(res);
            _context.SaveChanges();

            ///set all projects under category to deleted
            var subproject= res.ProjectCategories.Where(x => x.CategoryId == CategoryId).ToList();
             if(subproject.Count() == 0)
            {

            }
            else
            {
                foreach (var item in subproject)
                {
                    var project = _context.Projects.Find(item.ProjectId);
                    project.IsDeleted = true;

                    _context.Projects.Update(project);
                    _context.SaveChanges();
                }
            }

        }

        public void EditCategory(CategoryViewModel model)
        {
            var res = _context.Categories.Find(model.CategoryId);

            res.CategoryDescription = model.CategoryDescription;
            res.CategoryName = model.CategoryName;
            if(model.CategoryImage!= null)
            {
                res.CategoryImage = UploadImage(model.CategoryImage);
            }

            _context.Categories.Update(res);
            _context.SaveChanges();
            
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.Where(x => x.IsDeleted == false).ToList();
        }

        public Category GetCategory(int CategoryId)
        {
            return _context.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();
        }

        public List<ProjectCategory> GetProjectCategories(int CategoryId)
        {
            return _context.ProjectCategories.Include(x => x.Project).Where(x => x.CategoryId == CategoryId).ToList();
        }


        private byte[] UploadImage(IFormFile formFile)
        {
            byte[] fileBytes;

            using (var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                fileBytes = stream.ToArray();
            }

            return fileBytes;
        }

    }
}
