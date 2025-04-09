﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIs.Public.Models;
using Microsoft.AspNetCore.Authorization;
using APIs.Core.Controllers;
using APIs.Public.Models.Business;
using VM.PVM.Public;
using APIs.Automation.Models.Business;
using VM.Public;
using Models.Business.ConsoleBusiness;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using VM.Base;
using APIs.CustomAttributes.Helper;
using Microsoft.Extensions.Options;
using APIs.CustomAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using APIs.Teniaco.Models.Business;
using APIs.Projects.Models.Business;
using APIs.Melkavan.Models.Business;
using APIs.TelegramBot.Models.Business;

namespace APIs.Public.Controllers
{
    /// <summary>
    /// StatesManagement
    /// </summary>
    [CustomApiAuthentication]
    public class StatesManagementController : ApiBaseController
    {
        /// <summary>
        /// StatesManagement
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
        public StatesManagementController(IHostEnvironment _hostingEnvironment,
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
        /// GetListOfStates
        /// </summary>
        /// <param name="getListOfStatesPVM"></param>
        /// <returns>JsonResultWithRecordsObjectVM</returns>
        [HttpPost("GetListOfStates")]
        [ProducesResponseType(typeof(JsonResultWithRecordsObjectVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetListOfStates(GetListOfStatesPVM getListOfStatesPVM)
        {
            JsonResultWithRecordsObjectVM jsonResultWithRecordsObjectVM = new JsonResultWithRecordsObjectVM(new object() { });

            try
            {
                var listOfStates = publicApiBusiness.GetListOfStates(getListOfStatesPVM.CountryId);

                jsonResultWithRecordsObjectVM.Result = "OK";
                jsonResultWithRecordsObjectVM.Records = listOfStates;
                //jsonResultWithRecordsObjectVM.Records = JsonConvert.SerializeObject(listOfStates);

                return new JsonResult(jsonResultWithRecordsObjectVM);
                //return jsonResultWithRecordsObjectVM;
            }
            catch (Exception exc)
            { }

            jsonResultWithRecordsObjectVM.Result = "ERROR";
            jsonResultWithRecordsObjectVM.Message = "ErrorInService";

            return new JsonResult(jsonResultWithRecordsObjectVM);
            //return jsonResultWithRecordsObjectVM;
        }
    }
}
