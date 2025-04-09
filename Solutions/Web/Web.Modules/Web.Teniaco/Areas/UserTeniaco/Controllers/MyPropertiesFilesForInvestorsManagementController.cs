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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FrameWork;
using System.IO;
using System.Threading.Tasks;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class MyPropertiesFilesForInvestorsManagementController : ExtraUsersController
    {
        public MyPropertiesFilesForInvestorsManagementController(IHostEnvironment _hostEnvironment,
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
        public IActionResult GetListOfMyPropertiesFilesForInvestors(long PropertyId,string FileType)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/MyPropertiesFilesForInvestorsManagement/GetListOfMyPropertyFilesForInvestors";

                GetListOfMyPropertyFilesForInvestorsPVM getListOfMyPropertyFilesPVM = new GetListOfMyPropertyFilesForInvestorsPVM()
                {
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    //jtStartIndex = jtStartIndex,
                    //jtPageSize = jtPageSize,
                    PropertyId = PropertyId,
                    PropertyFileTitle = "",
                    PropertyFileType = FileType,
                    jtSorting = ""
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetListOfMyPropertyFilesForInvestors(getListOfMyPropertyFilesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordsObjectVM = responseApiCaller.JsonResultWithRecordsObjectVM;

                    if (jsonResultWithRecordsObjectVM != null)
                    {
                        if (jsonResultWithRecordsObjectVM.Result.Equals("OK"))
                        {

                            JArray jArray = jsonResultWithRecordsObjectVM.Records;
                            var records = jArray.ToObject<List<MyPropertyFilesVM>>();


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
        public IActionResult CompleteDeletePropertyFilesForInvestors(int PropertyFileId = 0,string fileType="")
        {
            JsonResultObjectVM jsonResultObjectVM = new JsonResultObjectVM(new object() { });

            PropertyFilesVM propertyFilesVM = new PropertyFilesVM();

            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            try
            {
                string serviceUrl = teniacoApiUrl + "/api/PropertiesFilesManagement/GetPropertyFileWithPropertyFileId";

                GetPropertyFileWithPropertyFileIdPVM getPropertyFileWithPropertyFileIdPVM = new GetPropertyFileWithPropertyFileIdPVM()
                {
                    PropertyFileId = PropertyFileId,
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).GetPropertyFileWithPropertyFileId(getPropertyFileWithPropertyFileIdPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {
                            JObject jObject = jsonResultWithRecordObjectVM.Record;
                            var record = jObject.ToObject<PropertyFilesVM>();


                            if (record != null)
                            {
                                propertyFilesVM = record;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            { }

            try
            {
                var domainSettings = consoleBusiness.GetDomainsSettingsWithUserId(this.userId.Value);

                string propertyFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\PropertiesFiles\\my.teniaco.com\\" + propertyFilesVM.PropertyId + "\\"+ fileType;

                if (propertyFilesVM != null)
                {
                    if (!string.IsNullOrEmpty(propertyFilesVM.PropertyFilePath))
                    {
                        if (System.IO.File.Exists(propertyFolder + "\\" + propertyFilesVM.PropertyFilePath))
                        {
                            System.IO.File.Delete(propertyFolder + "\\" + propertyFilesVM.PropertyFilePath);
                            System.Threading.Thread.Sleep(100);
                        }

                        switch (propertyFilesVM.PropertyFileExt.ToLower())
                        {
                            case ".txt":
                            case ".jpg":
                            case ".jpeg":
                            case ".png":
                            case ".bmp":
                                break;
                            case ".mp4":
                                break;
                        }
                        if (propertyFilesVM.PropertyFileExt.ToLower().Equals(".jpg") ||
                            propertyFilesVM.PropertyFileExt.ToLower().Equals(".jpeg") ||
                            propertyFilesVM.PropertyFileExt.ToLower().Equals(".png") ||
                            propertyFilesVM.PropertyFileExt.ToLower().Equals(".bmp"))

                        {
                            if (System.IO.File.Exists(propertyFolder + "\\thumb_" + propertyFilesVM.PropertyFilePath))
                            {
                                System.IO.File.Delete(propertyFolder + "\\thumb_" + propertyFilesVM.PropertyFilePath);
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }
                }

                string serviceUrl = teniacoApiUrl + "/api/PropertiesFilesManagement/CompleteDeletePropertyFiles";

                CompleteDeletePropertyFilesPVM completeDeletePropertyFilesPVM =
                    new CompleteDeletePropertyFilesPVM()
                    {
                        PropertyFileId = PropertyFileId,
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                        UserId = this.userId.Value
                    };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).CompleteDeletePropertyFiles(completeDeletePropertyFilesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultObjectVM = responseApiCaller.JsonResultObjectVM;

                    if (jsonResultObjectVM != null)
                    {
                        if (jsonResultObjectVM.Result.Equals("OK"))
                        {
                            return Json(new { Result = "OK" });
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



        // For Docs And Maps
        [AjaxOnly]
        [HttpPost, DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> AddToMyPropertyFilesForInvestors(List<PropertyFileUploadPVM> propertyFileUploadPVMList, int propertyId, string fileType)
        {
            JsonResultObjectVM jsonResultObjectVM = new JsonResultObjectVM(new object() { });

            List<PropertyFilesVM> propertyFilesVMList = new List<PropertyFilesVM>();

            try
            {
                if (propertyFileUploadPVMList != null)
                {
                    if (propertyFileUploadPVMList.Count > 0)
                    {
                        var domainSettings = consoleBusiness.GetDomainsSettingsWithUserId(this.userId.Value);

                        string propertyFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\PropertiesFiles\\my.teniaco.com\\" + propertyId + "\\"+ fileType;

                        foreach (var propertyFileUploadPVM in propertyFileUploadPVMList)
                        {
                            try
                            {
                                string fileName = "";

                                string ext = Path.GetExtension(propertyFileUploadPVM.File.FileName);
                                fileName = Guid.NewGuid().ToString() + ext;
                                using (var fileStream = new FileStream(propertyFolder + "\\" + fileName, FileMode.Create))
                                {
                                    await propertyFileUploadPVM.File.CopyToAsync(fileStream);
                                    System.Threading.Thread.Sleep(100);
                                }

                                //if (ext.Equals(".jpeg") ||
                                //    ext.Equals(".jpg") ||
                                //    ext.Equals(".png") ||
                                //    ext.Equals(".gif") ||
                                //    ext.Equals(".bmp"))
                                //{
                                //    ImageModify.ResizeImage(propertyFolder + "\\",
                                //        fileName,
                                //        120,
                                //        120);
                                //}
                                //else
                                //    if (ext.Equals(".mp4"))
                                //{

                                //}

                                var propertyFilesVM = new PropertyFilesVM()
                                {
                                    CreateEnDate = DateTime.Now,
                                    CreateTime = PersianDate.TimeNow,
                                    UserIdCreator = this.userId.Value,
                                    IsActivated = true,
                                    IsDeleted = false,
                                    PropertyFileExt = ext,
                                    PropertyFilePath = fileName,
                                    PropertyFileTitle = propertyFileUploadPVM.PropertyFileTitle,
                                    PropertyFileOrder = propertyFileUploadPVM.PropertyFileOrder,
                                    PropertyFileType = fileType,
                                    PropertyId = propertyId,
                                };

                                propertyFilesVMList.Add(propertyFilesVM);
                            }
                            catch (Exception exc)
                            { }
                        }
                    }
                }

                string serviceUrl = teniacoApiUrl + "/api/MyPropertiesFilesForInvestorsManagement/AddToMyPropertyFilesForInvestors";

                AddToMyPropertyFilesForInvestorsPVM addToPropertyFilesPVM = new AddToMyPropertyFilesForInvestorsPVM()
                {
                    PropertyFilesVMList = propertyFilesVMList,
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    PropertyId = propertyId
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).AddToMyPropertyFilesForInvestors(addToPropertyFilesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultObjectVM != null)
                    {
                        if (jsonResultObjectVM.Result.Equals("OK"))
                        {
                            return Json(new
                            {
                                Result = "OK",
                                Message = "آپلود انجام شد",
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



        // For Media
        [AjaxOnly]
        [HttpPost, DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> AddToMyPropertyMediaFilesForInvestors(List<PropertyFileUploadPVM> propertyFileUploadPVMList,
    long propertyId,
    List<int>? DeletedPhotosIDs,
    List<string>? DeletedPhotosPaths,
    bool? IsMainChanged)
        {
            JsonResultObjectVM jsonResultObjectVM = new JsonResultObjectVM(new object() { });

            List<PropertyFilesVM> propertyFilesVMList = new List<PropertyFilesVM>();

            try
            {
                if (propertyFileUploadPVMList != null)
                {
                    if (propertyFileUploadPVMList.Count > 0)
                    {
                        var domainSettings = consoleBusiness.GetDomainsSettingsWithUserId(this.domainsSettings.UserIdCreator.Value);

                        string propertyFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\PropertiesFiles\\my.teniaco.com\\" + propertyId + "\\Media";

                        foreach (var propertyFileUploadPVM in propertyFileUploadPVMList)
                        {
                            try
                            {
                                string fileName = "";

                                string ext = Path.GetExtension(propertyFileUploadPVM.File.FileName).ToLower();
                                fileName = Guid.NewGuid().ToString() + ext;
                                using (var fileStream = new FileStream(propertyFolder + "\\" + fileName, FileMode.Create))
                                {
                                    await propertyFileUploadPVM.File.CopyToAsync(fileStream);
                                    System.Threading.Thread.Sleep(100);
                                }

                                if (ext.Equals(".jpeg") ||
                                    ext.Equals(".jpg") ||
                                    ext.Equals(".webp") ||
                                    ext.Equals(".png") ||
                                    ext.Equals(".gif") ||
                                    ext.Equals(".bmp"))
                                {
                                    ImageModify.ResizeImage(propertyFolder + "\\",
                                        fileName,
                                        120,
                                        120);
                                }


                                var propertyFilesVM = new PropertyFilesVM()
                                {
                                    CreateEnDate = DateTime.Now,
                                    CreateTime = PersianDate.TimeNow,
                                    UserIdCreator = this.userId.Value,
                                    IsActivated = true,
                                    IsDeleted = false,
                                    PropertyFileExt = ext,
                                    PropertyFilePath = fileName,
                                    PropertyFileTitle = propertyFileUploadPVM.PropertyFileTitle,
                                    PropertyFileOrder = propertyFileUploadPVM.PropertyFileOrder,
                                    PropertyFileType = "media",
                                    PropertyId = propertyId,
                                };


                                propertyFilesVMList.Add(propertyFilesVM);
                            }
                            catch (Exception exc)
                            { }
                        }
                    }
                }



                //for removing photos
                if (DeletedPhotosPaths != null)
                {
                    foreach (string path in DeletedPhotosPaths)
                    {
                        string fullPath = hostEnvironment.ContentRootPath + "\\wwwroot" + path.Replace("/", "\\");

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }

                        string thumbPath = fullPath.Replace("\\Media\\", "\\Media\\thumb_");

                        if (System.IO.File.Exists(thumbPath))
                        {
                            System.IO.File.Delete(thumbPath);
                        }
                    }
                }

                string serviceUrl = teniacoApiUrl + "/api/MyPropertiesFilesForInvestorsManagement/AddMediaToPropertyFilesForInvestors";

                AddToPropertyFilesPVM addToPropertyFilesPVM = new AddToPropertyFilesPVM()
                {
                    PropertyFilesVMList = propertyFilesVMList,
                    ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                    DeletedPhotosIDs = DeletedPhotosIDs,
                    IsMainChanged = IsMainChanged,
                    PropertyId = propertyId
                };

                responseApiCaller = new TeniacoApiCaller(serviceUrl).AddToPropertyFiles(addToPropertyFilesPVM);

                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultObjectVM != null)
                    {
                        if (jsonResultObjectVM.Result.Equals("OK"))
                        {
                            return Json(new
                            {
                                Result = "OK",
                                Message = "تغییرات انجام شد",
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
