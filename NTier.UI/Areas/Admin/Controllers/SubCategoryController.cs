using NTier.Model.Entities;
using NTier.Service.Option;
using NTier.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Areas.Admin.Controllers
{
    public class SubCategoryController : Controller
    {
        SubCategoryService _subCategoryService;
        CategoryService _categoryService;

        public SubCategoryController()
        {
            _subCategoryService = new SubCategoryService();
            _categoryService = new CategoryService();
        }

        public ActionResult List()
        {
            List<SubCategory> model = _subCategoryService.GetActives();
            //foreach (var item in model)
            //{
            //    item.Category = _categoryService.GetById(item.CategoryID);
            //}
            return View(model);
        }

        public ActionResult Create()
        {
            List<Category> model = _categoryService.GetActives();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SubCategory data)
        {
            if (!ModelState.IsValid) return View(data);
            data.Id = Guid.NewGuid();
            //data.Category = _categoryService.GetById(data.CategoryID);
            _subCategoryService.Add(data);
            return Redirect("/Admin/SubCategory/List");
        }

        public ActionResult Update(Guid id)
        {
            SubCategoryVM model = new SubCategoryVM();
            model.SubCategory= _subCategoryService.GetById(id);
            model.Categories = _categoryService.GetActives();

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(SubCategory data)
        {
            _subCategoryService.Update(data);
            return Redirect("/Admin/SubCategory/List");
        }

        public ActionResult Details(Guid id)
        {
            return View(_subCategoryService.GetById(id));
        }

        // Get Action'ından id parametresi yakalanır.(url'den)
        // _subCategoryService içerisinden remove metodu ile data silinir.
        // Listeye yönlendirilir

        public RedirectResult Delete(Guid id)
        {
            _subCategoryService.Remove(id);
            return Redirect("/Admin/SubCategory/List");
        }
    }
}