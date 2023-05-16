using AdmissionRepo.Utilities;
using AdmissionRepo;
using AdmissionUI.Helpers;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AdmissionModel.DTO;
using AdmissionData.Entities;
using AdmissionModel;
using System.Globalization;
using AdmissionData.Dapper;
using QRCoder;
using System.Drawing;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Rotativa.AspNetCore.Options;
using Rotativa.AspNetCore;

namespace AdmissionUI.Controllers
{
    [CanonicalActionFilter]
    public class HomeController : BaseController
    {
        private IHostingEnvironment hostingEnv;
        SmsClient sms = new SmsClient();
        int pageSize;
        int pageSizeJobs; IDapperContext _dapperContext;

        private IMailClient _mailClient;
        public HomeController(IConfiguration _config, IHttpContextAccessor _httpContextAccessor, IUnitOfWork uow, IMailClient mailClient, IDapperContext dapperContext, IHostingEnvironment Env) : base(_httpContextAccessor, _config, uow)
        {
            hostingEnv = Env;
            _dapperContext = dapperContext;
            _mailClient = mailClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            try
            {

                if (model.Enrollment.Trim() != "")
                {
                    var datet = Convert.ToDateTime(model.Dob, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                    model.DobDATE = datet;
                    StudentMastersDTO adminData = UOF.IAdmin.CheckBedStudentLogin(model);
                    if (adminData != null)
                    {
                        adminData.EncrptedRoll = AESEncription.Base64Encode(adminData.Roll);
                        return Json(new { data = adminData, res = "success" });
                    }

                }
                else
                {
                    return Json("The Email or Password is not provided!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }
            return Json("Internal error!");
        }
        public IActionResult Search([FromBody] LoginDTO model)
        {
            try
            {

                if (model.Enrollment.Trim() != "")
                {
                    var datet = Convert.ToDateTime(model.Dob, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                    model.DobDATE = datet;

                    var d = UOF.IAdmin.GetStudentMasterData(model.Enrollment);
                    if (d != null)
                    {
                        return Json(new { res = "1" });
                    }
                    StudentMastersDTO adminData = UOF.IAdmin.CheckStudentPreData(model);
                    if (adminData != null)
                    {
                        adminData.EncrptedRoll = AESEncription.Base64Encode(adminData.Roll);
                        return Json(new { data = adminData, res = "success" });
                    }

                }
                else
                {
                    return Json("The Email or Password is not provided!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }
            return Json("Internal error!");
        }
        [Route("~/admission-form-step1/{id}")]
        public IActionResult AdmissionForm_Step1(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = "select * from StudentMasters where Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;
                    if (model != null)
                    {
                        if (model.IsPaid == true)
                        {
                            model.EncrptedRoll = AESEncription.Base64Encode(model.Roll);
                            return RedirectToAction("AdmissionForm_Download", new { id = model.EncrptedRoll });
                        }
                    }

                    StudentMastersDTO adminData = UOF.IAdmin.GetStudentPreData(Enrollment);
                    if (adminData != null)
                    {
                        adminData.EncrptedRoll = AESEncription.Base64Encode(adminData.Roll);
                        return View(adminData);
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }
        [Route("~/admission-form-step2/{id}")]
        public IActionResult AdmissionForm_Step2(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    ViewBag.StateList = UOF.IAdmin.GetStateList();
                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO adminData = UOF.IAdmin.CheckBedStudentDATA(Enrollment);
                    if (adminData != null)
                    {
                        adminData.EncrptedRoll = AESEncription.Base64Encode(adminData.Roll);
                        return View(adminData);
                    }
                    else
                    {
                        adminData = new StudentMastersDTO();
                        adminData.EncrptedRoll = id;
                        return View(adminData);
                    }

                }


            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }
        [Route("~/admission-form-step3/{id}")]
        public IActionResult AdmissionForm_Step3(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = "select * from StudentMasters where Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;

                    model.QualifationList = UOF.IAdmin.GetQualificationList(Enrollment);
                    if (model != null)
                    {
                        //model.DocRequired = UOF.IAdmin.GetDocRequiredList(1);
                        model.EncrptedRoll = id;

                        return View(model);
                    }


                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }
        [Route("~/admission-form-step4/{id}")]
        public IActionResult AdmissionForm_Step4(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = "select * from StudentMasters where Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;

                    if (model != null)
                    {
                        model.GetDocUoloadList = UOF.IAdmin.GetDocUoloadList(Enrollment);
                        model.EncrptedRoll = id;

                        return View(model);
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }
        [Route("~/admission-form-step5/{id}")]
        public IActionResult AdmissionForm_Step5(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = @"select * ,Cname as CollegeName from StudentMasters 
JOIN CollegeMasters ON StudentMasters.CollegeCode=CollegeMasters.InstCode
where Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;

                    if (model != null)
                    {

                        model.EncrptedRoll = id;
                        model.GetDocUoloadList = UOF.IAdmin.GetDocUoloadList(Enrollment);
                        ViewBag.Photo = model.GetDocUoloadList.Where(x => x.DocName == "Photo").Select(x => x.FilePath).FirstOrDefault();
                        ViewBag.Signature = model.GetDocUoloadList.Where(x => x.DocName == "Signature").Select(x => x.FilePath).FirstOrDefault();
                        model.QualifationList = UOF.IAdmin.GetQualificationList(Enrollment);
                        QRCodeGenerator _qrCode = new QRCodeGenerator();
                        QRCodeData _qrCodeData = _qrCode.CreateQrCode(Enrollment, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(_qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        model.qcore = BitmapToBytesCode(qrCodeImage);

                        return View(model);
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }

        [NonAction]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        [Route("~/admission-form-step6/{id}")]
        public IActionResult AdmissionForm_Step6(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = "select * from StudentMasters where Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;

                    if (model != null)
                    {

                        model.EncrptedRoll = id;

                        return View(model);
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }
        [Route("~/admission-form-download/{id}")]
        public IActionResult AdmissionForm_Download(String id = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(id))
                {

                    var Enrollment = AESEncription.Base64Decode(id);
                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = @"select * ,Cname as CollegeName from StudentMasters 
JOIN CollegeMasters ON StudentMasters.CollegeCode=CollegeMasters.InstCode
where  Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;

                    if (model != null)
                    {
                        Enrollment = model.Roll;

                        model.EncrptedRoll = id;
                        model.GetDocUoloadList = UOF.IAdmin.GetDocUoloadList(Enrollment);
                        ViewBag.Photo = model.GetDocUoloadList.Where(x => x.DocName == "Photo").Select(x => x.FilePath).FirstOrDefault();
                        ViewBag.Signature = model.GetDocUoloadList.Where(x => x.DocName == "Signature").Select(x => x.FilePath).FirstOrDefault();
                        model.QualifationList = UOF.IAdmin.GetQualificationList(Enrollment);
                        QRCodeGenerator _qrCode = new QRCodeGenerator();
                        QRCodeData _qrCodeData = _qrCode.CreateQrCode(Enrollment, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(_qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        model.qcore = BitmapToBytesCode(qrCodeImage);

                        return View(model);
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Registration");
        }

        [Route("~/downloadform/{id}")]
        public ActionResult DownloadForm(String id = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var Enrollment = AESEncription.Base64Decode(id);

                    StudentMastersDTO model = new StudentMastersDTO();
                    string sqlComand = @"select * ,Cname as CollegeName from StudentMasters 
JOIN CollegeMasters ON StudentMasters.CollegeCode=CollegeMasters.InstCode
where  Roll='" + Enrollment.Trim() + "'";

                    model = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;

                    if (model != null)
                    {
                         Enrollment = model.Roll;

                        model.EncrptedRoll = id;
                        model.GetDocUoloadList = UOF.IAdmin.GetDocUoloadList(Enrollment);
                        ViewBag.Photo = WebConfigSetting.ImgCloudPath + model.GetDocUoloadList.Where(x => x.DocName == "Photo").Select(x => x.FilePath).FirstOrDefault();
                        ViewBag.Signature = WebConfigSetting.ImgCloudPath + model.GetDocUoloadList.Where(x => x.DocName == "Signature").Select(x => x.FilePath).FirstOrDefault();
                        model.QualifationList = UOF.IAdmin.GetQualificationList(Enrollment);
                        QRCodeGenerator _qrCode = new QRCodeGenerator();
                        QRCodeData _qrCodeData = _qrCode.CreateQrCode(model.WRN, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(_qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        model.qcore = BitmapToBytesCode(qrCodeImage);

                        return new ViewAsPdf("DownloadForm", model)
                        {
                            FileName = model.WRN + "_" + "Form.pdf",
                            //PageMargins = new Margins(10, 1, 0, 0),
                            PageOrientation = Orientation.Portrait,
                        };
                        //return View(model);

                    }

                }




            }
            catch (Exception e)
            {

            }
            return View();
        }

        public ActionResult GetPDistrictList(int id = 0)
        {
            try
            {
                List<tblDistrict> District = UOF.IAdmin.GetDistrictList(id);
                return PartialView("_PDistrictList", District);
            }
            catch (Exception e)
            {

            }
            return PartialView("_PDistrictList");
        }
        public ActionResult GetCDistrictList(int id = 0)
        {
            try
            {
                List<tblDistrict> District = UOF.IAdmin.GetDistrictList(id);
                return PartialView("_CDistrictList", District);
            }
            catch (Exception e)
            {

            }
            return PartialView("_CDistrictList");
        }


        [HttpPost]
        public async Task<IActionResult> Step2Profile([FromBody] StudentMastersDTO model)
        {
            try
            {
                //StudentMastersDTO model = new StudentMastersDTO();
                var datet = Convert.ToDateTime(model.DOBStr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                model.DOB = datet;
                model.DegreeID = 1;
                int appid = await UOF.IAdmin.AddRegistration(model);
                return Json(appid);

            }
            catch (Exception e)
            {

            }
            return Json(null);

        }
        [HttpPost]
        public async Task<IActionResult> Step3Quali([FromBody] QualifationMasters model)
        {
            try
            {
                //StudentMastersDTO model = new StudentMastersDTO();
                //var datet = Convert.ToDateTime(model.DOBStr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                //model.DOB = datet;
                //model.DegreeID = 1;
                bool appid = await UOF.IAdmin.UpdateQualification(model);
                if (appid == true)
                {
                    return Json(appid);
                }
                ///

            }
            catch (Exception e)
            {

            }
            return Json(null);

        }
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFile()
        {
            try
            {
                string _imgname = string.Empty;

                string pathurl = "";
                var file = Request.Form.Files[0];
                int EntryID = Convert.ToInt32(Request.Form["EntryID"]);
                if (file.Length > 0)
                {
                    var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var Name = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"');
                    List<string> fileParts = fileName.Split('.').ToList();

                    var ext = fileParts[1].ToString();

                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string myfile = time + "." + ext;

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
                    else if (Name == "Signature")
                    {
                        var path = hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Signature\\" + myfile;
                        bool exists = System.IO.Directory.Exists(hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Signature\\");
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Signature\\");

                        }
                        using (FileStream fs = System.IO.File.Create(path))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        s.FilePath = WebConfigSetting.DocPath + "\\Signature\\" + myfile;
                    }
                    else
                    {
                        var path = hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Doc\\" + myfile;
                        bool exists = System.IO.Directory.Exists(hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Doc\\");
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(hostingEnv.WebRootPath + WebConfigSetting.DocPath + "\\Doc\\");

                        }
                        using (FileStream fs = System.IO.File.Create(path))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        s.FilePath = WebConfigSetting.DocPath + "\\Doc\\" + myfile;
                    }

                    s.EntryID = EntryID;
                    s.VirtualFilePath = s.FilePath;

                    if (s.EntryID > 0)
                    {
                        bool sts = UOF.IAdmin.UploadDocData(s);

                        return Json(sts);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json("fail");
        }
        public async Task<ActionResult> SignOut()
        {
            if (CurrentUser != null)
            {
                if (CurrentUser.Roles.Contains("Student") == true)
                {
                    string[] Roles = CurrentUser != null ? CurrentUser.Roles : new string[] { "" };

                    await httpContextAccessor.HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string[] Roles = CurrentUser != null ? CurrentUser.Roles : new string[] { "" };

                    await httpContextAccessor.HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Login", "Account", new { Area = "Admin" });

                }
            }
            return RedirectToAction("Index", "Home");
        }


    }
}