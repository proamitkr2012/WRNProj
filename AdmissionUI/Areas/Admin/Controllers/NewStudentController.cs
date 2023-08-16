using AdmissionData.Entities;
using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Controllers;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;

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
			//,IHostingEnvironment EnvhostingEnv = Env;
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

        public async Task<IActionResult>  CourseWiseStudentCount()
        {
            SearchStudentModel sm = new SearchStudentModel();
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;

            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(-1)).ToList();
            courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
            ViewBag.Courses = courselist;

            return View(sm);
        }

        [HttpPost]
        public async Task<IActionResult> CourseWiseStudentCount(SearchStudentModel sm    )
        {
            SearchStudent ss=new SearchStudent();
            ss.CourseTypeId = sm.CourseTypeId;
            ss.CourseID = sm.CourseID;
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;
            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(sm.CourseTypeId)).ToList();
            courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
            ViewBag.Courses = courselist;


            var list = await _iuow.adminDashBoard.CourseWiseStudentCount(ss);

            if (list != null && list.Count() > 0)
            {
                sm.listcourseWise  = list.ToList();
            }
            return View(sm);
         }




        public async Task<IActionResult> SearchStudent()
		{
			SearchStudentModel sm = new SearchStudentModel();
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;

            var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(-1)).ToList();
            courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
            ViewBag.Courses = courselist;

            return View(sm);
		}

		[HttpPost]
        public async Task<IActionResult> SearchStudent(SearchStudentModel sm )
        {
            
                SearchStudent ss = new SearchStudent();
                ss.CourseTypeId = sm.CourseTypeId;
                ss.SearchText = !string.IsNullOrEmpty(sm.SearchText) ? sm.SearchText : "";
                ss.CourseID = sm.CourseID;

                var list = await _iuow.adminDashBoard.SearchStudentsData(ss);

                if (list != null && list.Count() > 0)
                {
                    sm.studentList = list.ToList();

                    for (int i = 0; i < sm.studentList.Count; i++)
                    {
                        sm.studentList[i].EncrptedData = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", sm.studentList[i].ApplicationNo, 0));
                    }
                }

                var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
                listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
                ViewBag.CourseType = listcoursetype;

                var courselist = (await _iuow.masterRepo.GetAllCoursebyCourseType(sm.CourseTypeId)).ToList();
                courselist.Insert(0, new Course { CourseId = 0, CourseName = "Select Course" });
                ViewBag.Courses = courselist;

                return View(sm);
            



        }

      



        
        public async Task<IActionResult> actionSearchStudent(string tid, string catid)
        {
            string strReq = "";
            strReq = tid.Replace("%2F", "/");
            tid = strReq;
            //strReq = strReq.Substring(strReq.IndexOf('?') + 1);

            if (!strReq.Equals(""))
            {
                strReq = DecryptQueryString(strReq);
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string Memcode = "", SMS = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                Memcode = arrIndMsg[1].ToString().Trim();
                arrIndMsg = arrMsgs[1].Split('='); //Get the Age
                SMS = arrIndMsg[1].ToString().Trim();
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Sid,Memcode)
                    ,new Claim(ClaimTypes.Role,CurrentUser.Roles[0].ToString() )
                    ,new Claim(ClaimTypes.Name,"" )
                     ,new Claim(ClaimTypes.PrimarySid,CurrentUser.UserId.ToString() )
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var stdlogin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", tid, 1));
                return RedirectToAction("stdprofile", "Student", new { area = "", tid = str });
             }
            else
            {

                return RedirectToAction("SearchStudent");
            }
        }



        public async Task<IActionResult> getStudentDetailsByWRN()
        {
          var std =  new StudentRegModel();
           return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> getStudentDetailsByWRN( string appno )
        {
           
            var stdmaster = await _iuow.studentPreRepo.GetByIdAsync(appno.Trim());
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<StudentMasters, StudentRegModel>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                var std = mapper.Map<StudentRegModel>(stdmaster);
                std.ApplicationNo = appno;
                return View(std);
            }
            catch (Exception ex) {
                var std = new StudentRegModel();
                return View(std);
            }
           
        }

        public async Task<IActionResult> UgPGChange()
        {
            SearchStudentModel search = new SearchStudentModel();
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;
            return View(search);
        }
        [HttpPost]
        public async Task<IActionResult> UgPGChange(SearchStudentModel sm, string commandname)
        {
            if (commandname.ToLower() == "search")
            {
                SearchStudent ss = new SearchStudent();
             
                ss.SearchText = !string.IsNullOrEmpty(sm.SearchText) ? sm.SearchText : "";
                ss.CourseTypeId = sm.CourseTypeId;
                ss.SearchText = !string.IsNullOrEmpty(sm.SearchText) ? sm.SearchText : "";
                var list = await _iuow.adminDashBoard.SearchUnpaiStudentsData(ss);
                sm.studentList= list.ToList();
            }
            else
            {
                if (commandname.ToLower() == "save")
                {
                    SearchStudent ss = new SearchStudent();
                    if (!string.IsNullOrEmpty(sm.SearchText) && sm.CourseTypeId != 0)
                    {
                        ss.SearchText = !string.IsNullOrEmpty(sm.SearchText) ? sm.SearchText : "";
                        ss.CourseTypeId = sm.CourseTypeId;
                        ss.SearchText = !string.IsNullOrEmpty(sm.SearchText) ? sm.SearchText : "";
                        var list = await _iuow.adminDashBoard.ChangeUG_PG(ss);
                        sm.studentList = list.ToList();
                    }
                }
            }
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList();
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;
            return View(sm);
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
