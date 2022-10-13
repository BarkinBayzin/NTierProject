using NTier.AuthService.Models;
using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTier.UI.Controllers
{
    public class HomeController : Controller
    {
        ProductService _productService;
        AppUserService _appUserService;
        CategoryService _categoryService;
        public HomeController()
        {
            _productService = new ProductService();
            _appUserService = new AppUserService();
            _categoryService = new CategoryService();
        }

        public ActionResult Index(Guid? id)
        {
            //Id API üzerinden gönderiliyor. Eğer Boş ise authentication yapmıyoruz.
            if (id != null)
            {
                AppUser user = new AppUser();
                user = _appUserService.GetById((Guid)id);
                string cookie = user.UserName.ToString();
                FormsAuthentication.SetAuthCookie(cookie, true);

                if (user.Role == Role.Admin) return Redirect("/Admin/Home/Index");
            }
            var model = _productService.GetDefaults(x => x.UnitsInStock > 0).OrderByDescending(x => x.CreatedDate).Take(16).ToList();

            return View();
        }

        //Bu metot PartialView'i yönlendirmek için kullanılıyor. 
        //ChildActionOnly bu action'ın sadee bu durumlarda çağırabileceğini belirtir. Opsiyoneldir..
        [ChildActionOnly]
        public ActionResult CategoryList()
        {
            return PartialView("_CategoryList",_categoryService.GetActives());
        }

        [ChildActionOnly]
        public ActionResult ProductList()
        {
            return PartialView("_ProductList", _productService.GetActives().OrderByDescending(x => x.CreatedDate).Take(9).ToList());
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Credentials model)
        {
            var url = "";
            if (model.username == null || model.password == null)
            {
                url = "https://localhost:44368/Home/Login";
                return Redirect(url);
            }
            if (_appUserService.CheckCredentials(model.username, model.password))
            {
                AppUser user = new AppUser();
                user = _appUserService.FindByUsername(model.username);

                if (user.Role == Role.Admin || user.Role == Role.Member)
                {
                    return View("Index",user.Id);
                }
                else
                {
                    return RedirectToAction("Index", "Home");


                }
            }
            return RedirectToAction("Login", "Home");

        }

        //Bu metot API üzerinden yönlendirilerek ulaşılmaktadır.
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

    }
}