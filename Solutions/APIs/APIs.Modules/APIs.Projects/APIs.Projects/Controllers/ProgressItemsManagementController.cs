﻿using APIs.Automation.Models.Business;
using APIs.Core.Controllers;
using APIs.CustomAttributes;
using APIs.CustomAttributes.Helper;
using APIs.Melkavan.Models.Business;
using APIs.Projects.Models.Business;
using APIs.Public.Models.Business;
using APIs.TelegramBot.Models.Business;
using APIs.Teniaco.Models.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Models.Business.ConsoleBusiness;
using System.Linq;
using System.Net;
using System;
using VM.Base;
using VM.Console;
using VM.Projects;
using VM.PVM.Projects;
using VM.Teniaco;

namespace APIs.Projects.Controllers
{
    [CustomApiAuthentication]
    /// <summary>
    /// ProgressItemsManagement
    /// </summary>
    public class ProgressItemsManagementController : ApiBaseController
    {
        /// <summary>
        /// ConstructionProjectsManagement
        /// </summary>
        /// <param name="_hostingEnvironment"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_actionContextAccessor"></param>
        /// <param name="_configurationRoot"></param>
        /// <param name="_consoleBusiness"></param>
        /// <param name="_automationApiBusiness"></param>
        /// <param name="_publicApiBusiness"></param>
        /// <param name="_teniacoApiBusiness"></param>
        /// <param name="_melkavanApiBusiness"></param>
        /// <param name="_projectsApiBusiness"></param>
        /// <param name="_telegramBotApiBusiness"></param>
        /// <param name="_appSettingsSection"></param>
        public ProgressItemsManagementController(IHostEnvironment _hostingEnvironment,
            IHttpContextAccessor _httpContextAccessor,
            IActionContextAccessor _actionContextAccessor,
            IConfigurationRoot _configurationRoot,
            IConsoleBusiness _consoleBusiness,
            IAutomationApiBusiness _automationApiBusiness,
            IPublicApiBusiness _publicApiBusiness,
            ITeniacoApiBusiness _teniacoApiBusiness,
            IMelkavanApiBusiness _melkavanApiBusiness,
            IProjectsApiBusiness _projectsApiBusiness,
            ITelegramBotApiBusiness _telegramBotApiBusiness,
            IOptions<AppSettings> _appSettingsSection) :
            base(
                _hostingEnvironment,
                _httpContextAccessor,
                _actionContextAccessor,
                _configurationRoot,
                _consoleBusiness,
                _automationApiBusiness,
                _publicApiBusiness,
                _teniacoApiBusiness,
                _melkavanApiBusiness,
                _projectsApiBusiness,
                _telegramBotApiBusiness,
                _appSettingsSection)
        {

        }

        /// <summary>
        /// GetHierarchyOfAllProgressItemsList
        /// </summary>
        /// <param name="getAllConstructionProjectsList"></param>
        /// <returns>JsonResultWithRecordsObjectVM</returns>

        [HttpPost("GetHierarchyOfAllProgressItemsList")]
        [ProducesResponseType(typeof(JsonResultWithRecordsObjectVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetHierarchyOfAllProgressItemsList([FromBody] GetHierarchyOfAllProgressItemsListPVM getHierarchyOfAllProgressItemsListPVM)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    if (getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds == null)
                    {
                        getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds = consoleBusiness.GetChildUserIdsWithStoredProcedure(this.domainAdminId.Value);
                    }
                    else
                  if (getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds.Count == 0)
                        getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds = consoleBusiness.GetChildUserIdsWithStoredProcedure(this.domainAdminId.Value);
                    else
                  if (getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds.Count == 1)
                        if (getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds.FirstOrDefault() == 0)
                            getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds = consoleBusiness.GetChildUserIdsWithStoredProcedure(this.domainAdminId.Value);
                }

                var listOfConstructionProjects = projectsApiBusiness.GetHierarchyOfAllProgressItemsList(
                    getHierarchyOfAllProgressItemsListPVM.ChildsUsersIds,
                    getHierarchyOfAllProgressItemsListPVM.ConstructionProjectId,
                    getHierarchyOfAllProgressItemsListPVM.RepresentativeId,
                    getHierarchyOfAllProgressItemsListPVM.Type
                   );

                jsonResultWithRecordsObjectVM.Result = "OK";
                jsonResultWithRecordsObjectVM.Records = listOfConstructionProjects;

                return new JsonResult(jsonResultWithRecordsObjectVM);
            }
            catch (Exception)
            {
            }


            jsonResultWithRecordsObjectVM.Result = "ERROR";
            jsonResultWithRecordsObjectVM.Message = "ErrorInService";

            return new JsonResult(jsonResultWithRecordsObjectVM);
        }
    }
}
