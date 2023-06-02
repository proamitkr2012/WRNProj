using AdmissionModel;
using AdmissionModel.DTO;
using AdmissionRepo;
using AdmissionRepo.Utilities;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AdmissionUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : BaseController
    {
        int pageSize;
        private IWebHostEnvironment environment;
        public string ImgCloudPath = "";
        private IMailClient _mailClient; IUnitOfWork UOF;
        public DashboardController(IHttpContextAccessor _httpContextAccessor, IConfiguration _config,
             IWebHostEnvironment _environment, IUnitOfWork uow, IMailClient mailClient) : base(_httpContextAccessor, _config, uow)
        {
            environment = _environment;
            string _pageSize = "15";//Convert.ToString(WebConfigSetting.PageSize);
            int PS;
            bool result = int.TryParse(_pageSize, out PS);
            pageSize = result == true ? PS : 15;
            _mailClient = mailClient;
            UOF = uow;
        }

        public async Task <IActionResult> Index()
        {
            var data = await UOF.adminDashBoard.RegisterationDataCountReport();
			var bedData = await UOF.adminDashBoard.BedRegisterationDataCountReport();
            AdminRptModel am = new AdminRptModel();
            am.NewAdmissionDataCount = data ;
            am.BedAdmissionDataCount = bedData;

			return View(am);
        }
        public IActionResult BedNewEntry()
        {

            ViewBag.CollegeList = UOF.IAdmin.GetCollegeList();

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> PreEntry([FromBody] StudentMastersDTO model)
        {
            try
            {
                //StudentMastersDTO model = new StudentMastersDTO();
                var datet = Convert.ToDateTime(model.DOBStr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                model.DOB = datet;
                model.DegreeID = 1;
                int appid = await UOF.IAdmin.AddPreRegistration(model,CurrentUser.UserId, GetClientIp());
                return Json(appid);

            }
            catch (Exception e)
            {

            }
            return Json(null);

        }


        public async Task<IActionResult> TotalBedStudentDetails()
        {
            SearchStudentModel sm = new SearchStudentModel();
            SearchStudent ss = new SearchStudent();
            ss.CourseTypeId = 0; ss.SearchText = string.Empty;
            var list = await UOF.adminDashBoard.TotalBedStudentList(ss);

            if (list != null && list.Count() > 0)
            {
                sm.studentList = list.ToList();
            }

            return View(sm);
        }

        public async Task<IActionResult> RegBedStudentDetails()
        {
            SearchStudentModel sm = new SearchStudentModel();
            SearchStudent ss = new SearchStudent();
            ss.CourseTypeId = 0; ss.SearchText = string.Empty;
            ss.CourseID = 0;
            var list = await UOF.adminDashBoard.RegisteredBedStudentList(ss);

            if (list != null && list.Count() > 0)
            {
                sm.studentList = list.ToList();
            }

            return View(sm);
        }


        public async Task<IActionResult> BedStudentsFees()
        {
            SearchStudentModel sm = new SearchStudentModel();
            SearchStudent ss = new SearchStudent();
            ss.CourseTypeId = 0; ss.SearchText = string.Empty;
            ss.CourseID = 0;
            var list = await UOF.adminDashBoard.Bed_StudentFees_List(ss);

            if (list != null && list.Count() > 0)
            {
                sm.studentList = list.ToList();
            }

            return View(sm);
        }

        public async Task<IActionResult> BedStudentsCourseFees()
        {
            SearchStudentModel sm = new SearchStudentModel();
            SearchStudent ss = new SearchStudent();
            ss.CourseTypeId = 0; ss.SearchText = string.Empty;
            ss.CourseID = 0;
            var list = await UOF.adminDashBoard.Bed_StudentFees_List(ss);

            if (list != null && list.Count() > 0)
            {
                sm.studentList = list.ToList();
            }

            return View(sm);
        }











        public string GetClientIp()
        {
            string _ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (_ipAddress.Contains("::1"))
                _ipAddress = "127.0.0.1";

            return _ipAddress;
        }

        public IActionResult StudentNewAdded()
        {
            List<StudentMastersDTO> studentlist = new List<StudentMastersDTO>();
            studentlist = UOF.IAdmin.GETStudentNewAdded(CurrentUser.UserId);
            return View(studentlist);
        }

 





    }
}
