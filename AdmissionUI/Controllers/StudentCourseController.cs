using AdmissionData.Entities;
using AdmissionModel;
using AdmissionRepo;
using AdmissionUI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.NetworkInformation;
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

        public async Task<IActionResult> addCourse(string? tid)
        {
            string id = tid;
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
            var eligbilitylist = (await _iuow.masterRepo.GetAllCourseEligbility()).ToList();

            ViewBag.Courses = courselist;
            ViewBag.Eligbility = eligbilitylist;

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

            if (commandName!=null &&  commandName.ToLower().Contains("eligbile")  )
            {
                StudentAppliedCourse sc = new StudentAppliedCourse() {ApllicationNo=std.ApplicationNo,CourseId=std.CourseID };
                int res= await _iuow.studentApplyCourse.AddAsync(sc);

            }
            else
            {
               
                var res = 0;
                int scount = std.studentapplyList.Count(x => x.IsFor_Paid == true && x.IsPaid == 0);
                if (scount > 0)
                {
                    foreach (var item in std.studentapplyList)
                    {
                        if (item.IsFor_Paid)
                        {
                            StudentAppliedCourse studentApplied = new StudentAppliedCourse() { ApllicationNo = std.ApplicationNo, CourseId = item.CourseId, IsFor_Paid = item.IsFor_Paid };
                            int j = await _iuow.studentApplyCourse.AddAsync(studentApplied);
                        }
                    }
                    return RedirectToAction("courseFessPay1", new { tid = str });
                }
                else
                {

                    int icount = std.studentapplyList.Count(x => x.IsPaid == 1);
                    if (icount > 0)
                    {
                        return RedirectToAction("courseFessPay1", new { tid = str });
                    }
                    else
                    {

                        std.Status = -1;
                        std.Msg = "Please select  course in list for payment fees";
                    }
                }
                 

            }

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
            var eligbilitylist = (await _iuow.masterRepo.GetAllCourseEligbility()).ToList();
           
            ViewBag.Courses = courselist;
            ViewBag.Eligbility = eligbilitylist;

            std.ApplicationNo = appno;
            std.EncrptedData = str;
            return View(std);

        }



        [HttpPost]
        public async Task<IActionResult> deletestdChooseCourse([FromBody] HomeModels models)
        {
            var res= await _iuow.studentApplyCourse.DeleteAsync(models.AppNo, models.CourseId);
            return Ok( res );
        }


        
        public async Task<IActionResult> courseFessPay1(string? tid)
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
                std.studentapplyList = lisapplycourses.ToList().Where(x => x.IsFor_Paid == true  && x.IsPaid==0 && string.IsNullOrEmpty(x.TransactionID)  ).ToList();
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

        [HttpPost]
        public async Task<IActionResult> courseFessPay1(StudentApplyCourseModel std, string commandName)
        {

            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));

            if (commandName.ToLower() == "add")
            {
                StudentAppliedCourse sc = new StudentAppliedCourse() { ApllicationNo = std.ApplicationNo, CourseId = std.CourseID };
                int res = await _iuow.studentApplyCourse.AddAsync(sc);

            }
            else
            {
                var res = 0;
                var fees = std.studentapplyList.Sum(x => x.Fees);
                string courses = string.Join(",",std.studentapplyList.Select(x => x.CourseId));
                var grn =await _iuow.studentApplyCourse.genrateGrpFeesforPayment(std.ApplicationNo, std.studentapplyList[0].CourseId, fees);
                foreach (var item in std.studentapplyList)
                {
                    StudentAppliedCourse studentApplied = new StudentAppliedCourse() { ApllicationNo = std.ApplicationNo, CourseId = item.CourseId, TransactionID = grn, IsPaid=1 };
                   int j = await _iuow.studentApplyCourse.updateCourseFeesGrnStatus(studentApplied);
                  // int k = await _iuow.studentApplyCourse.updateCourseFeesPaymentStatus(studentApplied);
                }
                return Redirect("https://sabpaisa.agrauniv.online/newadmission/Request.aspx?appno=" + grn);
            }

            StudentMasters studentMasters = await _iuow.studentPreRepo.GetByIdAsync(appno);
            var lisapplycourses = await _iuow.studentApplyCourse.GetAllAsyncByAppNo(appno);
            if (lisapplycourses != null && lisapplycourses.Count() > 0)
            {
                std.studentapplyList = lisapplycourses.ToList().Where(x => x.IsFor_Paid == true).ToList(); ;
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

        public async Task<IActionResult> stdCollegeSelection(string? tid,string cid)
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            StudenCollegeModel std = new StudenCollegeModel();
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            //StudentMasters studentMasters = await _iuow.studentPreRepo.GetByIdAsync(appno);
            int icid = !string.IsNullOrEmpty(cid) ? Convert.ToInt32(cid) : 0;
            StudentAppliedColleges sc = new StudentAppliedColleges() {ApplicationNo=appno,CourseId= icid };
            var lisapplycollege = await _iuow.studentApplyCollege.GetAllStudentselectCollegeByCourseIdAsync (sc);
            if (lisapplycollege != null && lisapplycollege.Count() > 0)
            {
                std.studentapplycollegeList = lisapplycollege.ToList();
                    //.OrderBy(x=>x.CollegeName ).ThenBy(n => n.City).ThenBy(n => n.IsRWCollege).ToList();
            }
            else
            {
                std.studentapplycollegeList = new List<StudentAppliedColleges>();
            }

             std.ApplicationNo = appno;
             std.EncrptedData = str;
            
            return View(std);

             
        }

        
         [HttpPost]
        public async Task<IActionResult> ChooseCollege([FromBody]HomeModels models)
        {
            if (!string.IsNullOrEmpty(models.Ccode) && !string.IsNullOrEmpty(models.AppNo) && models.CourseId > 0)
            {
                StudentAppliedColleges entity = new StudentAppliedColleges()
                {
                    ApplicationNo = models.AppNo,
                    CCode=models.Ccode,
                    CourseId=models.CourseId,
                    IsSelect=models.CatId== 0?false:true,

                };
              var res=  await _iuow.studentApplyCollege.AddAsync(entity);
                return Ok(res);
            }
            else
            return Ok("-2");
        }

           
        public async Task<IActionResult> stdsubjectSelection(string?tid, string cid, string ccid)
        {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            StudenCollegeModel std = new StudenCollegeModel();
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
          
            
            StudentAppliedColleges sc = new StudentAppliedColleges() { ApplicationNo = appno, CourseId = 0 };
            var lisapplycollege = await _iuow.studentApplyCollege.GetAllStudentselectCollegeByCourseIdAsync(sc);
            if (lisapplycollege != null && lisapplycollege.Count() > 0)
            {
                std.studentapplycollegeList = lisapplycollege.ToList().Where(x=>x.SRNO>0).OrderBy(x => x.CollegeName).ToList();
            }
            else
            {
                std.studentapplycollegeList = new List<StudentAppliedColleges>();
            }

            std.ApplicationNo = appno;
            std.EncrptedData = str;

            return View(std);

        }


            public async Task<IActionResult> stdSubjects(string? tid, string cid, string ccid)
            {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            StudentSelectSubjectModel std = new StudentSelectSubjectModel();
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            //StudentMasters studentMasters = await _iuow.studentPreRepo.GetByIdAsync(appno);
            int icid = !string.IsNullOrEmpty(cid) ? Convert.ToInt32(cid) : 0;
            StudentAppliedColleges sc = new StudentAppliedColleges() { ApplicationNo = appno, CourseId = 0 };
            var listmajor = (await _iuow.masterRepo.GetAllCollegeMajorSubjects(ccid, icid)).ToList();
            var lisapplycollege = await _iuow.studentApplyCollege.GetAllStudentselectCollegeByCourseIdAsync(sc);
            if (lisapplycollege != null && lisapplycollege.Count() > 0)
            {
                var listcoll = lisapplycollege.ToList().Where(x => x.SRNO > 0 && x.CourseId== icid && x.CollegeCode==ccid).OrderBy(x => x.CollegeName).ToList();
               
                var listselectedcollege = new List<StudentAppliedCollegesSubject>();

                int uperlimt = listmajor.Count() > 10 ? 10: listmajor.Count();

                for(int rw=0;rw< uperlimt;rw++) 
                {
                listselectedcollege.Add(new StudentAppliedCollegesSubject() { ApplicationNo= listcoll[0].ApplicationNo,CourseId= listcoll[0].CourseId, CCode= listcoll[0].CollegeCode,CollegeCode= listcoll[0].CollegeCode, CollegeName= listcoll[0].CollegeName,CourseName= listcoll[0].CourseName,ChoiceOrder=rw+1 });
                }
                var studsubject = await _iuow.studentApplyCollege.getStudentAppliedCollegesSubject(new StudentAppliedCollegesSubject() { ApplicationNo = appno , CourseId =icid, CCode = ccid });
                
                if (studsubject != null)
                {
                    int idx = 0;
                    foreach (var item in listselectedcollege)
                    {
                        foreach (var subitem in studsubject)
                        {
                            if (listselectedcollege[idx].ApplicationNo == subitem.ApplicationNo && item.CCode == subitem.CCode && item.CourseId == subitem.CourseId && item.ChoiceOrder==subitem.ChoiceOrder)
                            {
                                listselectedcollege[idx].ChoiceOrder = subitem.ChoiceOrder;
                                listselectedcollege[idx].MajorSubjectID = subitem.MajorSubjectID;
                                listselectedcollege[idx].CoSubjectID = subitem.CoSubjectID;
                                listselectedcollege[idx].SkillSubjectID = subitem.SkillSubjectID;
                            }
                        }
                        idx++;
                    }

                }
                  std.stdapplycolsubList = listselectedcollege;
            }
            else
            {
                std.stdapplycolsubList = new List<StudentAppliedCollegesSubject>();
            }

            std.ApplicationNo = appno;
            std.EncrptedData = str;
            std.Ccode = ccid;
            std.CourseID = icid;
          
            listmajor.Insert(0, new Subjects { SubjectId = 0, Subject = "Select Major Minor Subject" });
            ViewBag.Majors = listmajor;

            var listco = (await _iuow.masterRepo.GetAllCollegeCOSubjects(ccid, icid)).ToList();
            listco.Insert(0, new Subjects { SubjectId = 0, Subject = "Select Co-Curricular Subject" });

            ViewBag.CoSubjects = listco;
            var listskill = (await _iuow.masterRepo.GetAllCollegeSkillSubjects(ccid, icid)).ToList();
            listskill.Insert(0, new Subjects { SubjectId = 0, Subject = "Select Vocational" });
            ViewBag.Skills = listskill;
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> stdSubjects(StudentSelectSubjectModel std )
        {
            int res = 0;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));

            foreach (var item in std.stdapplycolsubList)
            { 
                if(item.MajorSubjectID>0)
                {
                    if (item.CoSubjectID > 0 && item.SkillSubjectID > 0)
                    {

                          int j = await _iuow.studentApplyCollege.AddStudentAppliedCollegesSubject(item);
                          if (j > 0) res++;
                        
                          std.Status = 0;
                          std.Msg = "";
                    }
                    else
                    {
                        std.Status = -1;
                        std.Msg=  item.CoSubjectID ==0? (item.SkillSubjectID ==0? "Please select Co-Curricular and Skill Development Subject ":"" ):"Please select Co-Curricular Subject "  ;
                    }

                }
            
            
            }

            return RedirectToAction("stdchoosenSubjects", new { tid = str });

            //var listmajor = (await _iuow.masterRepo.GetAllCollegeMajorSubjects(std.Ccode, std.CourseID)).ToList();
            //listmajor.Insert(0, new Subjects { SubjectId = 0, Subject = "Select Major Minor Subject" });
            //ViewBag.Majors = listmajor;

            //var listco = (await _iuow.masterRepo.GetAllCollegeCOSubjects(std.Ccode, std.CourseID)).ToList();
            //listco.Insert(0, new Subjects { SubjectId = 0, Subject = "Select Co-Curricular Subject" });

            //ViewBag.CoSubjects = listco;
            //var listskill = (await _iuow.masterRepo.GetAllCollegeSkillSubjects(std.Ccode, std.CourseID)).ToList();
            //listskill.Insert(0, new Subjects { SubjectId = 0, Subject = "Select Vocational " });

            //ViewBag.Skills = listskill;

            //return View(std);
        }

        public async Task<IActionResult> stdchoosenSubjects(String?tid)
        {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            StudentChoosedSubjectModel studentSubjects = new StudentChoosedSubjectModel();
            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
           // StudentSelectSubjectModel std=new StudentSelectSubjectModel() {ApplicationNo=appno };
            var list = await _iuow.studentApplyCollege.getStudentChoosensSubject(appno);
            if (list != null && list.Count() > 0)
            {
                studentSubjects.studentSubjects = list.ToList();
                studentSubjects.Status = 0;
                studentSubjects.Msg = " ";
            }
            else {
                studentSubjects.Status = -1;
                studentSubjects.Msg = "No Subject Found ! ";
                studentSubjects.studentSubjects = new List<StudentSubjects>();
            }
            return View(studentSubjects);
        }


        public async Task<IActionResult> stdformprev(String? tid)
        {
            string id = tid;

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

            var lst = _iuow.IAdmin.GetStateList();
            

            var lstCdistrict = _iuow.IAdmin.GetDistrictList(Convert.ToInt32(printfrm.CState));
            var lstpdistrict = _iuow.IAdmin.GetDistrictList(Convert.ToInt32(printfrm.PState));
            
           
            if(lstCdistrict!=null && lstCdistrict.Count()>0)
            printfrm.CDistrict = lstCdistrict.Where(x => x.Id ==Convert.ToInt32(printfrm.CDistrict)).ToList().FirstOrDefault().Name;

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
            var lisapplycollege = await _iuow.studentApplyCollege.GetAllStudentselectCollegeByCourseIdAsync(sc);
            if (lisapplycollege != null && lisapplycollege.Count() > 0)
            {
              ViewBag.CollegeList  = lisapplycollege.ToList().Where(x=>!string.IsNullOrEmpty(x.CCode) ).OrderBy(x => x.CollegeName).ToList();
            }

            printfrm.ApplicationNo=appno;
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
