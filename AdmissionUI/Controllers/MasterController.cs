using AdmissionData.Entities;
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



    }
}
