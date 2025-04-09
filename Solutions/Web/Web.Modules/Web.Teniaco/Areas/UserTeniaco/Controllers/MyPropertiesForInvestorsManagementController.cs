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
using Web.Teniaco.Areas.AdminTeniaco.Controllers;
using ApiCallers.MelkavanApiCaller;
using ApiCallers.PublicApiCaller;
using FrameWork;
using VM.Melkavan;
using VM.Public;
using VM.PVM.Melkavan;
using VM.PVM.Public;
using VM.Teniaco.VM.Teniaco;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class MyPropertiesForInvestorsManagementController : ExtraUsersController
    {
        public MyPropertiesForInvestorsManagementController(IHostEnvironment _hostEnvironment,
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
            string serviceUrl = "";
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });


            //نوع ملک
            if (ViewData["PropertyTypesList"] == null)
            {
                List<PropertyTypesVM> propertyTypesVMList = new List<PropertyTypesVM>();

                GetAllPropertyTypesListPVM getAllPropertyTypesListPVM = new GetAllPropertyTypesListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                };

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/PropertyTypesManagement/GetAllPropertyTypesList";

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllPropertyTypesList(getAllPropertyTypesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                propertyTypesVMList = jArray.ToObject<List<PropertyTypesVM>>();


                                if (propertyTypesVMList != null)
                                    if (propertyTypesVMList.Count > 0)
                                    {
                                        //var userIdCreators = records.Where(i => i.UserIdCreator.HasValue).Select(u => u.UserIdCreator.Value).ToList();
                                        //var customUsers = consoleBusiness.GetCustomUsers(userIdCreators);

                                        //foreach (var record in records)
                                        //{
                                        //    if (record.UserIdCreator.HasValue)
                                        //    {
                                        //        var customUser = customUsers.FirstOrDefault(c => c.UserId.Equals(record.UserIdCreator.Value));
                                        //        if (customUser != null)
                                        //        {
                                        //            record.UserCreatorName = customUser.UserName;

                                        //            if (!string.IsNullOrEmpty(customUser.Name))
                                        //                record.UserCreatorName += " " + customUser.Name;

                                        //            if (!string.IsNullOrEmpty(customUser.Family))
                                        //                record.UserCreatorName += " " + customUser.Family;
                                        //        }
                                        //    }
                                        //}

                                        //statesVMList = records;
                                    }

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["PropertyTypesList"] = propertyTypesVMList;
            }

            //نوع کاربری
            if (ViewData["TypeOfUsesList"] == null)
            {
                List<TypeOfUsesVM> typeOfUsesVMList = new List<TypeOfUsesVM>();
                GetAllTypeOfUsesListPVM getAllTypeOfUsesListPVM = new GetAllTypeOfUsesListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                };

                try
                {
                    serviceUrl = teniacoApiUrl + "/api/TypeOfUsesManagement/GetAllTypeOfUsesList";

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllTypeOfUsesList(getAllTypeOfUsesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                typeOfUsesVMList = jArray.ToObject<List<TypeOfUsesVM>>();


                                if (typeOfUsesVMList != null)
                                    if (typeOfUsesVMList.Count > 0)
                                    {
                                        //var userIdCreators = records.Where(i => i.UserIdCreator.HasValue).Select(u => u.UserIdCreator.Value).ToList();
                                        //var customUsers = consoleBusiness.GetCustomUsers(userIdCreators);

                                        //foreach (var record in records)
                                        //{
                                        //    if (record.UserIdCreator.HasValue)
                                        //    {
                                        //        var customUser = customUsers.FirstOrDefault(c => c.UserId.Equals(record.UserIdCreator.Value));
                                        //        if (customUser != null)
                                        //        {
                                        //            record.UserCreatorName = customUser.UserName;

                                        //            if (!string.IsNullOrEmpty(customUser.Name))
                                        //                record.UserCreatorName += " " + customUser.Name;

                                        //            if (!string.IsNullOrEmpty(customUser.Family))
                                        //                record.UserCreatorName += " " + customUser.Family;
                                        //        }
                                        //    }
                                        //}

                                        //statesVMList = records;
                                    }

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["TypeOfUsesList"] = typeOfUsesVMList;
            }

            //نوع سند
            if (ViewData["DocumentTypesList"] == null)
            {
                List<DocumentTypesVM> documentTypesVMList = new List<DocumentTypesVM>();

                GetAllDocumentTypesListPVM getAllDocumentTypesListPVM = new GetAllDocumentTypesListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    ////ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                    ////       this.domainsSettings.DomainSettingId),
                };
                try
                {
                    serviceUrl = teniacoApiUrl + "/api/DocumentTypesManagement/GetAllDocumentTypesList";

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllDocumentTypesList(getAllDocumentTypesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                documentTypesVMList = jArray.ToObject<List<DocumentTypesVM>>();


                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["DocumentTypesList"] = documentTypesVMList;
            }


            //نوع ریشه سند
            if (ViewData["DocumentRootTypesList"] == null)
            {
                List<DocumentRootTypesVM> documentRootTypesVMList = new List<DocumentRootTypesVM>();

                GetAllDocumentRootTypesListPVM getAllDocumentRootTypesListPVM = new GetAllDocumentRootTypesListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    ////ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                    ////       this.domainsSettings.DomainSettingId),
                };
                try
                {
                    serviceUrl = teniacoApiUrl + "/api/DocumentRootTypesManagement/GetAllDocumentRootTypesList";

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllDocumentRootTypesList(getAllDocumentRootTypesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                documentRootTypesVMList = jArray.ToObject<List<DocumentRootTypesVM>>();


                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["DocumentRootTypesList"] = documentRootTypesVMList;
            }


            //نوع مالکیت 
            if (ViewData["DocumentOwnershipTypesList"] == null)
            {
                List<DocumentOwnershipTypesVM> documentOwnershipTypesVMList = new List<DocumentOwnershipTypesVM>();

                GetAllDocumentOwnershipTypesListPVM getAllDocumentOwnershipTypesListPVM = new GetAllDocumentOwnershipTypesListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    ////ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                    ////       this.domainsSettings.DomainSettingId),
                };
                try
                {
                    serviceUrl = teniacoApiUrl + "/api/DocumentOwnershipTypesManagement/GetAllDocumentOwnershipTypesList";

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).GetAllDocumentOwnershipTypesList(getAllDocumentOwnershipTypesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                documentOwnershipTypesVMList = jArray.ToObject<List<DocumentOwnershipTypesVM>>();


                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["DocumentOwnershipTypesList"] = documentOwnershipTypesVMList;
            }


            //عمر بنا
            if (ViewData["BuildingLifesList"] == null)
            {
                List<BuildingLifesVM> buildingLifesList = new List<BuildingLifesVM>();

                GetAllBuildingLifesListPVM getAllBuildingLifesListPVM = new GetAllBuildingLifesListPVM()
                {
                    //ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                    //this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                };


                try
                {
                    serviceUrl = melkavanApiUrl + "/api/BuildingLifesManagement/GetAllBuildingLifesList";

                    responseApiCaller = new MelkavanApiCaller(serviceUrl).GetAllBuildingLifesList(getAllBuildingLifesListPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                        if (jsonResultWithRecordsObjectVM != null)
                        {
                            if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                            {
                                #region Fill UserCreatorName

                                JArray jArray = jsonResultWithRecordsObjectVM.Records;
                                buildingLifesList = jArray.ToObject<List<BuildingLifesVM>>();

                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exc)
                { }

                ViewData["BuildingLifesList"] = buildingLifesList;
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


            //استان
            if (ViewData["StatesList"] == null)
            {
                List<StatesVM> statesVMList = new List<StatesVM>();

                try
                {
                    serviceUrl = publicApiUrl + "/api/StatesManagement/GetListOfStates";

                    GetListOfStatesPVM getListOfStatesPVM = new GetListOfStatesPVM()
                    {
                        //ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                        //    this.domainsSettings.DomainSettingId),
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
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

            //بخش
            if (ViewData["CitiesList"] == null)
            {
                List<CitiesVM> citiesVMList = new List<CitiesVM>();

                try
                {
                    serviceUrl = publicApiUrl + "/api/CitiesManagement/GetAllCitiesListWithOutStrPolygon";

                    GetAllCitiesListPVM getAllCitiesListPVM = new GetAllCitiesListPVM()
                    {
                        //ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                        //    this.domainsSettings.DomainSettingId),
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
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

            //شهر یا منطقه
            if (ViewData["ZonesList"] == null)
            {
                List<ZonesVM> zonesVMList = new List<ZonesVM>();

                try
                {
                    serviceUrl = publicApiUrl + "/api/ZonesManagement/GetAllZonesListWithOutStrPolygon";

                    GetAllZonesListPVM getAllZonesListPVM = new GetAllZonesListPVM()
                    {
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                        this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                        //ChildsUsersIds = consoleBusiness.GetDomainChildUserIdsWithStoredProcedure(this.userId.Value,
                        //   this.domainsSettings.DomainSettingId),
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

            // ناحیه
            if (ViewData["DistrictsList"] == null)
            {
                List<DistrictsVM> districtsVMList = new List<DistrictsVM>();

                try
                {
                    serviceUrl = publicApiUrl + "/api/DistrictsManagement/GetAllDistrictsList";

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
        public IActionResult GetListOfMyPropertiesForInvestors(int pageNum, int pageSize = 5)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MyPropertiesForInvestorsManagement/GetListOfMyPropertiesForInvestors";

                GetListOfMyPropertiesForInvestorsPVM getListOfMyPropertiesPVM = new GetListOfMyPropertiesForInvestorsPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    UserId = this.userId.Value,
                    jtSorting = "",
                    PropertyCodeName = "",
                    pageNum = pageNum,
                    pageSize = pageSize
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfMyPropertiesForInvestors(getListOfMyPropertiesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<MyPropertiesForInvestorsAdvanceSearchVM>>();


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



        [HttpPost]
        [AjaxOnly]
        public IActionResult AddToPropertiesForInvestors(PropertiesVM propertiesVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                propertiesVM.CreateEnDate = DateTime.Now;
                propertiesVM.CreateTime = PersianDate.TimeNow;
                propertiesVM.UserIdCreator = this.userId.Value;
                propertiesVM.IsActivated = true;
                propertiesVM.IsDeleted = false;
                if (propertiesVM.OfferPrice.HasValue)
                {
                    propertiesVM.OfferPrice = long.Parse(propertiesVM.StrOfferPrice.Replace(",", ""));
                    propertiesVM.CalculatedOfferPrice = (long)double.Parse(propertiesVM.StrCalculatedOfferPrice.Replace(",", ""));
                }


                try
                {
                    if (propertiesVM.PropertiesDetailsVM != null)
                        if (!propertiesVM.PropertiesDetailsVM.BuildingLifeId.HasValue)
                            propertiesVM.PropertiesDetailsVM.BuildingLifeId = 0;
                    ModelState.Remove("PropertiesDetailsVM.BuildingLifeId");
                }
                catch (Exception exc)
                {
                    propertiesVM.PropertiesDetailsVM.BuildingLifeId = 0;
                    ModelState.Remove("PropertiesDetailsVM.BuildingLifeId");
                }

                ModelState.Remove("PropertyBuyersVM");
                ModelState.Remove("PropertyOwnersVM");
                ModelState.Remove("PropertiesDetailsVM.Exchangeable");
                ModelState.Remove("PropertiesDetailsVM.Participable");


                propertiesVM.PropertyOwnersVM = new()
                {
                    new PropertyOwnersVM
                    {
                        OwnerId = this.userId.Value,
                        PropertyOwnerId = 0,
                        PropertyId = 0,
                        Share = 6,
                        SharePercent = 100,
                        OwnerType = "users"
                    }
                };


                propertiesVM.PropertyBuyersVM = new()
                {
                    new PropertyBuyersVM
                    {
                        PropertyId = 0,
                        PersonId = 0
                    }
                };

                if (ModelState.IsValid)
                {
                    string serviceUrl = teniacoApiUrl + "/api/PropertiesManagement/AddToProperties";

                    AddToPropertiesPVM addToFormPVM = new AddToPropertiesPVM()
                    {
                        PropertiesVM = propertiesVM,
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),

                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).AddToProperties(addToFormPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                        if (jsonResultWithRecordObjectVM != null)
                        {
                            if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                            {
                                JObject jObject = jsonResultWithRecordObjectVM.Record;
                                var record = jObject.ToObject<PropertiesVM>();

                                if (record != null)
                                {
                                    propertiesVM = record;

                                    #region create needed folders

                                    if (propertiesVM.PropertyId > 0)
                                    {
                                        #region create domain folder
                                        string propertiesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\PropertiesFiles\\";
                                        try
                                        {
                                            if (!System.IO.Directory.Exists(propertiesFolder + "\\" + "my.teniaco.com"))
                                            {
                                                System.IO.Directory.CreateDirectory(propertiesFolder + "\\" + "my.teniaco.com");
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        catch (Exception exc)
                                        { }

                                        #endregion

                                        #region create root folder for this property
                                        try
                                        {
                                            if (!System.IO.Directory.Exists(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId))
                                            {
                                                System.IO.Directory.CreateDirectory(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId);
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        catch (Exception exc)
                                        { }
                                        #endregion

                                        #region create maps folder
                                        try
                                        {
                                            if (!System.IO.Directory.Exists(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId + "\\Maps"))
                                            {
                                                System.IO.Directory.CreateDirectory(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId + "\\Maps");
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        catch (Exception exc)
                                        { }
                                        #endregion

                                        #region create docs folder
                                        try
                                        {
                                            if (!System.IO.Directory.Exists(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId + "\\Docs"))
                                            {
                                                System.IO.Directory.CreateDirectory(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId + "\\Docs");
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        catch (Exception exc)
                                        { }
                                        #endregion

                                        #region create media folder
                                        if (!System.IO.Directory.Exists(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId + "\\Media"))
                                        {
                                            System.IO.Directory.CreateDirectory(propertiesFolder + "\\" + "my.teniaco.com" + "\\" + propertiesVM.PropertyId + "\\Media");
                                            System.Threading.Thread.Sleep(100);
                                        }
                                        #endregion
                                    }

                                    #endregion

                                    return Json(new
                                    {
                                        Result = "OK",
                                        Record = propertiesVM,
                                        Message = "تعریف انجام شد",
                                    });
                                }
                            }
                            else
                                if (jsonResultWithRecordObjectVM.Result.Equals("ERROR") &&
                                jsonResultWithRecordObjectVM.Message.Equals("DuplicateProperty"))
                            {
                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "رکورد تکراری است"
                                });
                            }
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




        [HttpPost]
        [AjaxOnly]
        public IActionResult GetPropertyDetailsWithPropertyId(long PropertyId)
        {
            PropertiesVM propertiesVM = new PropertiesVM();
            propertiesVM.PropertyAddressVM = new PropertyAddressVM();
            propertiesVM.PropertiesPricesHistoriesVM = new List<PropertiesPricesHistoriesVM>();

            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/PropertiesManagement/GetPropertyWithPropertyId";

                GetPropertyWithPropertyIdPVM getPropertyWithPropertyIdPVM = new GetPropertyWithPropertyIdPVM()
                {
                    PropertyId = PropertyId,
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetPropertyWithPropertyId(getPropertyWithPropertyIdPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {
                            JObject jObject = jsonResultWithRecordObjectVM.Record;
                            var record = jObject.ToObject<PropertiesVM>();


                            if (record != null)
                            {
                                propertiesVM = record;

                                propertiesVM.StrOfferPrice = propertiesVM.PropertiesPricesHistoriesVM.FirstOrDefault().OfferPrice.ToString();
                                propertiesVM.StrCalculatedOfferPrice = propertiesVM.PropertiesPricesHistoriesVM.FirstOrDefault().CalculatedOfferPrice.ToString();

                                propertiesVM.PropertiesPricesHistoriesVM.FirstOrDefault().StrOfferPrice = propertiesVM.PropertiesPricesHistoriesVM.FirstOrDefault().OfferPrice.ToString();
                                propertiesVM.PropertiesPricesHistoriesVM.FirstOrDefault().StrCalculatedOfferPrice = propertiesVM.PropertiesPricesHistoriesVM.FirstOrDefault().CalculatedOfferPrice.ToString();
                            }


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
            { }

            return Json(new
            {
                Result = jsonResultWithRecordObjectVM.Result,
                Message = "خطا"
            });
        }


        [HttpPost]
        [AjaxOnly]
        public IActionResult UpdateMyPropertiesForInvestors(PropertiesVM propertiesVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                propertiesVM.EditEnDate = DateTime.Now;
                propertiesVM.EditTime = PersianDate.TimeNow;
                propertiesVM.UserIdEditor = this.userId.Value;
                if (propertiesVM.OfferPrice.HasValue)
                {
                    propertiesVM.OfferPrice = long.Parse(propertiesVM.StrOfferPrice.Replace(",", ""));
                    propertiesVM.CalculatedOfferPrice = (long)double.Parse(propertiesVM.StrCalculatedOfferPrice.Replace(",", ""));
                }
                try
                {
                    if (propertiesVM.PropertiesDetailsVM != null)
                        if (!propertiesVM.PropertiesDetailsVM.BuildingLifeId.HasValue)
                            propertiesVM.PropertiesDetailsVM.BuildingLifeId = 0;
                    ModelState.Remove("PropertiesDetailsVM.BuildingLifeId");

                }
                catch (Exception exc)
                {
                    propertiesVM.PropertiesDetailsVM.BuildingLifeId = 0;
                    ModelState.Remove("PropertiesDetailsVM.BuildingLifeId");
                }

                ModelState.Remove("PropertyBuyersVM");
                ModelState.Remove("PropertyOwnersVM");
                ModelState.Remove("PropertiesDetailsVM.Participable");
                ModelState.Remove("PropertiesDetailsVM.Exchangeable");


                propertiesVM.PropertyOwnersVM = new()
                {
                    new PropertyOwnersVM
                    {
                        OwnerId = this.userId.Value,
                        PropertyOwnerId = 0,
                        PropertyId = 0,
                        Share = 6,
                        SharePercent = 100,
                        OwnerType = "users"
                    }
                };


                propertiesVM.PropertyBuyersVM = new()
                {
                    new PropertyBuyersVM
                    {
                        PropertyId = 0,
                        PersonId = 0
                    }
                };


                if (ModelState.IsValid)
                {
                    string serviceUrl = teniacoApiUrl + "/api/MyPropertiesForInvestorsManagement/UpdateMyPropertiesForInvestors";

                    UpdateMyPropertiesForInvestorsPVM updateFormPVM = new UpdateMyPropertiesForInvestorsPVM()
                    {
                        PropertiesVM = propertiesVM,
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                        UserId = this.userId.Value,
                    };

                    responseApiCaller = new TeniacoApiCaller(serviceUrl).UpdateMyPropertiesForInvestors(updateFormPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                        if (jsonResultWithRecordObjectVM != null)
                        {
                            if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                            {
                                JObject jObject = jsonResultWithRecordObjectVM.Record;
                                propertiesVM = jObject.ToObject<PropertiesVM>();

                                #region Fill UserCreatorName

                                if (propertiesVM.UserIdCreator.HasValue)
                                {
                                    var customUser = consoleBusiness.GetCustomUser(propertiesVM.UserIdCreator.Value);

                                    if (customUser != null)
                                    {
                                        propertiesVM.UserCreatorName = customUser.UserName;

                                        if (!string.IsNullOrEmpty(customUser.Name))
                                            propertiesVM.UserCreatorName += " " + customUser.Name;

                                        if (!string.IsNullOrEmpty(customUser.Family))
                                            propertiesVM.UserCreatorName += " " + customUser.Family;
                                    }
                                }

                                #endregion

                                return Json(new
                                {
                                    Result = jsonResultWithRecordObjectVM.Result,
                                    Record = propertiesVM,
                                    Message = "ویرایش انجام شد",
                                });
                            }
                            else
                            {
                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                        }
                    }

                    return Json(new
                    {
                        Result = jsonResultWithRecordObjectVM.Result,
                        Record = propertiesVM,
                    });
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



        [HttpPost]
        [AjaxOnly]
        public IActionResult AddToDistricts(DistrictsVM districtsVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });
            try
            {
                districtsVM.CreateEnDate = DateTime.Now;
                districtsVM.CreateTime = PersianDate.TimeNow;
                districtsVM.UserIdCreator = this.userId.Value;

                if (ModelState.IsValid)
                {
                    string serviceUrl = publicApiUrl + "/api/DistrictsManagement/AddToDistricts";

                    AddToDistrictsPVM addToDistrictsPVM = new AddToDistrictsPVM()
                    {
                        DistrictsVM = districtsVM,
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    };

                    responseApiCaller = new PublicApiCaller(serviceUrl).AddToDistricts(addToDistrictsPVM);

                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                        if (jsonResultWithRecordObjectVM != null)
                        {
                            if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                            {
                                JObject jObject = jsonResultWithRecordObjectVM.Record;
                                var record = jObject.ToObject<DistrictsVM>();

                                if (record != null)
                                {
                                    districtsVM = record;

                                    return Json(new
                                    {
                                        Result = "OK",
                                        Record = districtsVM,
                                        Message = "تعریف انجام شد",
                                    });
                                }
                            }
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