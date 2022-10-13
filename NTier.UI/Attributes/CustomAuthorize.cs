using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTier.UI.Attributes
{
    //Auth işlemlerinin Enum ile gerçekleştirilebilmesi için bu sınıfı kullanıyoruz.
    [AttributeUsage(AttributeTargets.Method |AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //AllowsMultiple ile birden fazla rol giriş olabiliyor.
    public class CustomAuthorize:AuthorizeAttribute
    {
        //String dizi rolleri tutmak için
        private string[] UserProfilesRequired { get; set; }

        public CustomAuthorize(params object[] userProfilesRequired)
        {
            if (userProfilesRequired.Any(p => p.GetType().BaseType == typeof(Enum)))
                throw new ArgumentException("userProfilesRequired");

            this.UserProfilesRequired = userProfilesRequired.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool authorized = false;

            //Kullanıcınınn rolü yakalnıyor.
            AppUserService service = new AppUserService();

            AppUser user = service.FindByUsername(HttpContext.Current.User.Identity.Name);
            string userRole = Enum.GetName(typeof(Role), user.Role);

            //Kullanıcı belirlilen rollerden birine uyuyorsa devam edebilir.
            foreach (var role in this.UserProfilesRequired)
                if(userRole == role)
                {
                    authorized = true;
                    break;
                }

            //Eğer uymuyorsa Home/Index sayfasına yönlendirilir.

            if(!authorized)
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Action("Index", "Home", new { Id = 302, Area = "" });
                filterContext.Result = new RedirectResult(loginUrl);

                return;
            }
        }
    }
}