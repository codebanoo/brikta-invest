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
using VM.Melkavan;
using ApiCallers.MelkavanApiCaller;
using VM.PVM.Melkavan;
using VM.PVM.Public;
using ApiCallers.PublicApiCaller;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class MelkavanPropertiesManagementController : ExtraUsersController
    {
        public MelkavanPropertiesManagementController(IHostEnvironment _hostEnvironment,
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
            ViewData["Title"] = "لیست املاک";
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });
            //JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            //int countOfListAdvertisementVMList = 0;
            //int pageNum = 1;
            //List<AdvertisementVM> advertisementVMList = new List<AdvertisementVM>();
            //try
            //{
            //serviceUrl = melkavanApiUrl + "/api/AdvertisementManagement/GetListOfAdvertisementWithMoreDetail";
            //GetListOfAdvertisementWithMoreDetailPVM GetListOfAdvertisementPVM = new GetListOfAdvertisementWithMoreDetailPVM();

            //GetListOfAdvertisementPVM.jtPageSize = 50;
            //GetListOfAdvertisementPVM.jtStartIndex = (pageNum - 1) * GetListOfAdvertisementPVM.jtPageSize;
            //GetListOfAdvertisementPVM.HaveAddress = true;
            //GetListOfAdvertisementPVM.HaveFeatures = true;
            //GetListOfAdvertisementPVM.HaveFiles = true;
            //GetListOfAdvertisementPVM.HaveDetails = true;
            //GetListOfAdvertisementPVM.HaveFavorites = true;
            //GetListOfAdvertisementPVM.HaveCallers = true;
            //GetListOfAdvertisementPVM.HaveViewers = true;

            //responseApiCaller = new MelkavanApiCaller(serviceUrl).GetListOfAdvertisementWithMoreDetail(GetListOfAdvertisementPVM);
            //if (responseApiCaller.IsSuccessStatusCode)
            //{
            //    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
            //    if (jsonResultWithRecordsObjectVM != null)
            //    {
            //        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
            //        {

            //            JArray jArray = jsonResultWithRecordsObjectVM.Records;
            //            advertisementVMList = jArray.ToObject<List<AdvertisementVM>>();
            //            countOfListAdvertisementVMList = jsonResultWithRecordsObjectVM.TotalRecordCount;
            //        }
            //    }
            //}
            //}
            //catch (Exception exc)
            //{ }
            //ViewData["advertisementVMList"] = advertisementVMList;
            //ViewData["countOfListAdvertisementVMList"] = countOfListAdvertisementVMList;

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


            //ViewData["domainAddress"] = this.domain;
            ViewData["domainAddress"] = "melkavan.com";
            return View("UserIndex");
        }


        [AjaxOnly]
        [HttpPost]
        public IActionResult GetListOfAdvertisementWithMoreDetail(int? pageNum)
        {
            ViewData["Title"] = "لیست املاک";
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            int CountOfList = 0;
            List<AdvertisementVM> advertisementVMList = new List<AdvertisementVM>();
            try
            {
                serviceUrl = melkavanApiUrl + "/api/AdvertisementManagement/GetListOfAdvertisementWithMoreDetail";
                GetListOfAdvertisementWithMoreDetailPVM GetListOfAdvertisementPVM = new GetListOfAdvertisementWithMoreDetailPVM();

                GetListOfAdvertisementPVM.jtPageSize = 50;

                //#if DEBUG
                //                GetListOfAdvertisementPVM.jtPageSize = 5;
                //#else
                //            {
                //                   ;
                //                    GetListOfAdvertisementPVM.jtPageSize = 50;
                //            }
                //#endif
                GetListOfAdvertisementPVM.jtStartIndex = (pageNum - 1) * GetListOfAdvertisementPVM.jtPageSize;
                GetListOfAdvertisementPVM.HaveAddress = true;
                GetListOfAdvertisementPVM.HaveFeatures = true;
                GetListOfAdvertisementPVM.HaveFiles = true;
                GetListOfAdvertisementPVM.HaveDetails = true;
                GetListOfAdvertisementPVM.HaveFavorites = true;
                GetListOfAdvertisementPVM.HaveCallers = true;
                GetListOfAdvertisementPVM.HaveViewers = true;
                responseApiCaller = new MelkavanApiCaller(serviceUrl).GetListOfAdvertisementWithMoreDetail(GetListOfAdvertisementPVM);
                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {
                            return Json(jsonResultWithRecordsObjectVM);
                        }
                    }
                }
            }
            catch (Exception exc)
            { }
            return View("UserIndex");
        }



        [AjaxOnly]
        [HttpPost]
        public IActionResult GetAdvertisementsViewersReportWithIdAndFilterType(GetAdvertisementsViewersReportWithIdAndFilterTypePVM getAdvertisementsViewersReportWithIdAndFilterTypePVM)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            try
            {
                serviceUrl = melkavanApiUrl + "/api/AdvertisementManagement/GetAdvertisementsViewersReportWithIdAndFilterType";
                //GetAdvertisementsViewersReportWithIdAndFilterTypePVM GetListOfAdvertisementPVM = new GetAdvertisementsViewersReportWithIdAndFilterTypePVM();
                //GetListOfAdvertisementPVM.FilterType = getAdvertisementsViewersReportWithIdAndFilterTypePVM.FilterType;
                //GetListOfAdvertisementPVM.AdvertisementId = getAdvertisementsViewersReportWithIdAndFilterTypePVM.AdvertisementId;
                responseApiCaller = new MelkavanApiCaller(serviceUrl).GetAdvertisementsViewersReportWithIdAndFilterType(getAdvertisementsViewersReportWithIdAndFilterTypePVM);
                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {
                            return Json(jsonResultWithRecordsObjectVM.Records);
                        }
                    }
                }
            }
            catch (Exception exc)
            { }
            return View("UserIndex");

        }

        [AjaxOnly]
        [HttpPost]
        public IActionResult GetUserDetailChartDataByUserId(long userId)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordObjectVM(new object() { });
            try
            {
                serviceUrl = melkavanApiUrl + "/api/AdvertisementUserProfileManagement/GetAdvertiserProfileWithAdvertiserId";
                GetAdvertiserProfileWithAdvertiserIdPVM getAdvertiserProfileWithAdvertiserIdPVM = new GetAdvertiserProfileWithAdvertiserIdPVM();
                getAdvertiserProfileWithAdvertiserIdPVM.AdvertiserId = userId;
                responseApiCaller = new MelkavanApiCaller(serviceUrl).GetAdvertiserProfileWithAdvertiserId(getAdvertiserProfileWithAdvertiserIdPVM);
                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;
                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {
                            return Json(jsonResultWithRecordsObjectVM.Record);
                        }
                    }
                }
            }
            catch (Exception exc)
            { }
            return View("UserIndex");

        }


        [AjaxOnly]
        [HttpPost]
        public IActionResult GetListOfNearAdvertisementsWithPropertyId(long propertyId,string recordType)
        {
            ViewData["Title"] = "لیست املاک";
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            int CountOfList = 0;
            List<AdvertisementVM> advertisementVMList = new List<AdvertisementVM>();
            try
            {
                serviceUrl = teniacoApiUrl + "/api/MelkavanPropertiesManagement/GetListOfNearAdvertisementsWithPropertyId";
                GetListOfNearAdvertisementsWithPropertyIdPVM getListOfNearAdvertisementsWithPropertyId = new GetListOfNearAdvertisementsWithPropertyIdPVM();

                getListOfNearAdvertisementsWithPropertyId.jtPageSize = 5;

                getListOfNearAdvertisementsWithPropertyId.PropertyId = propertyId;
                getListOfNearAdvertisementsWithPropertyId.HaveAddress = true;
                getListOfNearAdvertisementsWithPropertyId.HaveFeatures = true;
                getListOfNearAdvertisementsWithPropertyId.HaveFiles = true;
                getListOfNearAdvertisementsWithPropertyId.HaveDetails = true;
                getListOfNearAdvertisementsWithPropertyId.HaveCallers = true;
                getListOfNearAdvertisementsWithPropertyId.HaveViewers = true;
                getListOfNearAdvertisementsWithPropertyId.RecordType = recordType;
                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfNearAdvertisementsWithPropertyId(getListOfNearAdvertisementsWithPropertyId);
                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {
                            return Json(jsonResultWithRecordsObjectVM);
                        }
                    }
                }
            }
            catch (Exception exc)
            { }
            return View("UserIndex");
        }


        [HttpPost]
        [AjaxOnly]
        public IActionResult GetListOfMelkavanProperties(int pageNum, int pageSize)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MelkavanPropertiesManagement/GetListOfMelkavanProperties";

                GetListOfMelkavanPropertiesPVM getListOfMelkavanPropertiesPVM = new GetListOfMelkavanPropertiesPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    UserId = this.userId.Value,
                    jtSorting = "",
                    PropertyCodeName = "",
                    pageNum = pageNum,
                    pageSize = pageSize
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfMelkavanProperties(getListOfMelkavanPropertiesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<MelkavanPropertiesAdvanceSearchVM>>();


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