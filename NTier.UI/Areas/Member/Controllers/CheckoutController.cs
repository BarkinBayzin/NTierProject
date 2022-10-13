using NTier.Model.Entities;
using NTier.Service.Option;
using NTier.UI.Areas.Member.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Areas.Member.Controllers
{
    //[CustomAuthorize(Role.Member, Role.Admin)]
    public class CheckoutController : Controller
    {
        OrderService _orderService;
        ProductService _productService;
        AppUserService _appUserService;
        public CheckoutController()
        {
            _appUserService = new AppUserService();
            _orderService = new OrderService();
            _productService = new ProductService();
        }

        public ActionResult Add()
        {
            if(Session["sepet"] == null)
            {
                return Redirect("~/Home/Index");
            }

            ProductCart cart = Session["sepet"] as ProductCart;

            Orders order = new Orders();

            AppUser user = _appUserService.FindByUsername(HttpContext.User.Identity.Name);
            order.AppUserId = user.Id;
            order.AppUser = user;
            _appUserService.DetachEntity(user);

            Product product = new Product();
            foreach (var item in cart.CartProductList)
            {
                product = _productService.GetById(item.Id);

                order.OrderDetails.Add(new OrderDetails
                {
                    ProductID = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
                _productService.DetachEntity(product);
            }

            _orderService.Add(order);

            return Redirect("/Home/Index");
        }
    }
}