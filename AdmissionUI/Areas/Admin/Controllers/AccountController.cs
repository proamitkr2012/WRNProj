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
    [Area("Admin")]
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

        [Route("~/admin/login")]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [Route("~/admin/login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] AdminLoginDTO model, string ReturnUrl)
        {
            try
            {

                if (model.Email.Trim() != "" && model.Password.Trim() != "")
                {
                    AdminMasterDTO adminData = UOF.IAdmin.AuthenticateAdmin(model.Email, model.Password);
                    if (adminData != null)
                    {
                        //if (adminData.IsVerified != true)
                        //{
                        //    return Json("The admin account is not verified. Please verifiy you account.");
                        //}
                        //if (adminData.IsActive != true)
                        //{
                        //    return Json("The admin account is de-activated. Please contact website administrator.");

                        //}
                        GenerateTicket(adminData, true);
                        if (!string.IsNullOrEmpty(ReturnUrl))
                            return Redirect(ReturnUrl);
                        else
                            return Json("success");

                    }
                    else
                    {
                        return Json("The Email or Password provided is incorrect");

                    }
                    
                }
                else
                {
                    return Json("The Email or Password is not provided!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        private async void GenerateTicket(AdminMasterDTO adminData, bool IsPersistent = true)
        {
            CustomPrincipal serializeModel = new CustomPrincipal();
            serializeModel.UserId = adminData.AdminId;
            serializeModel.Name = adminData.Name;
            serializeModel.MobileNo = adminData.MobileNo;
            serializeModel.Email = adminData.Email;

            serializeModel.Roles = adminData.Roles;
            serializeModel.ProfilePic = adminData.ProfilePic;
            string userData = JsonConvert.SerializeObject(serializeModel);

            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.UserData, userData),
                            new Claim(ClaimTypes.Email, adminData.Email),
                            new Claim(ClaimTypes.Role, adminData.Roles.ToString()),
                        };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                // IsPersistent = IsPersistent,
                AllowRefresh = true,
                // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            });
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity))
            //SetLoginDeviceHistory(Convert.ToInt32(member.MemberId), 3); //3 for member
        }

    }
}
