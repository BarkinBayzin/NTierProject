using NTier.Model.Entities;
using NTier.Service.Option;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Areas.Admin.Controllers
{
    //[CustomAuthorize[Role.Admin]]
    public class ProductController : Controller
    {
        ProductService _productService;
        SubCategoryService _subCategoryService;

        public ProductController()
        {
            _productService = new ProductService();
            _subCategoryService = new SubCategoryService();
        }

        public ActionResult List(int page = 1)
        {
            List<Product> model = _productService.GetActives().OrderByDescending(x => x.CreatedDate).ToList();

            return View(model.ToPagedList(page, 10));
        }

        public ActionResult Create()
        {
            List<SubCategory> model = _subCategoryService.GetActives();
            return View(model);
        }
    }
}