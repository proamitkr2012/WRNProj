using AdmissionData.Entities;
using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdmissionUI.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly ILogger<StudentCourseController> _logger;
        private readonly IUnitOfWork _iuow;
        
        public StudentCourseController(ILogger<StudentCourseController> logger, IUnitOfWork iuow, IMasterRepo imasterRepo )
        {
            _logger = logger;
            _iuow = iuow;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> addCourse(string? id)
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            
            StudentApplyCourseModel std = new StudentApplyCourseModel();
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            StudentMasters studentMasters = await _iuow.studentPreRepo.GetByIdAsync(appno);
            var lisapplycourses = await _iuow.studentApplyCourse.GetAllAsyncByAppNo(appno);
            if (lisapplycourses != null && lisapplycourses.Count() > 0)
            {
                std.studentapplyList = lisapplycourses.ToList();
            }
            else
            {
                std.studentapplyList = new List<StudentAppliedCourse>();
            }

            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(studentMasters.CourseTypeID)).ToList();
            courselist.Insert(0, new Course { CourseId = 0 , CourseName = "Select Course" });
            ViewBag.Courses= courselist;
            std.ApplicationNo = appno;
            std.EncrptedData = str;

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> addCourse (StudentApplyCourseModel std, string commandName )
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

             
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            StudentMasters studentMasters = await _iuow.studentPreRepo.GetByIdAsync(appno);
            var lisapplycourses = await _iuow.studentApplyCourse.GetAllAsyncByAppNo(appno);
            if (lisapplycourses != null && lisapplycourses.Count() > 0)
            {
                std.studentapplyList = lisapplycourses.ToList();
            }
            else
            {
                std.studentapplyList = new List<StudentAppliedCourse>();
            }

            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(studentMasters.CourseTypeID)).ToList();
            courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
            ViewBag.Courses = courselist;
            std.ApplicationNo = appno;
            std.EncrptedData = str;
            return View(std);

        }
















            public string EncryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
        }


        private string DecryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Decrypt(strQueryString, "r0b1nr0y");
        }
    }
}
