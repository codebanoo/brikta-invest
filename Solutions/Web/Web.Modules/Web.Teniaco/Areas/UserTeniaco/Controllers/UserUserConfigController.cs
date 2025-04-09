using AutoMapper;
using Microsoft.Aspnet.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Models.Business.ConsoleBusiness;
using Services.Business;
using System;
using Web.Core.Controllers;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class UserUserConfigController : ExtraUsersController
    {
        public UserUserConfigController(IHostEnvironment _hostEnvironment,
            IHttpContextAccessor _httpContextAccessor,
            IActionContextAccessor _actionContextAccessor,
            IConfigurationRoot _configurationRoot,
            IMapper _mapper,
            IConsoleBusiness _consoleBusiness,
            IPublicServicesBusiness _publicServicesBusiness,
            IMemoryCache _memoryCache,
            IDistributedCache _distributedCache) :
            base(_hostEnvironment,
            _httpContextAccessor,
            _actionContextAccessor,
            _configurationRoot,
            _mapper,
            _consoleBusiness,
            _publicServicesBusiness,
            _memoryCache,
            _distributedCache)
        {
        }

        public ActionResult LogOff()
        {
            DateTime expireTime = DateTime.Now.AddDays(365);
            CookieOptions option = new CookieOptions();
            option.Expires = expireTime;
            Response.Cookies.Delete("UserId", option);

            if (BaseAuthentication.LogOff(HttpContext))
                return RedirectToAction("Login", "Home", new { area = "" });

            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}
