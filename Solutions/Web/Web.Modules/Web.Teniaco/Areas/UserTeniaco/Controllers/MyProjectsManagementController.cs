using ApiCallers.ProjectsApiCaller;
using AutoMapper;
using CustomAttributes;
using FrameWork;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VM.Base;
using VM.Projects;
using VM.PVM.Projects;
using VM.Teniaco;
using Web.Core.Controllers;

namespace Web.Teniaco.Areas.UserTeniaco.Controllers
{
    [Area("UserTeniaco")]
    public class MyProjectsManagementController : ExtraUsersController
    {
        public MyProjectsManagementController(IHostEnvironment _hostEnvironment,
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
            ViewData["projectsVMList"] = projectsVMList;
            ViewData["DomainName"] = this.domain;
            ViewData["UserId"] = this.userId;
            return View("UserIndex");

        }



        #region دریافت گزارشات  روزانه پروژه

        [AjaxOnly]
        [HttpPost]
        public IActionResult GetConstructionProjectsDailyDataByConstructionProjectId(long projectId, int? pageNum = 1)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            try
            {
                serviceUrl = projectsApiUrl + "/api/ConstructionProjectsManagement/GetConstructionProjectsDailyDataByConstructionProjectId";
                GetConstructionProjectsDailyDataByProjectIdPVM getConstructionProjectsDailyDataByProjectIdPVM = new GetConstructionProjectsDailyDataByProjectIdPVM()
                {
                    jtPageSize = 30,
                    jtStartIndex = pageNum,
                    ConstructionProjectId = projectId,
                };
                responseApiCaller = new ProjectsApiCaller(serviceUrl).GetConstructionProjectsDailyDataByConstructionProjectId(getConstructionProjectsDailyDataByProjectIdPVM);
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
        #endregion

        #region دیتای قراردادها


        [AjaxOnly]
        [HttpPost]
        public IActionResult GetAgreementDataByAgreementTypeAndConstructionProjectId(GetAgreementDataByAgreementTypeAndConstructionProjectIdPVM
              getAgreementDataByAgreementTypeAndConstructionProjectIdPVM)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                serviceUrl = projectsApiUrl + "/api/ConstructionProjectsManagement/GetAgreementDataByAgreementTypeAndConstructionProjectId";
                GetAgreementDataByAgreementTypeAndConstructionProjectIdPVM agreementDataByTypeAndConstructionProjectIdPVM = new GetAgreementDataByAgreementTypeAndConstructionProjectIdPVM()
                {

                    ConstructionProjectId = getAgreementDataByAgreementTypeAndConstructionProjectIdPVM.ConstructionProjectId,
                    ContractType = getAgreementDataByAgreementTypeAndConstructionProjectIdPVM.ContractType,
                    HaveAttachments = true,
                    HaveConversations = true,
                    UserId = this.userId
                };
                responseApiCaller = new ProjectsApiCaller(serviceUrl).GetAgreementDataByAgreementTypeAndConstructionProjectId(agreementDataByTypeAndConstructionProjectIdPVM);
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
        #endregion

        #region دیتای الحاقیه ها


        [AjaxOnly]
        [HttpPost]
        public IActionResult GetAttachementsByAgreementIdAndTableTitle(GetAttachementsByAgreementIdAndTableTitlePVM
         getAttachementsByAgreementIdAndTableTitlePVM)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });
            try
            {
                GetAttachementsByAgreementIdAndTableTitlePVM getAttachementsByAgreementIdAndTableTitle = new GetAttachementsByAgreementIdAndTableTitlePVM
                {
                    UserId = this.userId,
                    AgreeemntId = getAttachementsByAgreementIdAndTableTitlePVM.AgreeemntId,
                    TableTitle = getAttachementsByAgreementIdAndTableTitlePVM.TableTitle
                };


                serviceUrl = projectsApiUrl + "/api/AttachementFilesManagement/GetAttachementsByAgreementIdAndTableTitle";

                responseApiCaller = new ProjectsApiCaller(serviceUrl).GetAttachementsByAgreementIdAndTableTitle(getAttachementsByAgreementIdAndTableTitle);
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

        #endregion

        #region وضعیت قرارداد یا الحاقیه



        [AjaxOnly]
        [HttpPost]
        public IActionResult ConfirmAgreementsAndAttachmentsByAggreementIdAndAttachmentId(ConfirmAgreementsAndAttachmentsByAggreementIdAndAttachmentIdPVM
            ConfirmAgreementsAndAttachmentsByAggreementIdAndAttachmentIdPVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });
            try
            {
                serviceUrl = projectsApiUrl + "/api/FileStatesLogsManagement/AddToFileStatesLogs";
                var fileStatesLogsVM = new FileStatesLogsVM
                {
                    TableTitle = ConfirmAgreementsAndAttachmentsByAggreementIdAndAttachmentIdPVM.ContractType,
                    RecordId = ConfirmAgreementsAndAttachmentsByAggreementIdAndAttachmentIdPVM.TargetId,
                    FileStateId = 4,
                };
                AddToFileStatesLogsPVM addToFileStatesLogsPVM = new AddToFileStatesLogsPVM()
                {
                    FileStatesLogsVM = fileStatesLogsVM
                };
                responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToFileStatesLogs(addToFileStatesLogsPVM);
                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;
                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {
                            return Json(jsonResultWithRecordObjectVM);
                        }
                    }
                }
            }
            catch (Exception exc)
            { }
            return View("UserIndex");
        }

        #endregion

        #region آپلود قایل برای قرارداد
        [AjaxOnly]
        [HttpPost, DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> AddAgreement(AddAgreementPVM addAgreementPVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });
            try
            {
                string serviceUrl = "";
                string fileName = "";
                string fileType = "";
                string ext = Path.GetExtension(addAgreementPVM.File.FileName);
                fileName = Guid.NewGuid().ToString() + ext;

                if (ext.Equals(".jpeg") ||
                    ext.Equals(".jpg") ||
                    ext.Equals(".png") ||
                    ext.Equals(".gif") ||
                    ext.Equals(".bmp"))
                {
                    fileType = "media";

                }
                else if (ext.Equals(".pdf"))
                {
                    fileType = "pdf";

                }
                else if (ext.Equals(".xls") ||
                          ext.Equals(".xlsx"))
                {
                    fileType = "excel";

                }
                else if (ext.Equals(".docx") ||
                          ext.Equals(".doc"))
                {
                    fileType = "word";
                }
                else
                {
                    fileType = "other";
                }
                switch (addAgreementPVM.ContractType)
                {
                    case "PartnershipAgreement":
                        PartnershipAgreementsVM partnershipAgreementsVM = new PartnershipAgreementsVM();
                        partnershipAgreementsVM = new PartnershipAgreementsVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            PartnershipAgreementFileExt = ext,
                            PartnershipAgreementFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            PartnershipAgreementDescription = addAgreementPVM.AgreementDescription,
                            PartnershipAgreementNumber = 0,
                            PartnershipAgreementFileOrder = 0,
                            PartnershipAgreementFileType = fileType,
                            PartnershipAgreementTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/PartnershipAgreementsManagement/AddToPartnershipAgreements";
                        AddToPartnershipAgreementsPVM addToPartnershipAgreementsPVM = new AddToPartnershipAgreementsPVM()
                        {
                            PartnershipAgreementsVM = partnershipAgreementsVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToPartnershipAgreements(addToPartnershipAgreementsPVM);

                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    partnershipAgreementsVM = jObject.ToObject<PartnershipAgreementsVM>();


                                }
                            }
                        }
                        try
                        {
                            if (partnershipAgreementsVM.PartnershipAgreementId == 0)
                            {
                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string PartnershipAgreementFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + partnershipAgreementsVM.ConstructionProjectId + "\\PartnershipAgreementFiles\\" + partnershipAgreementsVM.PartnershipAgreementId + "\\Image";
                            if (!Directory.Exists(PartnershipAgreementFilesFolder))
                            {
                                Directory.CreateDirectory(PartnershipAgreementFilesFolder);
                            }
                            using (var fileStream = new FileStream(PartnershipAgreementFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }

                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = partnershipAgreementsVM.PartnershipAgreementDescription,
                                AgreementFileExt = partnershipAgreementsVM.PartnershipAgreementFileExt,
                                AgreementFilePath = partnershipAgreementsVM.PartnershipAgreementFilePath,
                                AgreementId = partnershipAgreementsVM.PartnershipAgreementId,
                                ConstructionProjectId = partnershipAgreementsVM.ConstructionProjectId,
                                AgreementTitle = partnershipAgreementsVM.PartnershipAgreementTitle,
                                AgreementType = "PartnershipAgreement"
                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });
                    case "ContractAgreement":
                        ContractAgreementsVM contractAgreementsVM = new ContractAgreementsVM();
                        contractAgreementsVM = new ContractAgreementsVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            ContractAgreementFileExt = ext,
                            ContractAgreementFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            ContractAgreementDescription = addAgreementPVM.AgreementDescription,
                            ContractAgreementNumber = 0,
                            ContractAgreementFileOrder = 0,
                            ContractAgreementFileType = fileType,
                            ContractAgreementTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/ContractAgreementsManagement/AddToContractAgreements";
                        AddToContractAgreementsPVM addToContractAgreementsPVM = new AddToContractAgreementsPVM()
                        {
                            ContractAgreementsVM = contractAgreementsVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToContractAgreements(addToContractAgreementsPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    contractAgreementsVM = jObject.ToObject<ContractAgreementsVM>();
                                }
                            }
                        }
                        try
                        {
                            if (contractAgreementsVM.ContractAgreementId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string ContractAgreementFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + contractAgreementsVM.ConstructionProjectId + "\\ContractAgreementFiles\\" + contractAgreementsVM.ContractAgreementId + "\\Image";
                            if (!Directory.Exists(ContractAgreementFilesFolder))
                            {
                                Directory.CreateDirectory(ContractAgreementFilesFolder);
                            }
                            using (var fileStream = new FileStream(ContractAgreementFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }
                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = contractAgreementsVM.ContractAgreementDescription,
                                AgreementFileExt = contractAgreementsVM.ContractAgreementFileExt,
                                AgreementFilePath = contractAgreementsVM.ContractAgreementFilePath,
                                AgreementId = contractAgreementsVM.ContractAgreementId,
                                ConstructionProjectId = contractAgreementsVM.ConstructionProjectId,
                                AgreementTitle = contractAgreementsVM.ContractAgreementTitle,
                                AgreementType = "ContractAgreement"

                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });

                    case "ConfirmationAgreement":
                        ConfirmationAgreementsVM confirmationAgreementsVM = new ConfirmationAgreementsVM();
                        confirmationAgreementsVM = new ConfirmationAgreementsVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            ConfirmationAgreementFileExt = ext,
                            ConfirmationAgreementFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            ConfirmationAgreementDescription = addAgreementPVM.AgreementDescription,
                            ConfirmationAgreementNumber = 0,
                            ConfirmationAgreementFileOrder = 0,
                            ConfirmationAgreementFileType = fileType,
                            ConfirmationAgreementTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/ConfirmationAgreementsManagement/AddToConfirmationAgreements";
                        AddToConfirmationAgreementsPVM addToConfirmationAgreementsPVM = new AddToConfirmationAgreementsPVM()
                        {
                            ConfirmationAgreementsVM = confirmationAgreementsVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToConfirmationAgreements(addToConfirmationAgreementsPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    confirmationAgreementsVM = jObject.ToObject<ConfirmationAgreementsVM>();
                                }
                            }
                        }
                        try
                        {
                            if (confirmationAgreementsVM.ConfirmationAgreementId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string ConfirmationAgreementFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + confirmationAgreementsVM.ConstructionProjectId + "\\ConfirmationAgreementFiles\\" + confirmationAgreementsVM.ConfirmationAgreementId + "\\Image";
                            if (!Directory.Exists(ConfirmationAgreementFilesFolder))
                            {
                                Directory.CreateDirectory(ConfirmationAgreementFilesFolder);
                            }
                            using (var fileStream = new FileStream(ConfirmationAgreementFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }

                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = confirmationAgreementsVM.ConfirmationAgreementDescription,
                                AgreementFileExt = confirmationAgreementsVM.ConfirmationAgreementFileExt,
                                AgreementFilePath = confirmationAgreementsVM.ConfirmationAgreementFilePath,
                                AgreementId = confirmationAgreementsVM.ConfirmationAgreementId,
                                ConstructionProjectId = confirmationAgreementsVM.ConstructionProjectId,
                                AgreementTitle = confirmationAgreementsVM.ConfirmationAgreementTitle,
                                AgreementType = "ConfirmationAgreement"

                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });



                    case "ContractorsAgreement":
                        ContractorsAgreementsVM contractorsAgreementsVM = new ContractorsAgreementsVM();
                        contractorsAgreementsVM = new ContractorsAgreementsVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            ContractorsAgreementFileExt = ext,
                            ContractorsAgreementFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            ContractorsAgreementDescription = addAgreementPVM.AgreementDescription,
                            ContractorsAgreementNumber = 0,
                            ContractorsAgreementFileOrder = 0,
                            ContractorsAgreementFileType = fileType,
                            ContractorsAgreementTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/ContractorsAgreementsManagement/AddToContractorsAgreements";
                        AddToContractorsAgreementsPVM addToContractorsAgreementsPVM = new AddToContractorsAgreementsPVM()
                        {
                            ContractorsAgreementsVM = contractorsAgreementsVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToContractorsAgreements(addToContractorsAgreementsPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    contractorsAgreementsVM = jObject.ToObject<ContractorsAgreementsVM>();
                                }
                            }
                        }
                        try
                        {
                            if (contractorsAgreementsVM.ContractorsAgreementId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string ContractorsAgreementFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + contractorsAgreementsVM.ConstructionProjectId + "\\ContractorsAgreementFiles\\" + contractorsAgreementsVM.ContractorsAgreementId + "\\Image";
                            if (!Directory.Exists(ContractorsAgreementFilesFolder))
                            {
                                Directory.CreateDirectory(ContractorsAgreementFilesFolder);
                            }
                            using (var fileStream = new FileStream(ContractorsAgreementFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }
                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = contractorsAgreementsVM.ContractorsAgreementDescription,
                                AgreementFileExt = contractorsAgreementsVM.ContractorsAgreementFileExt,
                                AgreementFilePath = contractorsAgreementsVM.ContractorsAgreementFilePath,
                                AgreementId = contractorsAgreementsVM.ContractorsAgreementId,
                                ConstructionProjectId = contractorsAgreementsVM.ConstructionProjectId,
                                AgreementTitle = contractorsAgreementsVM.ContractorsAgreementTitle,
                                AgreementType = "ContractorsAgreement"

                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });






                    case "InitialPlan":
                        InitialPlansVM initialPlansVM = new InitialPlansVM();
                        initialPlansVM = new InitialPlansVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            InitialPlanFileExt = ext,
                            InitialPlanFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            InitialPlanDescription = addAgreementPVM.AgreementDescription,
                            InitialPlanNumber = 0,
                            InitialPlanFileOrder = 0,
                            InitialPlanFileType = fileType,
                            InitialPlanTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/InitialPlansManagement/AddToInitialPlans";
                        AddToInitialPlansPVM addToInitialPlansPVM = new AddToInitialPlansPVM()
                        {
                            InitialPlansVM = initialPlansVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToInitialPlans(addToInitialPlansPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    initialPlansVM = jObject.ToObject<InitialPlansVM>();
                                }
                            }
                        }
                        try
                        {
                            if (initialPlansVM.InitialPlanId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string InitialPlanFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + initialPlansVM.ConstructionProjectId + "\\InitialPlanFiles\\" + initialPlansVM.InitialPlanId + "\\Image";
                            if (!Directory.Exists(InitialPlanFilesFolder))
                            {
                                Directory.CreateDirectory(InitialPlanFilesFolder);
                            }
                            using (var fileStream = new FileStream(InitialPlanFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }
                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = initialPlansVM.InitialPlanDescription,
                                AgreementFileExt = initialPlansVM.InitialPlanFileExt,
                                AgreementFilePath = initialPlansVM.InitialPlanFilePath,
                                AgreementId = initialPlansVM.InitialPlanId,
                                ConstructionProjectId = initialPlansVM.ConstructionProjectId,
                                AgreementTitle = initialPlansVM.InitialPlanTitle,
                                AgreementType = "InitialPlan"

                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });



                    case "PitchDeck":
                        PitchDecksVM pitchDecksVM = new PitchDecksVM();
                        pitchDecksVM = new PitchDecksVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            PitchDeckFileExt = ext,
                            PitchDeckFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            PitchDeckDescription = addAgreementPVM.AgreementDescription,
                            PitchDeckNumber = 0,
                            PitchDeckFileOrder = 0,
                            PitchDeckFileType = fileType,
                            PitchDeckTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/PitchDecksManagement/AddToPitchDecks";
                        AddToPitchDecksPVM addToPitchDecksPVM = new AddToPitchDecksPVM()
                        {
                            PitchDecksVM = pitchDecksVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToPitchDecks(addToPitchDecksPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    pitchDecksVM = jObject.ToObject<PitchDecksVM>();
                                }
                            }
                        }
                        try
                        {
                            if (pitchDecksVM.PitchDeckId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string PitchDeckFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + pitchDecksVM.ConstructionProjectId + "\\PitchDeckFiles\\" + pitchDecksVM.PitchDeckId + "\\Image";
                            if (!Directory.Exists(PitchDeckFilesFolder))
                            {
                                Directory.CreateDirectory(PitchDeckFilesFolder);
                            }
                            using (var fileStream = new FileStream(PitchDeckFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }
                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = pitchDecksVM.PitchDeckDescription,
                                AgreementFileExt = pitchDecksVM.PitchDeckFileExt,
                                AgreementFilePath = pitchDecksVM.PitchDeckFilePath,
                                AgreementId = pitchDecksVM.PitchDeckId,
                                ConstructionProjectId = pitchDecksVM.ConstructionProjectId,
                                AgreementTitle = pitchDecksVM.PitchDeckTitle,
                                AgreementType = "PitchDeck"

                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });

                    case "MeetingBoard":
                        MeetingBoardsVM meetingBoardsVM = new MeetingBoardsVM();
                        meetingBoardsVM = new MeetingBoardsVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            MeetingBoardFileExt = ext,
                            MeetingBoardFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            MeetingBoardDescription = addAgreementPVM.AgreementDescription,
                            MeetingBoardNumber = 0,
                            MeetingBoardFileOrder = 0,
                            MeetingBoardFileType = fileType,
                            MeetingBoardTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/MeetingBoardsManagement/AddToMeetingBoards";
                        AddToMeetingBoardsPVM addToMeetingBoardsPVM = new AddToMeetingBoardsPVM()
                        {
                            MeetingBoardsVM = meetingBoardsVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToMeetingBoards(addToMeetingBoardsPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    meetingBoardsVM = jObject.ToObject<MeetingBoardsVM>();
                                }
                            }
                        }
                        try
                        {
                            if (meetingBoardsVM.MeetingBoardId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string MeetingBoardFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + meetingBoardsVM.ConstructionProjectId + "\\MeetingBoardFiles\\" + meetingBoardsVM.MeetingBoardId + "\\Image";
                            if (!Directory.Exists(MeetingBoardFilesFolder))
                            {
                                Directory.CreateDirectory(MeetingBoardFilesFolder);
                            }
                            using (var fileStream = new FileStream(MeetingBoardFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }
                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = meetingBoardsVM.MeetingBoardDescription,
                                AgreementFileExt = meetingBoardsVM.MeetingBoardFileExt,
                                AgreementFilePath = meetingBoardsVM.MeetingBoardFilePath,
                                AgreementId = meetingBoardsVM.MeetingBoardId,
                                ConstructionProjectId = meetingBoardsVM.ConstructionProjectId,
                                AgreementTitle = meetingBoardsVM.MeetingBoardTitle,
                                AgreementType = "MeetingBoard"

                            };

                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
                        }
                        catch (Exception exc)
                        { }
                        return Json(new
                        {
                            Result = "ERROR",
                            Message = "خطا"
                        });




                    case "ProgressPicture":
                        ProgressPicturesVM progressPicturesVM = new ProgressPicturesVM();
                        progressPicturesVM = new ProgressPicturesVM()
                        {
                            CreateEnDate = DateTime.Now,
                            CreateTime = PersianDate.TimeNow,
                            UserIdCreator = this.userId.Value,
                            IsActivated = true,
                            IsDeleted = false,
                            IsConfirm = false,
                            ProgressPictureFileExt = ext,
                            ProgressPictureFilePath = fileName,
                            ConstructionProjectId = addAgreementPVM.ConstructionProjectId,
                            ProgressPictureDescription = addAgreementPVM.AgreementDescription,
                            ProgressPictureNumber = 0,
                            ProgressPictureFileOrder = 0,
                            ProgressPictureFileType = fileType,
                            ProgressPictureTitle = addAgreementPVM.AgreementTitle,
                        };
                        serviceUrl = projectsApiUrl + "/api/ProgressPicturesManagement/AddToProgressPictures";
                        AddToProgressPicturesPVM addToProgressPicturesPVM = new AddToProgressPicturesPVM()
                        {
                            ProgressPicturesVM = progressPicturesVM,
                            UserId = this.userId.Value
                        };
                        responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToProgressPictures(addToProgressPicturesPVM);
                        if (responseApiCaller.IsSuccessStatusCode)
                        {
                            jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                            if (jsonResultWithRecordObjectVM != null)
                            {
                                if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                                {
                                    JObject jObject = jsonResultWithRecordObjectVM.Record;
                                    progressPicturesVM = jObject.ToObject<ProgressPicturesVM>();
                                }
                            }
                        }
                        try
                        {
                            if (progressPicturesVM.ProgressPictureId == 0)
                            {

                                return Json(new
                                {
                                    Result = "ERROR",
                                    Message = "خطا"
                                });
                            }
                            string ProgressPictureFilesFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName +
                                "\\" + progressPicturesVM.ConstructionProjectId + "\\ProgressPictureFiles\\" + progressPicturesVM.ProgressPictureId + "\\Image";
                            if (!Directory.Exists(ProgressPictureFilesFolder))
                            {
                                Directory.CreateDirectory(ProgressPictureFilesFolder);
                            }
                            using (var fileStream = new FileStream(ProgressPictureFilesFolder + "\\" + fileName, FileMode.Create))
                            {
                                await addAgreementPVM.File.CopyToAsync(fileStream);
                                System.Threading.Thread.Sleep(100);
                            }
                            AgreementFileWithAttachmentsVM agreementVm = new AgreementFileWithAttachmentsVM
                            {
                                AgreementDescription = progressPicturesVM.ProgressPictureDescription,
                                AgreementFileExt = progressPicturesVM.ProgressPictureFileExt,
                                AgreementFilePath = progressPicturesVM.ProgressPictureFilePath,
                                AgreementId = progressPicturesVM.ProgressPictureId,
                                ConstructionProjectId = progressPicturesVM.ConstructionProjectId,
                                AgreementTitle = progressPicturesVM.ProgressPictureTitle,
                                AgreementType = "ProgressPicture"

                            };
                            return Json(new
                            {
                                Record = agreementVm,
                                Result = "OK",
                                Message = "آپلود انجام شد",
                            });
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
            catch (Exception exc)
            { }
            return Json(new
            {
                Result = "ERROR",
                Message = "خطا"
            });
        }
        #endregion

        #region آپلود فایل برای الحاقیه
        [AjaxOnly]
        [HttpPost, DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> AddAttachment(AddAttachmentPVM addAttachmentPVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });

            AttachementFilesVM attachementFilesVM = new AttachementFilesVM();

            try
            {
                if (addAttachmentPVM != null)
                {
                    var domainSettings = consoleBusiness.GetDomainsSettingsWithUserId(this.userId.Value);


                    string fileName = "";

                    string fileType = "";

                    string ext = Path.GetExtension(addAttachmentPVM.File.FileName);
                    fileName = Guid.NewGuid().ToString() + ext;

                    if (ext.Equals(".jpeg") ||
                        ext.Equals(".jpg") ||
                        ext.Equals(".png") ||
                        ext.Equals(".gif") ||
                        ext.Equals(".bmp"))
                    {
                        fileType = "media";

                    }
                    else if (ext.Equals(".pdf"))
                    {
                        fileType = "pdf";

                    }
                    else if (ext.Equals(".xls") ||
                              ext.Equals(".xlsx"))
                    {
                        fileType = "excel";

                    }
                    else if (ext.Equals(".docx") ||
                              ext.Equals(".doc"))
                    {
                        fileType = "word";
                    }
                    else
                    {
                        fileType = "other";
                    }

                    attachementFilesVM = new AttachementFilesVM()
                    {
                        AttachementParentId = addAttachmentPVM.AgreementId,
                        AttachementTableTitle = addAttachmentPVM.ContractType + "s",
                        IsConfirm = false,
                        IsSend = false,
                        IsView = false,
                        CreateEnDate = DateTime.Now,
                        CreateTime = PersianDate.TimeNow,
                        UserIdCreator = this.userId.Value,
                        IsActivated = true,
                        IsDeleted = false,
                        AttachementFileExt = ext,
                        AttachementFilePath = fileName,
                        AttachementDescription = addAttachmentPVM.AttachmentDescription,
                        AttachementFileOrder = 0,
                        AttachementFileType = fileType,
                        AttachementTitle = addAttachmentPVM.AttachmentTitle,
                    };

                    string serviceUrl = projectsApiUrl + "/api/AttachementFilesManagement/AddToAttachementFiles";

                    AddToAttachementFilesPVM addToAttachementFilesPVM1 = new AddToAttachementFilesPVM()
                    {
                        AttachementFilesVM = attachementFilesVM,
                        ChildsUsersIds = consoleBusiness.GetChildUserIdsMatchWithLevelIds(this.areaName, this.controllerName, this.actionName, this.userId.Value, this.parentUserId.Value,
                            this.domainsSettings.UserIdCreator.Value, this.roleIds, this.levelIds),
                        UserId = this.userId.Value
                    };

                    responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToAttachementFiles(addToAttachementFilesPVM1);
                    if (responseApiCaller.IsSuccessStatusCode)
                    {
                        jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                        if (jsonResultWithRecordObjectVM != null)
                        {
                            if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                            {
                                JObject jObject = jsonResultWithRecordObjectVM.Record;
                                attachementFilesVM = jObject.ToObject<AttachementFilesVM>();
                            }
                        }
                    }
                    try
                    {
                        if (attachementFilesVM.AttachementId == 0)
                        {

                            return Json(new
                            {
                                Result = "ERROR",
                                Message = "خطا"
                            });
                        }
                        //string attachementFileFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\AttachementFiles\\" + domainSettings.DomainName + "\\" + attachementFilesVM.AttachementId + "\\Image";
                        string attachementFileFolder = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainSettings.DomainName +
                            "\\" + addAttachmentPVM.ConstructionProjectId + "\\AttachementFiles\\" + attachementFilesVM.AttachementId + "\\Image";
                        if (!Directory.Exists(attachementFileFolder))
                        {
                            Directory.CreateDirectory(attachementFileFolder);
                        }
                        using (var fileStream = new FileStream(attachementFileFolder + "\\" + fileName, FileMode.Create))
                        {
                            await addAttachmentPVM.File.CopyToAsync(fileStream);
                            System.Threading.Thread.Sleep(100);
                        }
                        return Json(new
                        {
                            Record = attachementFilesVM,
                            Result = "OK",
                            Message = "آپلود انجام شد",
                        });

                    }
                    catch (Exception exc)
                    { }
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
        #endregion

        #region دانلود فایل
       
        public async Task<IActionResult> Download(string constructionProjectId, long fileId, string fileName, string type)
        {
            if (fileName == null)
                return Content("FileNotFound");
            string fileLocation = "";
            if (type == "Attachments")
            {
                fileLocation = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName + "\\"
                    + constructionProjectId + "\\AttachementFiles\\" + fileId + "\\Image\\";
            }
            else
            {
                fileLocation = hostEnvironment.ContentRootPath + "\\wwwroot\\Files\\ConstructionProjectFiles\\" + domainsSettings.DomainName + "\\"
                    + constructionProjectId + "\\" + type + "Files" + "\\" + fileId + "\\Image\\";
            }
            if (System.IO.File.Exists(fileLocation + fileName))
            {
                try
                {
                    serviceUrl = projectsApiUrl + "/api/FileStatesLogsManagement/AddToFileStatesLogs";
                    var fileStatesLogsVM = new FileStatesLogsVM
                    {
                        TableTitle = type,
                        RecordId = fileId,
                        FileStateId = 3,
                    };
                    AddToFileStatesLogsPVM addToFileStatesLogsPVM = new AddToFileStatesLogsPVM()
                    {
                        FileStatesLogsVM = fileStatesLogsVM
                    };
                    responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToFileStatesLogs(addToFileStatesLogsPVM);
                }
                catch (Exception exc)
                {
                    // Handle exception
                }
                var filePath = fileLocation + fileName;
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                string contentType = "";
                string extension = Path.GetExtension(fileName).ToLowerInvariant();
                switch (extension)
                {
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".jpg":
                    case ".jpeg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".dwg":
                        contentType = "application/octet-stream"; // Adjust as needed
                        break;
                    case ".skp":
                        contentType = "application/octet-stream"; // Adjust as needed
                        break;
                    case ".mp4":
                        contentType = "video/mp4";
                        break;
                    case ".mkv":
                        contentType = "video/x-matroska";
                        break;
                    case ".xlsx":
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                    case ".doc":
                    case ".docx":
                        contentType = "application/msword";
                        break;
                    default:
                        contentType = "application/octet-stream"; // Default to binary if the type is unknown
                        break;
                }
                return File(memory, contentType, Path.GetFileName(filePath));
            }
            return Content("FileNotFound");
        }
        #endregion

        #region  چت


        //اضافه کردن مکالمه
        [AjaxOnly]
        [HttpPost]
        public IActionResult AddToConversationLogs(AddToConversationLogsPVM addToConversationLogsPVM)
        {
            JsonResultWithRecordObjectVM jsonResultWithRecordObjectVM = new JsonResultWithRecordObjectVM(new object() { });
            try
            {

                AddToConversationLogsPVM addToConversationLogs = new AddToConversationLogsPVM()
                {
                    ConversationLogsVM = new ConversationLogsVM()
                    {
                        ConversationLogDescription = addToConversationLogsPVM.ConversationLogsVM.ConversationLogDescription,
                        RecordId = addToConversationLogsPVM.ConversationLogsVM.RecordId,
                        TableTitle = addToConversationLogsPVM.ConversationLogsVM.TableTitle,
                        UserIdCreator =this.userId.Value,
                        CreateEnDate = DateTime.Now,
                        CreateTime = PersianDate.TimeNow,
                        IsActivated = true,
                        IsDeleted = false,
                    },
                    
                    UserId = this.userId.Value
                };


                serviceUrl = projectsApiUrl + "/api/ConversationLogsManagement/AddToConversationLogs";

                responseApiCaller = new ProjectsApiCaller(serviceUrl).AddToConversationLogs(addToConversationLogs);
                if (responseApiCaller.IsSuccessStatusCode)
                {
                    jsonResultWithRecordObjectVM = responseApiCaller.JsonResultWithRecordObjectVM;

                    if (jsonResultWithRecordObjectVM != null)
                    {
                        if (jsonResultWithRecordObjectVM.Result.Equals("OK"))
                        {
                            return Json(jsonResultWithRecordObjectVM);
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


        //لیستی از مکالمات 
        [AjaxOnly]
        [HttpPost]
        public IActionResult GetConversationDataByAgreementTypeAndRecordId(GetConversationDataByAgreementTypeAndRecordIdPVM
          getConversationDataByAgreementTypeAndRecordIdPVM)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                GetConversationDataByAgreementTypeAndRecordIdPVM conversationDataByAgreementTypeAndRecordIdPVM = new GetConversationDataByAgreementTypeAndRecordIdPVM
                {
                    UserId = this.userId,
                    ContractType = getConversationDataByAgreementTypeAndRecordIdPVM.ContractType,
                    RecordId = getConversationDataByAgreementTypeAndRecordIdPVM.RecordId,
                };
                serviceUrl = projectsApiUrl + "/api/ConversationLogsManagement/GetConversationDataByAgreementTypeAndRecordId";
                responseApiCaller = new ProjectsApiCaller(serviceUrl).GetConversationDataByAgreementTypeAndRecordId(conversationDataByAgreementTypeAndRecordIdPVM);
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
        #endregion


    }
}