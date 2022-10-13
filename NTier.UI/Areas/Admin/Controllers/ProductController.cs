using NTier.Model.Entities;
using NTier.Service.Option;
using NTier.UI.Areas.Admin.Models;
using NTier.UI.Helpers;
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
            //foreach (var item in model)
            //{
            //    item.SubCategory = _subCategoryService.GetById(item.SubCategoryID);
            //}
            return View(model.ToPagedList(page, 10));
        }

        public ActionResult Create()
        {
            List<SubCategory> model = _subCategoryService.GetActives();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Product data, HttpPostedFileBase image)
        {
            if (data != null)
            {
                data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", image);

                if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
                    data.ImagePath = "~/Uploads/48718ae0-cc55-4377-94ce-de4b69cbf590.jpg";

                _productService.Add(data);
                return Redirect("/Admin/Product/List");
            }

            return View(data);
        }
        //Update View'ına gönderilecek ProductUpdateVM oluşturulur.
        public ActionResult Update(Guid id)
        {
            UpdateProductVM model = new UpdateProductVM()
            {
                Product = _productService.GetById(id),
                SubCategories = _subCategoryService.GetActives()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Product data, HttpPostedFileBase image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", image);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                Product updated = _productService.GetById(data.Id);
                if (updated.ImagePath == null || updated.ImagePath == "~/Uploads/48718ae0-cc55-4377-94ce-de4b69cbf590.jpg")
                {
                    data.ImagePath = "~/Uploads/48718ae0-cc55-4377-94ce-de4b69cbf590.jpg";
                }
                else
                {
                    data.ImagePath = updated.ImagePath;
                }
            }
            _productService.Update(data);
            return Redirect("/Admin/Product/List");

        }

        public RedirectResult Delete(Guid id)
        {
            _productService.Remove(id);
            return Redirect("/Admin/Product/List");
        }

        public ActionResult Details(Guid id)
        {
            return View(_productService.GetById(id));
        }
    }
}