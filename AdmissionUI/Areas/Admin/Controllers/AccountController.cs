using AdmissionModel;
using AdmissionRepo;
using AdmissionRepo.Utilities;
using AdmissionUI.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AdmissionModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;

namespace AdmissionUI.Areas.Admin.Controllers
{
    //[Area("Student")]
    public class AccountController : Controller
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected ISession Session => HttpContext.Session;
        private IHostingEnvironment hostingEnv;
        SmsClient sms = new SmsClient();
        int pageSize;
        int pageSizeJobs;

        private IMailClient mailClient;
        public string _baseUrl;
        IUnitOfWork UOF;
        public AccountController(IUnitOfWork uow, IHttpContextAccessor _httpContextAccessor, IMailClient _mailClient)
        {
            _baseUrl = WebConfigSetting.BaseURL;
            mailClient = _mailClient;
            httpContextAccessor = _httpContextAccessor;
            UOF = uow;
        }



    }
}
