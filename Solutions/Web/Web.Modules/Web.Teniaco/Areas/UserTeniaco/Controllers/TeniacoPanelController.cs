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
using ApiCallers.PublicApiCaller;
using VM.Public;
using VM.PVM.Public;
using ApiCallers.ProjectsApiCaller;
using VM.Projects;
using VM.PVM.Projects;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class TeniacoPanelController : ExtraUsersController
    {
        public TeniacoPanelController(IHostEnvironment _hostEnvironment,
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
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            #region Old Map Comments ( Commented by sina )
            //if (ViewData["MapLayerCategoriesList"] == null)
            //{
            //    List<MapLayerCategoriesVM> mapLayerCategoriesVMList = new List<MapLayerCategoriesVM>();

            //    try
            //    {
            //        serviceUrl = teniacoApiUrl + "/api/MapLayerCategoriesManagement/GetListOfPropertiesPricesForMap";

            //        GetAllMapLayerCategoriesListPVM getAllMapLayerCategoriesListPVM = new GetAllMapLayerCategoriesListPVM()
            //        {
            //            //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
            //            //    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
            //        };

            //        responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllMapLayerCategoriesList(getAllMapLayerCategoriesListPVM);

            //        if (responseApiCaller.IsSuccessStatusCode)
            //        {
            //            jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

            //            if (jsonResultWithRecordsObjectVM != null)
            //            {
            //                if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
            //                {
            //                    #region Fill UserCreatorName

            //                    JArray jArray = jsonResultWithRecordsObjectVM.Records;
            //                    mapLayerCategoriesVMList = jArray.ToObject<List<MapLayerCategoriesVM>>();

            //                    #endregion
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception exc)
            //    { }

            //    ViewData["MapLayerCategoriesList"] = mapLayerCategoriesVMList;
            //}
            #endregion

            try // New map created by sina
            {
                string serviceUrl = teniacoApiUrl + "/api/MapLayerCategoriesManagement/GetListOfPropertiesPricesForMap";

                GetListOfPropertiesPricesForMapPVM getListOfPropertiesPricesForMapPVM = new GetListOfPropertiesPricesForMapPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    Platform = 3
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfPropertiesPricesForMap(getListOfPropertiesPricesForMapPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<PropertiesPricesForMapVM>>();


                            ViewData["Records"] = records;
                            ViewData["Result"] = jsonResultWithRecordsObjectVM.Result;

                        }
                    }
                }
            }
            catch (Exception exc)
            { }


            #region comments

            //if (ViewData["MapLayersList"] == null)
            //{
            //    List<MapLayersVM> mapLayersVM = new List<MapLayersVM>();

            //    try
            //    {
            //        serviceUrl = teniacoApiUrl + "/api/MapLayersManagement/GetAllMapLayersList";

            //        GetAllMapLayersListPVM getAllMapLayersListPVM = new GetAllMapLayersListPVM()
            //        {
            //            ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
            //                this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
            //        };

            //        responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllMapLayersList(getAllMapLayersListPVM);

            //        if (responseApiCaller.IsSuccessStatusCode)
            //        {
            //            jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

            //            if (jsonResultWithRecordsObjectVM != null)
            //            {
            //                if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
            //                {
            //                    JArray jArray = jsonResultWithRecordsObjectVM.Records;
            //                    var records = jArray.ToObject<List<MapLayersVM>>();

            //                    if (records != null)
            //                    {
            //                        mapLayersVM = records;

            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception exc)
            //    { }

            //    //dynamic expando = new ExpandoObject();
            //    //expando = mapLayersVM;

            //    ViewData["MapLayersList"] = mapLayersVM;
            //}


            #endregion

            if (ViewData["StatesList"] == null)
            {
                List<StatesVM> statesVMList = new List<StatesVM>();

                try
                {
                    serviceUrl = publicApiUrl + "/api/StatesManagement/GetListOfStates";

                    GetListOfStatesPVM getListOfStatesPVM = new GetListOfStatesPVM()
                    {
                        //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        //    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    };

                    responseApiCaller = new PublicApiCaller(serviceUrl).GetListOfStates(getListOfStatesPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                statesVMList = jArray.ToObject<List<StatesVM>>();

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["StatesList"] = statesVMList;
            }

            if (ViewData["CitiesList"] == null)
            {
                List<CitiesVM> citiesVMList = new List<CitiesVM>();

                try
                {
                    serviceUrl = publicApiUrl + "/api/CitiesManagement/GetAllCitiesListWithOutStrPolygon";

                    GetAllCitiesListPVM getAllCitiesListPVM = new GetAllCitiesListPVM()
                    {
                        //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        //    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    };

                    responseApiCaller = new PublicApiCaller(serviceUrl).GetAllCitiesList(getAllCitiesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                citiesVMList = jArray.ToObject<List<CitiesVM>>();

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["CitiesList"] = citiesVMList;
            }

            if (ViewData["ZonesList"] == null)
            {
                List<ZonesVM> zonesVMList = new List<ZonesVM>();

                try
                {
                    string serviceUrl = publicApiUrl + "/api/ZonesManagement/GetAllZonesListWithOutStrPolygon";

                    GetAllZonesListPVM getAllZonesListPVM = new GetAllZonesListPVM()
                    {
                        //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        //    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    };

                    responseApiCaller = new PublicApiCaller(serviceUrl).GetAllZonesList(getAllZonesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                zonesVMList = jArray.ToObject<List<ZonesVM>>();

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["ZonesList"] = zonesVMList;
            }

            if (ViewData["DistrictsList"] == null)
            {
                List<DistrictsVM> districtsVMList = new List<DistrictsVM>();

                try
                {
                    string serviceUrl = publicApiUrl + "/api/DistrictsManagement/GetAllDistrictsListWithOutStrPolygon";

                    GetAllDistrictsListPVM getAllDistrictsListPVM = new GetAllDistrictsListPVM()
                    {
                        //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        //    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    };

                    responseApiCaller = new PublicApiCaller(serviceUrl).GetAllDistrictsList(getAllDistrictsListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                districtsVMList = jArray.ToObject<List<DistrictsVM>>();

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["DistrictsList"] = districtsVMList;
            }

            if (ViewData["OutterDashboardPricesBlock"] == null)
            {
                OutterDashboardPricesBlockVM outterDashboardPricesBlockVM = new OutterDashboardPricesBlockVM();

                try
                {
                    string serviceUrl = teniacoApiUrl + "/api/TeniacoPanel/GetOutterDashboardPricesBlock";

                    GetOutterDashboardPricesBlockPVM getOutterDashboardPricesBlockPVM = new GetOutterDashboardPricesBlockPVM()
                    {
                        UserId = this.userId.Value,
                        //PersonId = 0
                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetOutterDashboardPricesBlock(getOutterDashboardPricesBlockPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                        if (jsonResultWithRecordObjectVM != null)
                        {
                            if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JObject jObject = jsonResultWithRecordObjectVM.Record;
                                var record = jObject.ToObject<OutterDashboardPricesBlockVM>();

                                outterDashboardPricesBlockVM = record;

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["OutterDashboardPricesBlock"] = outterDashboardPricesBlockVM;
            }

            if (ViewData["ProjectsList"] == null)
            {
                List<ConstructionProjectsVM> projectsVMList = new List<ConstructionProjectsVM>();

                try
                {
                    serviceUrl = projectsApiUrl + "/api/ConstructionProjectsManagement/GetListOfConstructionProjectsWithUserId";

                    GetConstructionProjectWithUserIdPVM getConstructionProjectWithUserIdPVM = new GetConstructionProjectWithUserIdPVM()
                    {
                        UserId = this.userId.Value
                    };

                    responseApiCaller = new ProjectsApiCaller(serviceUrl).GetListOfConstructionProjectsWithUserId(getConstructionProjectWithUserIdPVM);
                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                                {
                                    JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                    projectsVMList = jArray.ToObject<List<ConstructionProjectsVM>>();

                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                List<long> constructionProjectIds = new List<long>();

                foreach (var item in projectsVMList)
                {
                    constructionProjectIds.Add(item.ConstructionProjectId);
                }

                ViewData["ConstructionProjectIds"] = constructionProjectIds;
                ViewData["ProjectsList"] = projectsVMList;

              

            }

            //نمودار پیشرفت پروژه ها
            if (ViewData["ProgressDataList"] == null)
            {

                try
                {
                    string serviceUrl = projectsApiUrl + "/api/ConstructionProjectsManagement/GetAllProgressDataList";

                    GetConstructionProjectWithUserIdPVM getConstructionProjectWithUserIdPVM = new GetConstructionProjectWithUserIdPVM()
                    {
                        UserId = this.userId.Value,
                    };

                    responseApiCaller = new ProjectsApiCaller(serviceUrl).GetAllProgressDataList(getConstructionProjectWithUserIdPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                var records = jArray.ToObject<List<ProjectProgressDataVM>>();

                                ViewData["ProgressDataList"] = records;
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }
            }

            //پراکندگی سرمایه
            if (ViewData["MyFundsDispersionList"] == null)
            {
                List<MyFundsVM> myFundsVMList = new List<MyFundsVM>();
                double sumOfDispersion = 0;

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/TeniacoPanel/GetListOfMyFundsDispersion";

                    GetListOfMyFundsDispersionPVM getListOfMyFundsDispersionPVM = new GetListOfMyFundsDispersionPVM()
                    {
                        UserId = this.userId.Value
                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfMyFundsDispersion(getListOfMyFundsDispersionPVM);
                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                                {
                                    JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                    myFundsVMList = jArray.ToObject<List<MyFundsVM>>();

                                    myFundsVMList = myFundsVMList.OrderByDescending(f => f.MyFundPrice).ToList();

                                    try
                                    {
                                        if (myFundsVMList != null)
                                            if (myFundsVMList.Count > 0)
                                            {
                                                sumOfDispersion = (double)myFundsVMList.Sum(f => f.MyFundPrice);
                                            }
                                    }
                                    catch (Exception exc)
                                    { }
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["MyFundsDispersionList"] = myFundsVMList;
                ViewData["SumOfDispersion"] = sumOfDispersion;
            }

            //رشد سرمایه
            if (ViewData["MyFundsGrowthList"] == null)
            {
                List<MyFundsVM> myFundsVMList = new List<MyFundsVM>();
                double sumOfGrowth = 0;

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/TeniacoPanel/GetListOfMyFundsGrowth";

                    GetListOfMyFundsGrowthPVM getListOfMyFundsGrowthPVM = new GetListOfMyFundsGrowthPVM()
                    {
                        UserId = this.userId.Value
                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfMyFundsGrowth(getListOfMyFundsGrowthPVM);
                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;
                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                                {
                                    JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                    myFundsVMList = jArray.ToObject<List<MyFundsVM>>();

                                    myFundsVMList = myFundsVMList.OrderByDescending(f => f.MyFundPrice).ToList();

                                    try
                                    {
                                        if (myFundsVMList != null)
                                            if (myFundsVMList.Count > 0)
                                            {
                                                sumOfGrowth = (double)myFundsVMList.Sum(f => f.MyFundPrice);
                                            }
                                    }
                                    catch (Exception exc)
                                    { }
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["MyFundsGrowthList"] = myFundsVMList;
                ViewData["SumOfGrowth"] = sumOfGrowth;
            }

            //جزئیات داشبورد ( انحراف ها و تعداد مدارک تایید نشده و آگهی های نزدیک ، لیست مکالمات خوانده نشده و مدارک تایید نشده
            if (ViewData["DetailsForOuterDashboard"] == null)
            {
                DetailsForOuterDashboardVM detailsForOuterDashboardVM = new DetailsForOuterDashboardVM();

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/TeniacoPanel/GetDetailsForOuterDashboard";

                    GetDetailsForOuterDashboardPVM getDetailsForOuterDashboardPVM = new GetDetailsForOuterDashboardPVM()
                    {
                        UserId = this.userId.Value
                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetDetailsForOuterDashboard(getDetailsForOuterDashboardPVM);
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
                                    detailsForOuterDashboardVM = jObject.ToObject<DetailsForOuterDashboardVM>();
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["DetailsForOuterDashboard"] = detailsForOuterDashboardVM;
            }

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

        public IActionResult OldIndex()
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            if (ViewData["MapLayerCategoriesList"] == null)
            {
                List<MapLayerCategoriesVM> mapLayerCategoriesVMList = new List<MapLayerCategoriesVM>();

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/MapLayerCategoriesManagement/GetAllMapLayerCategoriesList";

                    GetAllMapLayerCategoriesListPVM getAllMapLayerCategoriesListPVM = new GetAllMapLayerCategoriesListPVM();

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllMapLayerCategoriesList(getAllMapLayerCategoriesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                mapLayerCategoriesVMList = jArray.ToObject<List<MapLayerCategoriesVM>>();

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["MapLayerCategoriesList"] = mapLayerCategoriesVMList;
            }

            if (ViewData["MapLayersList"] == null)
            {
                List<MapLayersVM> mapLayersVM = new List<MapLayersVM>();

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/MapLayersManagement/GetAllMapLayersList";

                    GetAllMapLayersListPVM getAllMapLayersListPVM = new GetAllMapLayersListPVM();

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllMapLayersList(getAllMapLayersListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                var records = jArray.ToObject<List<MapLayersVM>>();

                                if (records != null)
                                {
                                    mapLayersVM = records;

                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                //dynamic expando = new ExpandoObject();
                //expando = mapLayersVM;

                ViewData["MapLayersList"] = mapLayersVM;
            }

            return View("UserOldIndex");
        }

        [HttpPost]
        [AjaxOnly]
        public IActionResult GetAllMapLayersList(int mapLayerCategoryId)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MapLayersManagement/GetAllMapLayersList";

                GetAllMapLayersListPVM getAllMapLayersListPVM = new GetAllMapLayersListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                    //    this.domainsSettings.DomainSettingId),
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    MapLayerCategoryId = mapLayerCategoryId,
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllMapLayersList(getAllMapLayersListPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {
                            return Json(new
                            {
                                jsonResultWithRecordsObjectVM.Result,
                                jsonResultWithRecordsObjectVM.Records,
                                jsonResultWithRecordsObjectVM.TotalRecordCount
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

        [HttpPost]
        [AjaxOnly]
        public IActionResult GetZoneWithZoneId(int zoneId)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                string serviceUrl = publicApiUrl + "/api/ZonesManagement/GetZoneWithZoneId";

                GetZoneWithZoneIdPVM getZoneWithZoneIdPVM = new GetZoneWithZoneIdPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                    //    this.domainsSettings.DomainSettingId),
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    ZoneId = zoneId,
                };

                responseApiCaller = new PublicApiCaller(serviceUrl).GetZoneWithZoneId(getZoneWithZoneIdPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {
                            return Json(new
                            {
                                jsonResultWithRecordObjectVM.Result,
                                jsonResultWithRecordObjectVM.Record
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

        [HttpPost]
        [AjaxOnly]
        public IActionResult GetDistrictWithDistrictId(int districtId)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                string serviceUrl = publicApiUrl + "/api/DistrictsManagement/GetDistrictWithDistrictId";

                GetDistrictWithDistrictIdPVM getDistrictWithDistrictIdPVM = new GetDistrictWithDistrictIdPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                    //    this.domainsSettings.DomainSettingId),
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    DistrictId = districtId,
                };

                responseApiCaller = new PublicApiCaller(serviceUrl).GetDistrictWithDistrictId(getDistrictWithDistrictIdPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {
                            return Json(new
                            {
                                jsonResultWithRecordObjectVM.Result,
                                jsonResultWithRecordObjectVM.Record
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

        [HttpPost]
        [AjaxOnly]
        public IActionResult GetListOfPropertiesPricesForMap(int platform,//all, teniaco, melkavan
            long? priceFrom,
            long? priceTo,
            int? stateId,
            int? cityId,
            int? zoneId,
            int? districtId,
            int? typeOfUseId,
            int? propertyTypeId)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MapLayerCategoriesManagement/GetListOfPropertiesPricesForMap";

                GetListOfPropertiesPricesForMapPVM getListOfPropertiesPricesForMapPVM = new GetListOfPropertiesPricesForMapPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),

                    Platform = platform,
                    PriceFrom = priceFrom,
                    PriceTo = priceTo,
                    StateId = stateId,
                    CityId = cityId,
                    ZoneId = zoneId,
                    DistrictId = districtId,
                    PropertyTypeId = propertyTypeId
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfPropertiesPricesForMap(getListOfPropertiesPricesForMapPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {
                            #region Fill UserCreatorName

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<PropertiesPricesForMapVM>>();

                            //if (records.Count > 0)
                            //{
                            //    var userIdCreators = records.Where(i => i.UserIdCreator.HasValue).Select(u => u.UserIdCreator.Value).ToList();
                            //    var customUsers = consoleBusiness.GetCustomUsers(userIdCreators);

                            //    foreach (var record in records)
                            //    {
                            //        if (record.UserIdCreator.HasValue)
                            //        {
                            //            var customUser = customUsers.FirstOrDefault(c => c.UserId.Equals(record.UserIdCreator.Value));
                            //            if (customUser != null)
                            //            {
                            //                record.UserCreatorName = customUser.UserName;

                            //                if (!string.IsNullOrEmpty(customUser.Name))
                            //                    record.UserCreatorName += " " + customUser.Name;

                            //                if (!string.IsNullOrEmpty(customUser.Family))
                            //                    record.UserCreatorName += " " + customUser.Family;
                            //            }
                            //        }
                            //    }
                            //}

                            #endregion

                            return Json(new
                            {
                                Result = jsonResultWithRecordsObjectVM.Result,
                                Records = records,
                                //TotalRecordCount = jsonResultWithRecordsObjectVM.TotalRecordCount
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