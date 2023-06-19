using AdmissionData.Entities;
using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AdmissionUI.Controllers
{
    public class AdmissionController : Controller
    {
        private readonly ILogger<AdmissionController> _logger;
        private readonly IUnitOfWork _iuow;

        public AdmissionController(ILogger<AdmissionController> logger, IUnitOfWork iuow, IMasterRepo imasterRepo)
        {
            _logger = logger;
            _iuow = iuow;

        }


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

        public async Task <IActionResult> printAdmissionDetails()
        {
            

            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;


            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));



            PrintFormModel printfrm = new PrintFormModel();
            var stdmaster = await _iuow.studentPreRepo.GetByIdAsync(appno);
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<StudentMasters, PrintFormModel>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            printfrm = mapper.Map<PrintFormModel>(stdmaster);
            var stdalldata = await _iuow.studentPreRepo.AppliedStudentDetailByID(printfrm.CollegeCode, printfrm.CourseId.ToString(), printfrm.ApplicationNo);

            printfrm.ApplicationDate = stdalldata.ApplicationDate;
            printfrm.CollegeName = stdalldata.CollegeName;
            printfrm.ReportFlgDate = stdalldata.ReportFlgDate;
            printfrm.AdmitFlgDate = stdalldata.AdmitFlgDate;
            printfrm.CourseName= stdalldata.CourseName;
            printfrm.CRN = stdalldata.CRN;

            var lst = _iuow.IAdmin.GetStateList();
            var lstCdistrict = _iuow.IAdmin.GetDistrictList(Convert.ToInt32(printfrm.CState));
            var lstpdistrict = _iuow.IAdmin.GetDistrictList(Convert.ToInt32(printfrm.PState));


            if (lstCdistrict != null && lstCdistrict.Count() > 0)
                printfrm.CDistrict = lstCdistrict.Where(x => x.Id == Convert.ToInt32(printfrm.CDistrict)).ToList().FirstOrDefault().Name;

            if (lstpdistrict != null && lstpdistrict.Count() > 0)
                printfrm.PDistrict = lstpdistrict.Where(x => x.Id == Convert.ToInt32(printfrm.PDistrict)).ToList().FirstOrDefault().Name;

            if (lst.Count > 0)
            {
                printfrm.CState = lst.Where(x => x.Id == Convert.ToInt32(printfrm.CState)).SingleOrDefault().Name;
                printfrm.PState = lst.Where(x => x.Id == Convert.ToInt32(printfrm.PState)).SingleOrDefault().Name;
            }

            printfrm.QualifationList = (await _iuow.qulificationRepo.GetAllByAppNoAsync(appno)).ToList();
            printfrm.stdweightageList = (await _iuow.StdWeightageRep.GetStudentWeightagesAsync(appno)).ToList();

            var lisapplycourses = await _iuow.studentApplyCourse.GetAllAsyncByAppNo(appno);
            if (lisapplycourses != null && lisapplycourses.Count() > 0)
            {
                printfrm.studentapplyList = lisapplycourses.ToList().Where(x => x.IsFor_Paid == true).ToList(); ;
            }
            else
            {
                printfrm.studentapplyList = new List<StudentAppliedCourse>();
            }

            StudentAppliedColleges sc = new StudentAppliedColleges() { ApplicationNo = appno, CourseId = 0 };
            
            var sublist =  await _iuow.studentApplyCourse.GetChooseSubjectCollegeWise( appno, Convert.ToString( printfrm.CollegeCode)  ,Convert.ToInt32( printfrm.CourseId)  );

            if (sublist != null && sublist.Count() > 0)
            {
                printfrm.subjectList = sublist.ToList();    
            }

            var selsublist = await _iuow.studentApplyCourse.GetAllStudentSubjectAssignbyCollege(printfrm.CollegeCode, printfrm.ApplicationNo, Convert.ToInt32(printfrm.CourseId));
            if (selsublist != null && selsublist.Count() > 0)
            {
                ViewBag.SubjectSelection = selsublist.ToList();
            }
           

            printfrm.ApplicationNo = appno;
            printfrm.EncrptedData = str;
            return View(printfrm);
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
