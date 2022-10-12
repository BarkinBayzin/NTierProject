using NTier.Core.Entity.Enum;
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
    public class HomeController : Controller
    {
        OrderService _orderService;

        public HomeController()
        {
            _orderService = new OrderService();
        }


        public ActionResult Index()
        {
            //Onaylanmamaış tüm sipariler admin'e gönderiliyoru.
            List<Orders> model = _orderService.GetDefaults(x => x.Confirmed == false && x.Status == Status.Active);

            //Sipariş sayısı viewbag içerisinde gönderiliyor.
            if(model != null) ViewBag.Siparis = model.Count;
            
            return View();
        }
    }
}