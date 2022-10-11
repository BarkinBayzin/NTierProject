﻿using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Controllers
{
    public class ProductController : Controller
    {
        ProductService _productService;
        SubCategoryService _subCategoryService;

        public ProductController(SubCategoryService subCategoryService, ProductService productService)
        {
            _subCategoryService = subCategoryService;
            _productService = productService;
        }

        public ActionResult Detail(Guid? id)
        {
            if(id != null)
            {
                Product model = _productService.GetById((Guid)id);
                return View(model);
            }

            return Redirect("~/Home/Index");
        }

        public ActionResult List(Guid? id)
        {
            if(id!=null)
            {
                IEnumerable<Product> productListByCategory = _productService.GetDefaults(x => x.SubCategoryID == id);
                return View(productListByCategory);
            }

            return Redirect("~/Home/Index");
        }
    }
}