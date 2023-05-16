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
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdmissionUI.Areas.Admin.Filters
{
    public class CustomAdminAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userData = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                if (!string.IsNullOrEmpty(userData))
                {
                    var user = JsonConvert.DeserializeObject<CustomPrincipal>(userData);
                    //Authorization
                    if (!user.Roles.Any(r => Roles.Contains(r)))
                    {
                        context.Result = new RedirectToActionResult("UnAuthorize", "Account", new { area = "" });
                    }
                }
                else
                {
                    context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            }
        }
    }
}
