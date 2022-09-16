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
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryservice;

        public CategoryController(ICategoryService categoryservice)
        {
            this.categoryservice = categoryservice;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var res = categoryservice.GetAllCategories();
            return View(res);
        }

        public ActionResult CategoryProject(int CategoryId)
        {
            ViewBag.CategoryName = categoryservice.GetCategory(CategoryId).CategoryName;
            var res=categoryservice.GetProjectCategories(CategoryId);
            return View(res);
        }

        // GET: CategoryController/Create
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryservice.AddCategory(model);
                    return RedirectToAction("Index", "Category");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult EditCategory(int CategoryId)
        {
            var results = categoryservice.GetCategory(CategoryId);
            var Editcategory = new CategoryViewModel() { 
                CategoryId =results.CategoryId,
                CategoryDescription = results.CategoryDescription,
                CategoryName = results.CategoryName,
                CategoryImageFromDatabase = results.CategoryImage
            };
            return View(Editcategory);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public ActionResult EditCategory(int id, CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryservice.EditCategory(model);
                    return RedirectToAction("Index", "Category");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public void DeleteCategory(int CategoryId)
        {
            categoryservice.DeleteCategory(CategoryId);
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
