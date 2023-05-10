using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace AdmissionUI.Helpers
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
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
                    var user = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(userData);
                    //Authorization
                    if (!user.Roles.Any(r => Roles.Contains(r)))
                    {
                        context.Result = new RedirectToActionResult("UnAuthorize", "Login", new { area = "" });
                    }
                }
                else
                {
                    context.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
            }
        }
    }
}