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
using AdmissionUI.Areas.Admin.Filters;

namespace AdmissionUI.Areas.Admin.Controllers
{
    [CustomAdminAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected ISession Session => httpContextAccessor.HttpContext.Session;
      //  protected BreadCrumbHelper BreadCrumb;
        protected IConfiguration config;
        protected IUnitOfWork UOF;

        public BaseController(IHttpContextAccessor _httpContextAccessor, IConfiguration _config, IUnitOfWork _UOF)
        {
            httpContextAccessor = _httpContextAccessor;
            //BreadCrumb = new BreadCrumbHelper(_httpContextAccessor);
            UOF = _UOF;
            config = _config;
        }

        public CustomPrincipal CurrentUser
        {
            get
            {
                if (User.Claims.Count() > 0)
                {
                    string userData = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    var user = JsonConvert.DeserializeObject<CustomPrincipal>(userData);
                    return user;
                }
                return null;
            }
        }
    }
}
