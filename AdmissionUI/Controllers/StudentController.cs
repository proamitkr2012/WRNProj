using Microsoft.AspNetCore.Mvc;

namespace AdmissionUI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult dashboard()
        {
            return View();
        }
    }
}
