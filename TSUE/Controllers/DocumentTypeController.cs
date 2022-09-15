using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Services.IServices;
using TSUE.ViewModels;

namespace TSUE.Controllers
{
    public class DocumentTypeController : Controller
    {
        private readonly IDocumentTypeService documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            this.documentTypeService = documentTypeService;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var res = documentTypeService.GetAllDocumentType();
            return View(res);
        }

        public ActionResult CategoryProject(int DocumentTypeId)
        {
            //ViewBag.CategoryName = documentTypeService.GetCategory(DocumentTypeId).CategoryName;
            var res=documentTypeService.GetProjectCategories(DocumentTypeId);
            return View(res);
        }

        // GET: CategoryController/Create
        public ActionResult CreateDocumentType()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        public ActionResult CreateDocumentType(DocumentTypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    documentTypeService.AddDocumentType(model);
                    return RedirectToAction("Index", "DocumentType");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult EditDocumentType(int DocumentTypeId)
        {
            var results = documentTypeService.GetDocumentType(DocumentTypeId);
            var EditDocumentType = new DocumentTypeViewModel() { 
                DocumentTypeId =results.DocumentTypeId,
                DocumentTypeName = results.DocumentTypeName,
                DocumentTypeImage = results.DocumentTypeIcon
            };
            return View(EditDocumentType);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public ActionResult EditDocumentType(int id, DocumentTypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    documentTypeService.EditDocumentType(model);
                    return RedirectToAction("Index", "DocumentType");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public void DeleteDocumentType(int DocumentTypeId)
        {
            documentTypeService.DeleteDocumentType(DocumentTypeId);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
