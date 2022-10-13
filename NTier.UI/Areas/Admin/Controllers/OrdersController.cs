using NTier.Core.Entity.Enum;
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
    //[CustomAuthorize(Role.Admin)]
    public class OrdersController : Controller
    {
        ProductService _productService;
        OrderDetailService _orderDetailService;
        OrderService _orderService;

        public OrdersController()
        {
            _orderDetailService = new OrderDetailService();
            _productService = new ProductService();
            _orderService = new OrderService();
        }
        [HttpGet]
        public ActionResult List(int page = 1)
        {
            //Daha onaylanmamış tüm siparişleri listele
            List<Orders> model = _orderService.ListPendingOrders();
            return View(model.ToPagedList(page, 10));
        }

        //Onaylanmamış sipariş sayısını ana sayfada listeler
        public JsonResult OrderCount()
        {
            int count = _orderService.PendingOrderCount();

            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid id)
        {
            List<OrderDetails> model = _orderDetailService.GetDefaults(x => x.Orders.Id == id);
            return View(model);
        }

        //Sipariş onaylama
        public RedirectResult ConfirmOrder(Guid id)
        {
            Orders order = new Orders();
            order = _orderService.GetById(id);

            order.Confirmed = true;
            _orderService.Update(order);

            foreach (var item in order.OrderDetails)
            {
                //Satın alınan ürünün, satış adetine göre toplam stok güncelleme alanı
                Product p = _productService.GetById(item.ProductID);
                p.UnitsInStock -= item.Quantity;
                _productService.Update(p);
            }

            return Redirect("~/Admin/Orders/List");
        }

        //Sipariş Reddetme
        public RedirectResult RejectOrder(Guid id)
        {
            Orders order = _orderService.GetById(id);

            order.Confirmed = false;
            order.Status = Status.Deleted;
            _orderService.Update(order);

            return Redirect("~/Admin/Orders/List");
        }

    }
}