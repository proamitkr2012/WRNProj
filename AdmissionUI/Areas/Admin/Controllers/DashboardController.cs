using AdmissionData.Entities;
using AdmissionModel;
using AdmissionModel.DTO;
using AdmissionModel.Enum;
using AdmissionRepo;
using AdmissionRepo.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;

namespace AdmissionUI.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class DashboardController : BaseController
    {
        int pageSize;
        private IWebHostEnvironment environment;
        public string ImgCloudPath = "";
        private IMailClient _mailClient; IUnitOfWork UOF;
        public DashboardController(IHttpContextAccessor _httpContextAccessor, IConfiguration _config,
             IWebHostEnvironment _environment, IUnitOfWork uow, IMailClient mailClient) : base(_httpContextAccessor, _config, uow)
        {
            environment = _environment;
            string _pageSize = "15";//Convert.ToString(WebConfigSetting.PageSize);
            int PS;
            bool result = int.TryParse(_pageSize, out PS);
            pageSize = result == true ? PS : 15;
            _mailClient = mailClient;
            UOF = uow;
        }

        public IActionResult Index()
        {
           
           ViewBag.CollegeList= UOF.IAdmin.GetCollegeList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PreEntry([FromBody] StudentMastersDTO model)
        {
            try
            {
                //StudentMastersDTO model = new StudentMastersDTO();
                var datet = Convert.ToDateTime(model.DOBStr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                model.DOB = datet;
                model.DegreeID = 1;
                int appid = await UOF.IAdmin.AddPreRegistration(model);
                return Json(appid);

            }
            catch (Exception e)
            {

            }
            return Json(null);

        }

    }
}
