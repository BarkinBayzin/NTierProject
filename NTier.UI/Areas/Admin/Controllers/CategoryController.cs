using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Areas.Admin.Controllers
{
    //[CustomAuthorize(Role.Admin)]
    public class CategoryController : Controller
    {
        CategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        public ActionResult List()
        {
            List<Category> model = _categoryService.GetActives();
            return View(model);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Category data)
        {
            _categoryService.Add(data);
            return Redirect("/Admin/Category/List");
        }

        public ActionResult Update(Guid id)
        {
            Category category = _categoryService.GetById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            _categoryService.Update(category);
            return Redirect("/Admin/Category/List");
        }

        public RedirectResult Delete(Guid id)
        {
            _categoryService.Remove(id);
            return Redirect("/Admin/Category/List");
        }

        public ActionResult Details(Guid id)
        {
            return View(_categoryService.GetById(id)); 
        }
    }
}