using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdmissionUI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult dashboard()
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            

            return View();
        }

        public IActionResult stdprofile()
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;


            return View();
        }

    }
}
