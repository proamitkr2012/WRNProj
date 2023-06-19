using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionUI.Controllers
{
    public class AdmissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult payadmissionFees()
        {
            LoginModel login = new LoginModel();
            return View(login);

        }

        [HttpPost]
        public async Task<IActionResult> payadmissionFees(LoginModel login)
        {
           
            return View(login);
        }

        public IActionResult printAdmissionDetails()
        {
            LoginModel login = new LoginModel();
            return View(login);

        }


        


    }
}
