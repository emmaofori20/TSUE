using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Models.Data;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Services
{
    public class ProjectService: IProjectService
    {
        private readonly TSUEProjectDbContext _context;
        private readonly BirdTsueDBContext birdTsueDBContext;

        public ProjectService(TSUEProjectDbContext context, BirdTsueDBContext birdTsueDBContext)
        {
            this._context = context;
            this.birdTsueDBContext = birdTsueDBContext;
        }

       

        public List<Models.Data.Project> GetAllProject()
        {
            return birdTsueDBContext.Projects.Where(x=>x.IsDeleted == false)
                .Include(x=>x.DocumentType)
                .Include(x=>x.ProjectDocuments).ToList();
        }

        public Models.Project GetProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Models.Data.Project AddProject(AddProjectViewModel model)
        {
            var newProject = new Models.Data.Project()
            {
                StudyTitle = model.StudyTitle,
                Overview = model.ProjectOverview,
                CreatedBy = "Admin",
                YearOfPublication = model.ProjectDate,
                DocumentTypeId = model.DocumentTypeId,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
                Authors = model.Author,
                ProjectIcon = model.ProjectIcon == null? null : UploadImage(model.ProjectIcon)

            };

            birdTsueDBContext.Projects.Add(newProject);
            birdTsueDBContext.SaveChanges();

            ProjectLanguage newProjectLanguage = new ProjectLanguage()
            {
                ProjectId = newProject.ProjectId,
                LanguageId = model.LanguageId,
                
            };
            birdTsueDBContext.ProjectLanguages.Add(newProjectLanguage);
            birdTsueDBContext.SaveChanges();

            if(model.CountryId != 0)
            {
                ProjectCountry projectCountry = new ProjectCountry()
                {
                    CountryId = model.CountryId,
                    ProjectId = newProject.ProjectId
                };

                birdTsueDBContext.ProjectCountries.Add(projectCountry);
                birdTsueDBContext.SaveChanges();
            }
           

            if(model.ProjectFile.Count != 0)
            {
                foreach (var item in model.ProjectFile)
                {
                    ProjectDocument projectfile = new ProjectDocument()
                    {
                        DocumentFile = UploadImage(item),
                        ProjectId = newProject.ProjectId,
                        DocumentName = item.FileName,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Admin",


                    };
                    birdTsueDBContext.ProjectDocuments.Add(projectfile);
                    birdTsueDBContext.SaveChanges();
                }
              
            }

           

            return newProject;
        }

        //public void AddProjectComment(ProjectCommentViewModel model)
        //{
        //    var comment = new Comment()
        //    {
        //        Email = model.AddComment.Email,
        //        Comment1 = model.AddComment.Message,
        //        CommenterName = model.AddComment.FullName,
        //        CreatedBy = model.AddComment.FullName,
        //        CreatedOn = DateTime.Now,

        //    };
        //    _context.Comments.Add(comment);
        //    _context.SaveChanges();

        //    // Adding to projectComments table

        //    var projectCommnet = new ProjectComment()
        //    {
        //        ProjectId = model.ProjectId,
        //        CommentId = comment.CommentId
        //    };

        //    _context.ProjectComments.Add(projectCommnet);
        //    _context.SaveChanges();
        //}

       

        //public Project GetProject(int projectId)
        //{
        //    return _context.Projects.Include(x => x.ProjectFiles).Where(x => x.ProjectId == projectId).FirstOrDefault();
        //}

        //public ProjectCommentViewModel ProjectComments(int ProjectId)
        //{
        //    var results = new ProjectCommentViewModel()
        //    {
        //        ProjectId = ProjectId,
        //        ProjectComment = _context.ProjectComments.Include(x => x.Comment)
        //        .Where(x => x.ProjectId == ProjectId)
        //        .ToList(),

        //    };

        //    return results;
        //}


        public AddProjectViewModel SetProjectParametersToCreateProject()
        {
            var res = birdTsueDBContext.DocumentTypes.ToList();
            var projectDocumentType = new AddProjectViewModel()
            {
                SelectLanguage = new SelectList(birdTsueDBContext.Languages
                                    .Select(s => new { Id = s.LanguageId, Text = $"{s.LanguageName}" }), "Id", "Text"),
                SelectCountry = new SelectList(birdTsueDBContext.Countries
                                    .Select(s => new { Id = s.CountryId, Text = $"{s.CountryName}" }), "Id", "Text"),
                SelectDocumentType = new SelectList(birdTsueDBContext.DocumentTypes.Where(X=>X.IsDeleted == false)
                                    .Select(s => new { Id = s.DocumentTypeId, Text = $"{s.DocumentTypeName}" }), "Id", "Text"),
            };

            return projectDocumentType;
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
