using NTier.Model.Entities;
using NTier.Service.Option;
using NTier.UI.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Areas.Admin.Controllers
{
    //[CustomAuthorize(Role.Admin)]
    public class AppUserController : Controller
    {
        AppUserService _appUserService;
        public AppUserController()
        {
            _appUserService = new AppUserService();
        }

        public ActionResult List(int page = 1)
        {
            List<AppUser> model = _appUserService.GetActives();
            return View(model.ToPagedList(page, 10));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AppUser data, HttpPostedFileBase image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", image);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
                data.ImagePath = "~/Uploads/48718ae0-cc55-4377-94ce-de4b69cbf590.jpg";

            _appUserService.Add(data);
            return Redirect("~/Admin/AppUser/List");
        }

        public ActionResult Edit(Guid id)
        {
            AppUser appUser = _appUserService.GetById(id);
            return View(appUser);
        }

        [HttpPost]
        public ActionResult Edit(AppUser data, HttpPostedFileBase image)
        {
            //Elimizdeki imajı güncelleme esnasında kaybetmemek içinbir kontrol daha uyguluyoruz.
            AppUser updated = _appUserService.GetById(data.Id);

            if (image != null) data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", image);
            else
                data.ImagePath = updated.ImagePath;

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {                
                if (updated.ImagePath == null || updated.ImagePath == "~/Uploads/48718ae0-cc55-4377-94ce-de4b69cbf590.jpg")
                    data.ImagePath = "~/Uploads/48718ae0-cc55-4377-94ce-de4b69cbf590.jpg";
                else data.ImagePath = updated.ImagePath;                
            }
            _appUserService.Update(data);
            return Redirect("~/Admin/AppUser/List");
        }

        public ActionResult Details(Guid id)
        {
            return View(_appUserService.GetById(id));
        }

        public RedirectResult Delete(Guid id)
        {
            _appUserService.Remove(id);
            return Redirect("~/Admin/AppUser/List");
        }
    }
}