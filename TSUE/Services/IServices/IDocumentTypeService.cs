using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models.Data;
using TSUE.ViewModels;

namespace TSUE.Services.IServices
{
    public interface IDocumentTypeService
    {
        public List<DocumentType> GetAllDocumentType();

        public List<Project> GetProjectsBelongingToDocumentType(int DocumentTypeId);

        public DocumentType GetDocumentType(int DocumentTypeId);

        public void AddDocumentType(DocumentTypeViewModel model);

        public void EditDocumentType(DocumentTypeViewModel model);

        public void DeleteDocumentType(int DocumentTypeId);
    }
}
