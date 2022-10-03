using Microsoft.AspNetCore.Http;
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
    public class DocumentTypeService: IDocumentTypeService
    {
        private readonly BirdTsueDBContext birdTsueDBContext;

        public DocumentTypeService( BirdTsueDBContext birdTsueDBContext)
        {
            this.birdTsueDBContext = birdTsueDBContext;
        }

        public void AddDocumentType(DocumentTypeViewModel model)
        {
            var newDocumentType = new DocumentType() {
                DocumentTypeIcon = model.DocumentTypeIcon == null? null: UploadImage(model.DocumentTypeIcon),
                DocumentTypeName = model.DocumentTypeName,
                IsDeleted = false
            };

            birdTsueDBContext.DocumentTypes.Add(newDocumentType);
            birdTsueDBContext.SaveChanges();
           
        }

        public void DeleteDocumentType(int DocumentTypeId)
        {
            var res = birdTsueDBContext.DocumentTypes.Include(x=>x.Projects).FirstOrDefault(x=>x.DocumentTypeId == DocumentTypeId);
            res.IsDeleted = true;

            birdTsueDBContext.DocumentTypes.Update(res);
            birdTsueDBContext.SaveChanges();

            ///set all projects under DocumentTypes to deleted
            var subproject = res.Projects.Where(x => x.DocumentTypeId == DocumentTypeId).ToList();
             if(subproject.Count() == 0)
            {

            }
            else
            {
                foreach (var item in subproject)
                {
                    var project = birdTsueDBContext.Projects.Find(item.ProjectId);
                    project.IsDeleted = true;

                    birdTsueDBContext.Projects.Update(project);
                    birdTsueDBContext.SaveChanges();
                }
            }

        }

        public void EditDocumentType(DocumentTypeViewModel model)
        {
            var res = birdTsueDBContext.DocumentTypes.Find(model.DocumentTypeId);

            res.DocumentTypeName = model.DocumentTypeName;
            if(model.DocumentTypeIcon!= null)
            {
                res.DocumentTypeIcon = UploadImage(model.DocumentTypeIcon);
            }

            birdTsueDBContext.DocumentTypes.Update(res);
            birdTsueDBContext.SaveChanges();
            
        }
        public List<DocumentType> GetAllDocumentType()
        {
            return birdTsueDBContext.DocumentTypes.Where(x => x.IsDeleted == false).ToList();
        }

        public DocumentType GetDocumentType(int DocumentTypeId)
        {
            return birdTsueDBContext.DocumentTypes.Where(x => x.DocumentTypeId == DocumentTypeId).FirstOrDefault();
        }

        public List<Project> GetProjectsBelongingToDocumentType(int DocumentTypeId)
        {
            return birdTsueDBContext.Projects
                .Include(x => x.ProjectDocuments)
                .Include(x=> x.ProjectLanguages)
                .ThenInclude(y=>y.Language)
                .Include(x=> x.ProjectCountries)
                .Where(x => x.DocumentTypeId == DocumentTypeId).ToList();
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
