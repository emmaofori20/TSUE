using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models.Data;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Services
{
    public class ProjectService : IProjectService
    {
        private readonly BirdTsueDBContext birdTsueDBContext;

        public ProjectService(BirdTsueDBContext birdTsueDBContext)
        {
            this.birdTsueDBContext = birdTsueDBContext;
        }

        public List<Project> GetAllProject()
        {
            return birdTsueDBContext.Projects.Where(x => x.IsDeleted == false)
                .Include(x => x.DocumentType)
                .Include(x => x.ProjectDocuments)
                .Include(x => x.ProjectLanguages)
                .ThenInclude(x => x.Language)
                .Include(x => x.ProjectCountries)
                .ToList();
        }

        public Project GetProject(int projectId)
        {
            return birdTsueDBContext.Projects.Where(x => x.ProjectId == projectId)
                .Include(x => x.DocumentType)
                .Include(x => x.ProjectDocuments)
                .Include(x => x.ProjectLanguages)
                .ThenInclude(x => x.Language)
                .Include(x => x.ProjectComments)
                .Include(x => x.ProjectCountries).FirstOrDefault();
        }

        public Project AddProject(AddProjectViewModel model)
        {
            var newProject = new Models.Data.Project()
            {
                StudyTitle = model.StudyTitle,
                Overview = model.ProjectOverview,
                CreatedBy = "Admin",
                YearOfPublication = model.YearOfPublication,
                DocumentTypeId = model.DocumentTypeId,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
                Authors = model.Author,
                ProjectIcon = model.ProjectIcon == null ? null : UploadImage(model.ProjectIcon)

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

            if (model.CountryId != 0)
            {
                ProjectCountry projectCountry = new ProjectCountry()
                {
                    CountryId = model.CountryId,
                    ProjectId = newProject.ProjectId
                };

                birdTsueDBContext.ProjectCountries.Add(projectCountry);
                birdTsueDBContext.SaveChanges();
            }


            if (model.ProjectFile.Count != 0)
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

        public void AddProjectComment(ProjectAndCommentViewModel model)
        {
            var ProjectComment = new ProjectComment()
            {
                Message = model.AddComment.Message,
                Email = model.AddComment.Email,
                ProjectId = model.ProjectId,
                FullName = model.AddComment.FullName,
                CreatedBy = model.AddComment.FullName,
                CreatedOn = DateTime.Now
            };

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
        }



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
        public async Task<UpdateProjectViewModel> GetProjectForUpdate(int projectId)
        {
            var project = await birdTsueDBContext.Projects
                .Include(x => x.ProjectDocuments)
                .Include(x => x.DocumentType)
                .Include(x => x.ProjectCountries)
                .ThenInclude(x => x.Country)
                .Include(x => x.ProjectLanguages)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(x => x.ProjectId == projectId);
            UpdateProjectViewModel updateProjectViewModel = new UpdateProjectViewModel()
            {
                ProjectId = project.ProjectId,
                StudyTitle = project.StudyTitle,
                ProjectOverview = project.Overview,
                Authors = project.Authors,
                IsDeleted = project.IsDeleted,
                YearOfPublication = project.YearOfPublication,
                ProjectIconByte = project.ProjectIcon,
                DocumentTypeId = project.DocumentTypeId,
                CountryId = project.ProjectCountries.FirstOrDefault(x => x.ProjectId == projectId).CountryId,
                LanguageId = project.ProjectLanguages.FirstOrDefault(x => x.ProjectId == projectId).LanguageId,
                ProjectDocumentsForUpdate = project.ProjectDocuments.Select(x => new ProjectDocumentForUpdate
                {
                    ProjectDocumentId = x.ProjectDocumentId,
                    ProjectId = project.ProjectId,
                    ProjectDocumentName = x.DocumentName,
                    ProjectDocumentByte = x.DocumentFile
                }).ToList()
            };

            return updateProjectViewModel;
        }

        public async Task<int> UpdateProject(UpdateProjectViewModel model)
        {
            var project = await birdTsueDBContext.Projects
                .Include(x => x.ProjectLanguages)
                .ThenInclude(x => x.Language)
                .Include(x => x.ProjectCountries)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.ProjectId == model.ProjectId);
            if (project != null)
            {
                project.ProjectId = model.ProjectId;
                project.Overview = model.ProjectOverview;
                project.StudyTitle = model.StudyTitle;
                project.YearOfPublication = model.YearOfPublication;
                project.ProjectIcon = model.ProjectIcon != null ? UploadImage(model.ProjectIcon) : project.ProjectIcon;
                project.IsDeleted = false;
                project.Authors = model.Authors;
                project.UpdatedOn = DateTime.Now;
                project.UpdatedBy = "Admin";
                project.DocumentTypeId = model.DocumentTypeId;

                if (model.LanguageId != project.ProjectLanguages.FirstOrDefault().LanguageId)
                {
                    foreach (var item in project.ProjectLanguages)
                    {
                        item.LanguageId = model.LanguageId;
                        item.ProjectId = model.ProjectId;
                        birdTsueDBContext.ProjectLanguages.Update(item);
                    }
                }

                if (model.CountryId != project.ProjectCountries.FirstOrDefault().CountryId)
                {
                    foreach (var item in project.ProjectCountries)
                    {
                        item.CountryId = model.CountryId;
                        item.ProjectId = model.ProjectId;
                        birdTsueDBContext.ProjectCountries.Update(item);
                    }
                }




                var projectDocuments = birdTsueDBContext.ProjectDocuments.Where(x => x.ProjectId == model.ProjectId).ToList();

                foreach (var item in model.ProjectDocumentsForUpdate)
                {
                    var currentProjectFile = projectDocuments.Where(x => x.ProjectDocumentId == item.ProjectDocumentId).FirstOrDefault();

                    if (item.ProjectDocumentFile != null)
                    {
                        currentProjectFile.DocumentFile = UploadImage(item.ProjectDocumentFile);
                        birdTsueDBContext.ProjectDocuments.Update(currentProjectFile);

                    }
                };

            };

            birdTsueDBContext.Projects.Update(project);
            birdTsueDBContext.SaveChanges();
            return project.ProjectId;

        }


        public AddProjectViewModel SetProjectParametersToCreateProject()
        {
            var res = birdTsueDBContext.DocumentTypes.ToList();
            var projectDocumentType = new AddProjectViewModel()
            {
                SelectLanguage = new SelectList(birdTsueDBContext.Languages
                                    .Select(s => new { Id = s.LanguageId, Text = $"{s.LanguageName}" }), "Id", "Text"),
                SelectCountry = new SelectList(birdTsueDBContext.Countries
                                    .Select(s => new { Id = s.CountryId, Text = $"{s.CountryName}" }), "Id", "Text"),
                SelectDocumentType = new SelectList(birdTsueDBContext.DocumentTypes.Where(X => X.IsDeleted == false)
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

        public ProjectAndCommentViewModel ProjectComments(int ProjectId)
        {
            var projectcomment = new ProjectAndCommentViewModel
            {
                ProjectComment = birdTsueDBContext.ProjectComments.Where(x => x.ProjectId == ProjectId).ToList(),
                ProjectId = ProjectId
            };

            return projectcomment;
        }

        public ProjectDocument GetProjectDocument(int DocumentId)
        {
            return birdTsueDBContext.ProjectDocuments.Where(x => x.ProjectDocumentId == DocumentId).FirstOrDefault();
        }


    }
}