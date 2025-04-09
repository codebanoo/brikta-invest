using ApiCallers.TeniacoApiCaller;
using AutoMapper;
using CustomAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Models.Business.ConsoleBusiness;
using Newtonsoft.Json.Linq;
using Services.Business;
using System.Collections.Generic;
using System;
using VM.Base;
using VM.PVM.Teniaco;
using VM.Teniaco;
using Web.Core.Controllers;
using FrameWork;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class MyPropertiesFeaturesForInvestorsManagementController : ExtraUsersController
    {
        public MyPropertiesFeaturesForInvestorsManagementController(IHostEnvironment _hostEnvironment,
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
        public IActionResult GetMyPropertyFeaturesValuesForInvestors(long PropertyId,int PropertyTypeId)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MyPropertiesFeaturesForInvestorsManagement/GetMyPropertyFeaturesValuesForInvestors";

                GetMyPropertyFeaturesValuesForInvestorsPVM getMyPropertyFeaturesValuesPVM = new GetMyPropertyFeaturesValuesForInvestorsPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    UserId = this.userId.Value,
                    jtSorting = "",
                    PropertyId = PropertyId,
                    PropertyTypeId = PropertyTypeId
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetMyPropertyFeaturesValuesForInvestors(getMyPropertyFeaturesValuesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {

                            JObject jObject = jsonResultWithRecordObjectVM.Record;
                            var record = jObject.ToObject<PropertyFeaturesValuesVM>();


                            return Json(new
                            {
                                Result = jsonResultWithRecordObjectVM.Result,
                                Record = record,
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



        [AjaxOnly]
        [HttpPost]
        public IActionResult UpdatePropertyFeaturesForInvestors(List<FeaturesValuesVM> featuresValuesVMList, int propertyId)
        {
            try
            {
                JsonResultObjectVM jsonResultObjectVM = new JsonResultObjectVM(new object() { });

                foreach (FeaturesValuesVM featuresValuesVM in featuresValuesVMList)
                {
                    featuresValuesVM.CreateEnDate = DateTime.Now;
                    featuresValuesVM.CreateTime = PersianDate.TimeNow;
                    featuresValuesVM.UserIdCreator = this.userId.Value;

                    featuresValuesVM.IsActivated = true;
                    featuresValuesVM.IsDeleted = false;

                    featuresValuesVM.PropertyId = propertyId;
                }

                string serviceUrl = teniacoApiUrl + "/api/PropertiesFeaturesManagement/UpdatePropertyFeatures";

                UpdatePropertyFeaturesPVM updatePropertyFeaturesPVM = new UpdatePropertyFeaturesPVM()
                {
                    FeaturesValuesVMList = featuresValuesVMList,
                    UserId = this.userId.Value,
                    PropertyId = propertyId,
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).UpdatePropertyFeatures(updatePropertyFeaturesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultObjectVM = responseApiCaller.JsonResultObjectVM;

                    if (jsonResultObjectVM != null)
                    {
                        if (jsonResultObjectVM.Result.Equals("OK"))
                        {
                            return Json(new
                            {
                                Result = "OK",
                                Message = "بروز رسانی انجام شد"
                            });
                        }
                    }
                }
            }
            catch (Exception exc)
            { }

            return Json(new
            {
                Result = "ERROR",
                Message = "خطا"
            });
        }


    }
}
