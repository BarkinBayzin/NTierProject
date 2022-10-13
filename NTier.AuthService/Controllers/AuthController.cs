﻿using NTier.AuthService.Models;
using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NTier.AuthService.Controllers
{
    public class AuthController : ApiController
    {
        AppUserService _userService;
        public AuthController()
        {
            _userService = new AppUserService();
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Login(Credentials model)
        {
            var url = "";
            if (model.username == null || model.password == null)
            {
                url = "https://localhost:44368/Home/Login";
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Success = true, RedirectUrl = url });
            }
            if (_userService.CheckCredentials(model.username, model.password))
            {
                AppUser user = new AppUser();
                user = _userService.FindByUsername(model.username);

                if (user.Role == Role.Admin || user.Role == Role.Member)
                {
                    url = "https://localhost:44368/Home/Index" + user.Id;
                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = url });
                }
                else
                {
                    url = "https://localhost:44368/Home/Index";
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Success = true, RedirectUrl = url });

                }
            }
            url = "https://localhost:44368/Home/Login";
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { Success = true, RedirectUrl = url });
        }

        [HttpGet]
        public HttpResponseMessage Logout()
        {
            var newUrl = "https://localhost:44368/Home/Logout";
            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
        }
    }
}
