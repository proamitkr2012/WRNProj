using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Controllers;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewStudentController : BaseController
	{
		private readonly ILogger<NewStudentController> _logger;
		private readonly IUnitOfWork _iuow;
		//private IHostingEnvironment hostingEnv;
		public NewStudentController(IHttpContextAccessor _httpContextAccessor, ILogger<NewStudentController> logger, IUnitOfWork iuow, IMasterRepo imasterRepo ,IConfiguration _config ) : base(_httpContextAccessor, _config, iuow)
        {
			_logger = logger;
			_iuow = iuow;
			//, IHostingEnvironment EnvhostingEnv = Env;
		}

	
		public async Task<IActionResult> TotalStudentDetails()
		{
			SearchStudentModel sm = new SearchStudentModel();
            SearchStudent  ss= new SearchStudent();
            ss.CourseTypeId = 0; ss.SearchText = string.Empty;
			var list = await _iuow.adminDashBoard.TotalStudentList(ss);

			if (list != null && list.Count() > 0)
			{ 
			  sm.studentList = list.ToList();
			}

            return View(sm);
		}

        public async Task<IActionResult> RegStudentDetails()
        {
            SearchStudentModel sm = new SearchStudentModel();
            SearchStudent ss = new SearchStudent();
            ss.CourseTypeId = 0; ss.SearchText = string.Empty;
            ss.CourseID = 0;
            var list = await _iuow.adminDashBoard.RegisteredStudentList(ss);

            if (list != null && list.Count() > 0)
            {
                sm.studentList = list.ToList();
            }

            return View(sm);
        }


		public async Task<IActionResult> StudentsFees()
		{
			SearchStudentModel sm = new SearchStudentModel();
			SearchStudent ss = new SearchStudent();
			ss.CourseTypeId = 0; ss.SearchText = string.Empty;
			ss.CourseID = 0;
			var list = await _iuow.adminDashBoard.Student_Course_Fees_List(ss);

			if (list != null && list.Count() > 0)
			{
				sm.studentList = list.ToList();
			}

			return View(sm);
		}

		public async Task<IActionResult> StudentsCourseFees()
		{
			SearchStudentModel sm = new SearchStudentModel();
			SearchStudent ss = new SearchStudent();
			ss.CourseTypeId = 0; ss.SearchText = string.Empty;
			ss.CourseID = 0;
			var list = await _iuow.adminDashBoard.Student_Course_Fees_List(ss);

			if (list != null && list.Count() > 0)
			{
				sm.studentList = list.ToList();
			}

			return View(sm);
		}




		public IActionResult SearchStudent()
		{
			return View();
		}
	}
}
