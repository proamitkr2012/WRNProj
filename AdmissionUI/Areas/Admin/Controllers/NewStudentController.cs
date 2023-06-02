﻿using AdmissionData.Entities;
using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Controllers;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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



		 
		public async Task<IActionResult> SearchStudent()
		{
			SearchStudentModel sm = new SearchStudentModel();
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;

            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(0)).ToList();
            courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
            ViewBag.Courses = courselist;

            return View(sm);
		}

		[HttpPost]
        public async Task<IActionResult> SearchStudent(SearchStudentModel sm)
        {
         
            SearchStudent ss = new SearchStudent();
            ss.CourseTypeId = sm.CourseTypeId;
			ss.SearchText =!string.IsNullOrEmpty( sm.SearchText) ?sm.SearchText:"";
            ss.CourseID =sm.CourseID;

            var list = await _iuow.adminDashBoard.SearchStudentsData(ss);

            if (list != null && list.Count() > 0)
            {
                sm.studentList = list.ToList();
            }

            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;

            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(sm.CourseTypeId)).ToList();
            courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
            ViewBag.Courses = courselist;

            return View(sm);
        }


    }
}
