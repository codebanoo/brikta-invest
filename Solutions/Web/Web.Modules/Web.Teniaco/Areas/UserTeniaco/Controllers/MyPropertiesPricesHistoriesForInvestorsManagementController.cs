using ApiCallers.BaseApiCaller;
using ApiCallers.TeniacoApiCaller;
using CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Models.Business.ConsoleBusiness;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using VM.Base;
using VM.PVM.Teniaco;
using VM.Teniaco;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Services.Business;
using Web.Core.Controllers;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class MyPropertiesPricesHistoriesForInvestorsManagementController : ExtraUsersController
    {
        public MyPropertiesPricesHistoriesForInvestorsManagementController(IHostEnvironment _hostEnvironment,
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


        [HttpPost]
        [AjaxOnly]
        public IActionResult GetListOfMyPropertiesPricesHistoriesForInvestors(long PropertyId)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MyPropertiesPricesHistoriesForInvestorsManagement/GetListOfMyPropertiesPricesHistoriesForInvestors";

                GetListOfMyPropertiesPricesHistoriesForInvestorsPVM getListOfMyPropertiesPricesHistoriesPVM = new GetListOfMyPropertiesPricesHistoriesForInvestorsPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    UserId = this.userId.Value,
                    jtSorting = "",
                    PropertyId = PropertyId
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfMyPropertiesPricesHistoriesForInvestors(getListOfMyPropertiesPricesHistoriesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<PropertiesPricesHistoriesVM>>();


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
