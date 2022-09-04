﻿using Microsoft.AspNetCore.Http;
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
                CategoryName = model.CategoryName
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
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
