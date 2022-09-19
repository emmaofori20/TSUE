using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
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

        public ActionResult GetProjectsBelongingToDocumentType(int DocumentTypeId)
        {
            ViewBag.DocumentTypeName = documentTypeService.GetDocumentType(DocumentTypeId).DocumentTypeName;
            var res=documentTypeService.GetProjectsBelongingToDocumentType(DocumentTypeId);
            return View(res);
        }

        // GET: CategoryController/Create
        public ActionResult CreateDocumentType()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [Authorize]
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
            catch(Exception err)
            {
                var ErrorMessage = new ErrorViewModel()
                {
                    RequestId = err.Message
                };
                return View("Error", ErrorMessage);
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
        [Authorize]
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
            catch(Exception err)
            {
                var ErrorMessage = new ErrorViewModel()
                {
                    RequestId = err.Message
                };
                return View("Error", ErrorMessage);
            }
        }

        // GET: CategoryController/Delete/5

        [Authorize]
        public ActionResult DeleteDocumentType(int DocumentTypeId)
        {
            try
            {
                documentTypeService.DeleteDocumentType(DocumentTypeId);
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                var ErrorMessage = new ErrorViewModel()
                {
                    RequestId = err.Message
                };
                return View("Error", ErrorMessage);
            }
        }

    }
}
