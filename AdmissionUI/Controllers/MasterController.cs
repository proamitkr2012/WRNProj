using AdmissionData.Entities;
using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdmissionUI.Controllers
{
    public class MasterController : Controller
    {

        private readonly ILogger<MasterController> _logger;
        private readonly IUnitOfWork _iuow;

        public MasterController(ILogger<MasterController> logger, IUnitOfWork iuow, IMasterRepo imasterRepo)
        {
            _logger = logger;
            _iuow = iuow;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult  getCities([FromBody] HomeModels models)
        {
            var cities = _iuow.IAdmin.GetDistrictList(models.StateId);
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> getCourses([FromBody] HomeModels models)
        {
             var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(models.CatId)).ToList();
             //courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
             return Ok(courselist);
        }

    }
}
