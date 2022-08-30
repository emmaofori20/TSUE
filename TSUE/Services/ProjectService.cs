﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProjectService: IProjectService
    {
        private readonly TSUEProjectDbContext _context;

        public ProjectService(TSUEProjectDbContext context)
        {
            this._context = context;
        }

        public Project AddProject(AddProjectViewModel model)
        {
            var newProject = new Project()
            {
                ProjectTitle = model.ProjectTitle,
                ProjectDescription = model.ProjectDescription,
                ProjectSummary = model.ProjectSummary,
                CreatedBy = "Admin",
                ProjectDate = model.ProjectDate,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
                Author = model.Author,
                ProjectIcon = UploadImage(model.ProjectIcon)

            };

            _context.Projects.Add(newProject);
            _context.SaveChanges();

            ProjectCategory newProjectCategory = new ProjectCategory()
            {
                ProjectId = newProject.ProjectId,
                CategoryId = model.ProjectCategoryId,
                CreatedOn = DateTime.Now,
                CreatedBy = "Admin",
            };

            _context.ProjectCategories.Add(newProjectCategory);
            _context.SaveChanges();

            ProjectFile projectfile = new ProjectFile()
            {
                ProjectFile1 = UploadImage(model.ProjectFile),
                ProjectId = newProject.ProjectId,
                ProjectFileName = model.ProjectFile.FileName,
                CreatedOn = DateTime.Now,
                CreatedBy = "Admin",


            };
            _context.ProjectFiles.Add(projectfile);
            _context.SaveChanges();

            return newProject;
        }

        public List<Project> GetAllProject()
        {
            return _context.Projects.Where(x => x.IsDeleted == false).ToList();
        }

        public Project GetProject(int projectId)
        {
            return _context.Projects.Where(x => x.ProjectId == projectId).FirstOrDefault();
        }

        public AddProjectViewModel SetProjectForCreate()
        {
            var res = _context.Categories.ToList();
            var projectCategory = new AddProjectViewModel()
            {
                selectCategory = new SelectList(_context.Categories.Select( s => new { Id = s.CategoryId, Text = $"{s.CategoryName}" }), "Id", "Text"),
            };

            return projectCategory;
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
