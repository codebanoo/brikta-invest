using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Core.Ext;
using Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Models.Business;
using AutoMapper;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using CustomAttributes;
using Services.Business;
using Microsoft.AspNetCore.Authentication.Cookies;
using VM.Console;
using Models.Business.ConsoleBusiness;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ApiCallers.TeniacoApiCaller;
using Newtonsoft.Json.Linq;
using VM.Base;
using VM.PVM.Teniaco;
using VM.Teniaco;
using ApiCallers.ProjectsApiCaller;
using VM.Projects;
using VM.Projects.TeniacoSuggestions;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class TeniacoSuggestionsManagementController : ExtraUsersController
    {
        public TeniacoSuggestionsManagementController(IHostEnvironment _hostEnvironment,
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

        public IActionResult Index()
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            // تعداد مدارک تایید نشده و مکالمات خوانده نشده برای زنگوله
            if (ViewData["GetUnreadConversationsAndUnverifiedFilesCount"] == null)
            {
                GetUnreadConversationsAndUnverifiedFilesCountVM getUnreadConversationsAndUnverifiedFilesCountVM = new GetUnreadConversationsAndUnverifiedFilesCountVM();

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/TeniacoPanel/GetUnreadConversationsAndUnverifiedFilesCount";

                    GetUnreadConversationsAndUnverifiedFilesCountPVM getUnreadConversationsAndUnverifiedFilesCountPVM = new GetUnreadConversationsAndUnverifiedFilesCountPVM()
                    {
                        UserId = this.userId.Value
                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetUnreadConversationsAndUnverifiedFilesCount(getUnreadConversationsAndUnverifiedFilesCountPVM);
                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;
                        if (jsonResultWithRecordObjectVM != null)
                        {
                            if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    getUnreadConversationsAndUnverifiedFilesCountVM = jObject.ToObject<GetUnreadConversationsAndUnverifiedFilesCountVM>();
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["GetUnreadConversationsAndUnverifiedFilesCount"] = getUnreadConversationsAndUnverifiedFilesCountVM;
            }

            return View("UserIndex");
        }

        [HttpPost]
        [AjaxOnly]
        public IActionResult GetListOfTeniacoSuggestedProjects(int pageNum = 1, int pageSize = 5)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = projectsApiUrl + "/api/TeniacoSuggestionsManagement/GetListOfTeniacoSuggestedProjects";

                GetListOfTeniacoSuggestedProjectsPVM getListOfTeniacoSuggestedProjectsPVM = new GetListOfTeniacoSuggestedProjectsPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    UserId = this.userId.Value,
                    pageNum = pageNum,
                    pageSize = pageSize
                };

                responseApiCaller = new ProjectsApiCaller(serviceUrl).GetListOfTeniacoSuggestedProjects(getListOfTeniacoSuggestedProjectsPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<SuggestedConstructionProjectsVM>>();


                            return Json(new
                            {
                                Result = jsonResultWithRecordsObjectVM.Result,
                                Records = records,
                                TotalRecordCount = jsonResultWithRecordsObjectVM.TotalRecordCount
                            });
                        }
                    }
                }
            }
            catch (Exception exc)
            {
            }

            return Json(new
            {
                Result = "ERROR",
                Message = "خطا"
            });
        }

    }
}