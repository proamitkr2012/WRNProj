using AdmissionData.Entities;
using AdmissionModel;
using AdmissionModel.Entity;
using AdmissionRepo;
using AdmissionUI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AdmissionUI.Controllers
{
    public class StudentController : Controller
    {

        private readonly ILogger<StudentController> _logger;
        private readonly IUnitOfWork _iuow;
        private IHostingEnvironment hostingEnv;
        public StudentController(ILogger<StudentController> logger, IUnitOfWork iuow, IMasterRepo imasterRepo, IHostingEnvironment Env)
        {
            _logger = logger;
            _iuow = iuow;
            hostingEnv = Env;
        }
       
         




        public IActionResult dashboard()
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            //string strReq = "";
            //if (id != null)
            //{
            //    strReq = id.Replace("%2F", "/");
            //    id = strReq;
            //}

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            

            return View();
        }
         
        public async Task<IActionResult> stdprofile(string? tid)
        {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
           
            try
            {

                var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
                var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
                
                var stdmaster = await _iuow.studentPreRepo.GetByIdAsync(appno);

               //var lst = _iuow.IAdmin.GetStateList();

                var lst = _iuow.IAdmin.GetStateList().Where(x => x.Id > 0).OrderByDescending(x => x.SortOrder).ToList();
                lst.Insert(0, new tblState { Id = 0, Name = "Select State" });
                ViewBag.States = lst;
                int icstate = !string.IsNullOrEmpty(stdmaster.CState) ? Convert.ToInt32(stdmaster.CState) : 0; 
                var lstCdistrict = _iuow.IAdmin.GetDistrictList(icstate);
                lstCdistrict.Insert(0, new tblDistrict { Id = 0, Name = "Select District" });
                ViewBag.CCities = lstCdistrict;

                int ipstate = !string.IsNullOrEmpty(stdmaster.PState) ? Convert.ToInt32(stdmaster.PState) : 0;
                var lstpdistrict = _iuow.IAdmin.GetDistrictList(ipstate);
                lstpdistrict.Insert(0, new tblDistrict { Id = 0, Name = "Select District" });
                ViewBag.PCities = lstpdistrict;




                var listcategory = (await _iuow.masterRepo.GetCategory()).ToList();
                listcategory.Insert(0, new StudentCategory { CategoryId = 0, Category = "Select Category" });
                ViewBag.Category = listcategory;
                var listsubcate = (await _iuow.masterRepo.GetSubCategory(0)).ToList();
                listsubcate.Insert(0, new StudentSubcategory { SubCategoryId = 0, SubCategory = "Select SubCategory" });
                ViewBag.Subcategory = listsubcate;
               

                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<StudentMasters, StudentRegModel>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                var std = mapper.Map<StudentRegModel>(stdmaster);
                std.EncrptedData = str;
                std.Newdata = !string.IsNullOrEmpty(stdmaster.Aadhar) ? 0 : 1;
                return View(std);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                StudentRegModel std = new StudentRegModel();
                std.Status = -1;
                std.Msg = ex.Message;   
                return View(std);

            }
        }

        [HttpPost]
        public async Task<IActionResult> stdprofile(StudentRegModel std)
        {

            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }
            //string strReq = "";
            //if (id != null)
            //{
            //    strReq = id.Replace("%2F", "/");
            //    id = strReq;
            //}


            try
            {

                var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
                var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
                var lst = _iuow.IAdmin.GetStateList().Where(x => x.Id > 0).OrderByDescending(x => x.SortOrder).ToList();
                lst.Insert(0, new tblState { Id = 0, Name = "Select State" });
                ViewBag.States = lst;


                int icstate = !string.IsNullOrEmpty(std.CState) ? Convert.ToInt32(std.CState) : 0;
                var lstCdistrict = _iuow.IAdmin.GetDistrictList(icstate);
                lstCdistrict.Insert(0, new tblDistrict { Id = 0, Name = "Select District" });
                ViewBag.CCities = lstCdistrict;

                int ipstate = !string.IsNullOrEmpty(std.PState) ? Convert.ToInt32(std.PState) : 0;
                var lstpdistrict = _iuow.IAdmin.GetDistrictList(ipstate);
                lstpdistrict.Insert(0, new tblDistrict { Id = 0, Name = "Select District" });
                ViewBag.PCities = lstpdistrict;



                var listcategory = (await _iuow.masterRepo.GetCategory()).ToList();
                listcategory.Insert(0, new StudentCategory { CategoryId = 0, Category = "Select Category" });
                ViewBag.Category = listcategory;
                var listsubcate = (await _iuow.masterRepo.GetSubCategory(0)).ToList();
                listsubcate.Insert(0, new StudentSubcategory { SubCategoryId = 0, SubCategory = "Select SubCategory" });
                ViewBag.Subcategory = listsubcate;
               // var errorList = ModelState.Where(x => x.Value.Errors.Count > 0)
               //.ToDictionary(kvp => kvp.Key, kvp => ((int)kvp.Value.ValidationState));

                if (ModelState.IsValid)
                {
                    var dob = Convert.ToDateTime(std.DOB);
                    std.DOB = dob.ToString("yyyy-MM-dd");
                    var config = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap<StudentRegModel, StudentMasters >();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper = new Mapper(config);
                    var stdmaster = mapper.Map<StudentMasters>(std);
                    int res=await _iuow.studentPreRepo.UpdateAsync(stdmaster);
                    if (res > 0)
                    {
                        string enc = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
                        return RedirectToAction("stdQualification", new { tid = enc });

                    }
                    else
                    {
                        std.Status = -1;
                        std.Msg = "OOps somthing error,record has not been saved !";
                    }

                }
                return View(std);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                std.Status = -1;
                std.Msg = ex.Message;
                return View(std);

            }
        }

        public async Task<IActionResult> stdQualification(string? tid)
        {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));


            EligibiltyModel eligibiltyModel =new EligibiltyModel();
            eligibiltyModel.EncrptedData = str;
            eligibiltyModel.ApplicationNo = appno;
            var qulif= (await _iuow.qulificationRepo.GetAllByAppNoAsync(appno)).ToList();
            var linewcount = qulif.Where(o => o.DocID == 0); 


            List<StudentQualificationModel>li=new List<StudentQualificationModel>();
           
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<StudentQualification, StudentQualificationModel>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);


            foreach (var item in qulif)
                {
                var stdmaster = mapper.Map<StudentQualificationModel>(item);
                stdmaster.ApplicationNo = appno;
                li.Add(stdmaster);
            }

            eligibiltyModel.QualifationList= li;
             var lstboard = (await _iuow.masterRepo.GetEducationalBoard()).ToList();
            lstboard.Insert(0, new EducationalBoard { EDUBoardId = 0, Board = "Select Board" });
            ViewBag.Boards = lstboard;
            var lstuniv = (await _iuow.masterRepo.GetAlllUniversity()).ToList();
            lstuniv.Insert(0, new Universty { UniversityID = 0, University = "Select University" });
            ViewBag.University = lstuniv;

            if (linewcount.Count() == 0)
            {
                eligibiltyModel.Newdata = 0;
            }

            return View(eligibiltyModel);
        }


        [HttpPost]
        public async Task<IActionResult> stdQualification(EligibiltyModel elg )
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            ViewBag.Boards = await _iuow.masterRepo.GetEducationalBoard();
            ViewBag.University = await _iuow.masterRepo.GetAlllUniversity();
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<StudentQualificationModel, StudentQualification >();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            var res = 0;
            
            foreach (var item in elg.QualifationList)
            {
                var stdquli = mapper.Map<StudentQualification>(item);
                    stdquli.ApplicationNo = elg.ApplicationNo;

                if (item.Board_Universty_Name.ToLower() == "other")
                {
                    if (item.DocName == "Graduation")
                    {
                        Universty uni = new Universty() { University = item.Board_Other };
                        await _iuow.masterRepo.SaveUniversity(uni);
                    }
                    else
                    {
                        EducationalBoard edu = new EducationalBoard() { Board = item.Board_Other };
                        await _iuow.masterRepo.SaveEducationalBoard(edu);
                    }
                    stdquli.Board_Universty_Name = item.Board_Other;
                    
                }

                int j = await _iuow.qulificationRepo.AddAsync(stdquli);



                if(j > 0)
                {
                    res++;
                }

             
            }
            if(res>0)
            {
                return RedirectToAction("stdWeightage", new { tid = str });

            }
            
             return View(elg);
        }


        public async Task<IActionResult> stdWeightage(string? tid)
        {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            StdWeightageEligbilty eligibiltyModel = new StdWeightageEligbilty();
            eligibiltyModel.EncrptedData = str;
            eligibiltyModel.ApplicationNo = appno;
            var wt = (await _iuow.StdWeightageRep.GetStudentWeightagesAsync(appno)).ToList();

            List<stdWeightageModel> li = new List<stdWeightageModel>();

            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<StudentWeightage, stdWeightageModel>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            foreach (var item in wt)
            {
                var stdmaster = mapper.Map<stdWeightageModel>(item);
                stdmaster.ApplicationNo = appno;
                li.Add(stdmaster);
            }
            eligibiltyModel.stdweightageList = li;
           return View(eligibiltyModel);

        }

        [HttpPost]
        public async Task<IActionResult> stdWeightage(StdWeightageEligbilty eligibiltyModel)
        {
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
            List<StudentWeightage> li = new List<StudentWeightage>();
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<stdWeightageModel, StudentWeightage>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            var linewcount = eligibiltyModel.stdweightageList.Where(o => o.ctrlWeightage == true);
            int res = 0;
            if (linewcount.Count() > 0)
            {
                int r= await  _iuow.StdWeightageRep.DeleteAsync(eligibiltyModel.ApplicationNo);
                foreach (var item in eligibiltyModel.stdweightageList)
                {
                    if (item.ctrlWeightage == true)
                    {
                        var stdquli = mapper.Map<StudentWeightage>(item);
                        stdquli.ApplicationNo = eligibiltyModel.ApplicationNo;
                        int d = await _iuow.StdWeightageRep.AddAsync(stdquli);
                        if (d > 0)
                        { r++; }
                    }
                }

              

            }

            return RedirectToAction("stduploadDocs", new { tid = str });

            
        }


        public async Task<IActionResult> stduploadDocs(string? tid)
        {
            string id = tid;
            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
            {
                return RedirectToAction("stdlogin", "Registration");
            }

            var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));

            StudentMasters studentMasters =await _iuow.studentPreRepo.GetByIdAsync(appno);
            StdDocUploadModel stddoc = new StdDocUploadModel();
            stddoc.ApplicationNo = appno;
            stddoc.EncrptedData = str;
            stddoc.Std_Photo =string.IsNullOrEmpty( studentMasters.Std_PHOTO)?"Photo.jpg": studentMasters.Std_PHOTO;
            return View(stddoc);
        }





        //public async Task<IActionResult> ChooseCourse_College(string? id)
        //{
        //    if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count == 0)
        //    {
        //        return RedirectToAction("stdlogin", "Registration");
        //    }
            
        //    var appno = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
        //    var FullName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        //    string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", appno, 0));
        //    StudentMasters studentMasters = await _iuow.studentPreRepo.GetByIdAsync(appno);
            
        //    ViewBag.Courses = await _iuow.masterRepo.GetAllCoursebyCourseType(studentMasters.CourseTypeID);
        //    StudentCollegesModel std = new StudentCollegesModel();
        //    std.ApplicationNo = appno;
        //    std.EncrptedData = str;
            
        //    return View(std);
        //}



            [HttpPost]
        public async Task<IActionResult> checkAadhar([FromBody] HomeModels models)
        {
            var result =await _iuow.studentPreRepo.IsAadharExists(models.Aadhar);
            return Ok(result);
        }



         
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                string _imgname = string.Empty;

                string pathurl = "";
                var file = Request.Form.Files[0];
                string  EntryID = Request.Form["EntryID"];
                if (file.Length > 0)
                {
                    var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var Name = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"');
                    List<string> fileParts = fileName.Split('.').ToList();

                    var ext = fileParts[1].ToString();

                    string name = Path.GetFileNameWithoutExtension(fileName);
                   // string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string myfile = EntryID + "." + ext;

                    StudentDocUploaded s = new StudentDocUploaded();
                    if (Name == "Photo")
                    {
                        var path = hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Photo\\" + myfile;
                        bool exists = System.IO.Directory.Exists(hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Photo\\");
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Photo\\");

                        }
                        using (FileStream fs = System.IO.File.Create(path))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        s.FilePath = WebConfigSetting.DocPath + "\\Photo\\" + myfile;
                    }
                   
                        var sts = await _iuow.studentPreRepo.UploadDocData(EntryID,s.FilePath);
                        return Json(sts);
                     
                }
            }
            catch (Exception ex)
            {
            }
            return Json("-1");
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

//private bool IsValidate(Dictionary<string, int> errorList)
//{
//    string[] campareModel = { "HOSTYPE", "HOSNAME", "HOSMOBILE", "HOSREG_DATE", "HOSLICENSENO", "HOS_COUNCILNAME", "HOSEMAIL", "HOSADDRESS", "HOSSTATE", "HOSCITY", "HOSAREA", "HOSPINCODE", "HOSUSERNAME", "HOSPassword" };//, "HOSLOGO"
//    bool flag = true;
//    var result = errorList.Where(err =>
//                        campareModel.Any(foo => foo == err.Key));
//    flag = (result.ToList().Count > 0) ? false : true;
//    return flag;
//}