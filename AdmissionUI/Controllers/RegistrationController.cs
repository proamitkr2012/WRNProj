using AdmissionData.Entities;
using AdmissionRepo;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AdmissionModel;
using System.Linq.Expressions;

namespace AdmissionUI.Controllers
{
    public class RegistrationController : Controller
    {
        

        private readonly ILogger<RegistrationController> _logger;
        private readonly IUnitOfWork _iuow;
        private readonly IConfiguration _iconfig;
        public RegistrationController(IConfiguration iconfig,ILogger<RegistrationController> logger, IUnitOfWork iuow)
        {
            _logger = logger;
            _iuow = iuow;
            _iconfig = iconfig;
        }

        public IActionResult Index()
        {
            return View();
        }

         
        public async Task<IActionResult> preRegistration()
        {
            var listcoursetype = (await _iuow.masterRepo.GetCourseType()).ToList().Where(x => x.CourseTypeId > 1).ToList(); 
            listcoursetype.Insert(0, new CourseType { CourseTypeId = 0, CourseTypeName = "Select Course Type" });
            ViewBag.CourseType = listcoursetype;
            StudentPreRegModel studentPreReg = new StudentPreRegModel();
            return View(studentPreReg);
        }

        [HttpPost]
        public async Task<IActionResult> preRegistration(StudentPreRegModel studentPreReg)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    StudentMasters studentMasters = new StudentMasters();
                    studentMasters.Name = studentPreReg.StudentName;
                    studentMasters.FatherName = studentPreReg.FatherName;
                    studentMasters.Mobile = studentPreReg.MobileNo;
                    studentMasters.Email = studentPreReg.EmailID;
                    studentMasters.CourseTypeID= studentPreReg.CourseTypeID;
                    if (studentMasters.Mobile.Length == 10)
                    {
                        int ismobexists = await _iuow.studentPreRepo.IsMobileExistsAsync(studentPreReg.MobileNo);
                        if (ismobexists > 0)
                        {
                            var stddata  = await _iuow.studentPreRepo.GetByMobileAsync(studentPreReg.MobileNo);
                            string rnd = General.GenerateRandomOTP(4);
                            if (ismobexists == 1)//Mobile No exists but not verified
                            {
                                string tempid=  _iconfig.GetSection("SMS:OTPTEMPID").Value;
                                string msg= rnd+" is the OTP to validate your mobile at Dr. Bhimrao Ambedkar University. Kindly do not share the OTP with anyone ";
                                //Integrate Sms gateway Here 
                                var t=  await _iuow.iSMS.sendSMS(studentMasters.Mobile, msg, tempid);
                                var e = await _iuow.iSMS.sendMail(studentMasters.Email, "DBRAU OTP Verification ", msg);
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.PreRegID_PK, rnd));
                                return RedirectToAction("preVaildateOTP", new { tid = str });
                            }
                            else if (ismobexists == 2)//Mobile No exists and verified  but Password is Not Create
                            {
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.ApplicationNo, stddata.PreRegID_PK));
                                return RedirectToAction("createpassword", new { tid = str });
                            }
                            else if (ismobexists == 3)
                            {
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.ApplicationNo, stddata.PreRegID_PK));
                                return RedirectToAction("createpassword", new { tid = str });
                            }
                            else if (ismobexists == 4)
                            {
                               // string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.PreRegID_PK, rnd));
                                return RedirectToAction("stdlogin" );
                            }

                        }
                        else
                        {
                            int regid = await _iuow.studentPreRepo.AddAsync(studentMasters);
                            if (regid > 0)
                            {
                               
                                string rnd = General.GenerateRandomOTP(4);
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", regid, rnd));

                                //Integrate Sms gateway Here 
                                string tempid = _iconfig.GetSection("SMS:OTPTEMPID").Value;
                                string msg = rnd + " is the OTP to validate your mobile at Dr. Bhimrao Ambedkar University. Kindly do not share the OTP with anyone ";
                                //Integrate Sms gateway Here 
                                var t = await _iuow.iSMS.sendSMS(studentMasters.Mobile, msg, tempid);
                                var e = await _iuow.iSMS.sendMail(studentMasters.Email, "DBRAU OTP Verification ", msg);
                                return RedirectToAction("preVaildateOTP", new { tid = str });

                            }
                            else
                            {
                                studentPreReg.Status = -1;
                                studentPreReg.Msg = "OOps Server error !";

                            }
                        }
                    }
                    else
                    {
                        studentPreReg.Status = -1;
                        studentPreReg.Msg = "Invalid Mobile Number!";

                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                
                }

                return View(studentPreReg);
            }
            else
            {
                return View(studentPreReg);
            }
        }


        public async Task<IActionResult> preVaildateOTP(string? tid)
        {
            ValidatePreOTPModel validatePreOTP=new ValidatePreOTPModel();
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
                var std=await  _iuow.studentPreRepo.GetByPreIdAsync(Memcode);

                validatePreOTP.OTP = SMS;
                validatePreOTP.EncriptData = tid;
                validatePreOTP.Mobile = std.MobileNo.Substring(0, 2) + "XXXX" + std.MobileNo.Substring(6, std.MobileNo.Length - 6);

               


            }

            return View(validatePreOTP);
        }

        [HttpPost]
        public async Task<IActionResult> preVaildateOTP(ValidatePreOTPModel validatePreOTP)
        {
            string strReq = "";
            strReq = validatePreOTP.EncriptData.Replace("%2F", "/");
            //strReq = strReq.Substring(strReq.IndexOf('?') + 1);

            if (!strReq.Equals(""))
            {
                strReq = DecryptQueryString(strReq);
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string Memcode = "", OTPs = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                Memcode = arrIndMsg[1].ToString().Trim();
                arrIndMsg = arrMsgs[1].Split('='); //Get the Age
                OTPs = arrIndMsg[1].ToString().Trim();
                if (OTPs == validatePreOTP.InputOTP.Trim())
                {
                    var std = await _iuow.studentPreRepo.GetByPreIdAsync(Memcode);
                    StudentMasters studentMasters = new StudentMasters();
                    studentMasters.EntryID = Convert.ToInt32(Memcode);
                    studentMasters.Roll = "0";
                    studentMasters.Name = std.StudentName;
                    studentMasters.FatherName= std.FatherName;
                    studentMasters.Mobile = std.MobileNo;
                    studentMasters.Email = std.EmailID;
                    studentMasters.CourseTypeID = std.CourseTypeID;
                    studentMasters.IsNewadm = 1;
                    var response= await _iuow.studentPreRepo.VerifyOTP_InsertStudentData(studentMasters);
                    if (response.ResponseCode > 0)
                    {
                        //Responsemessage Contain ApplicationNO
                        string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}",response.ResponseMessage, Memcode));
                        return RedirectToAction("createpassword", new { tid = str });
                    }
                }
                else
                {
                    validatePreOTP.Status = -1;
                    validatePreOTP.Msg = "OTP is Invalid !";
                }
            
            
            
            
            }
            return View(validatePreOTP);
        }


        public async Task<IActionResult> createpassword(string? tid)
        {
            string id = tid;
            ValidatePreOTPModel validatePreOTP = new ValidatePreOTPModel();
            string strReq = "";
            strReq = id.Replace("%2F", "/");
            id = strReq;
            
            if (!strReq.Equals(""))
            {
                strReq = DecryptQueryString(strReq);
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string appno = "", preRegid = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                appno = arrIndMsg[1].ToString().Trim();
                arrIndMsg = arrMsgs[1].Split('='); //Get the Age
                preRegid = arrIndMsg[1].ToString().Trim();
            }
            validatePreOTP.EncriptData = id;
            return View(validatePreOTP);

        }

        [HttpPost]
        public async Task<IActionResult> createpassword(ValidatePreOTPModel validatePreOTP)
        {

            string strReq = "";
            strReq = validatePreOTP.EncriptData.Replace("%2F", "/");


            if (!strReq.Equals(""))
            {

                if (string.Compare(validatePreOTP.InputOTP, validatePreOTP.OTP) == 0)
                {
                    strReq = DecryptQueryString(strReq);
                    string[] arrMsgs = strReq.Split('&');
                    string[] arrIndMsg;
                    string appno = "", preRegid = "";
                    arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                    appno = arrIndMsg[1].ToString().Trim();
                    arrIndMsg = arrMsgs[1].Split('='); //Get the Age
                    preRegid = arrIndMsg[1].ToString().Trim();
                    StudentMasters std = new StudentMasters() {ApplicationNo= appno,PWD= validatePreOTP.InputOTP };
                    int res=  await _iuow.studentPreRepo.ChangePassword(std);
                    if (res > 0)
                    {
                        return RedirectToAction("stdlogin");
                    }

                }
                else
                {
                    validatePreOTP.Status = -1;
                    validatePreOTP.Msg = "New password is not match with confirm password";
                }
            
            
            }

            return View(validatePreOTP);
        }

        public IActionResult stdlogin()
        {
            LoginModel login = new LoginModel();
            return View(login);
             
        }

        [HttpPost]
        public async Task<IActionResult> stdlogin(LoginModel login)
        {

            if (ModelState.IsValid)
            {
                StudentMasters std = new StudentMasters();
                std.ApplicationNo = login.UserName.Trim();
                std.PWD = login.PWd.Trim();
                var stddata=  await _iuow.studentPreRepo.CheckAuthuntication(std);
                if (stddata != null && stddata.ApplicationNo != null)
                {
                    if (stddata.IsActive == true)
                    {

                        var identity = new ClaimsIdentity(new[] {
                     new Claim(ClaimTypes.Sid,stddata.ApplicationNo )
                    ,new Claim(ClaimTypes.Role,"0" )
                    ,new Claim(ClaimTypes.Name,stddata.Name )
                    ,new Claim(ClaimTypes.PrimarySid,"")
                    },CookieAuthenticationDefaults.AuthenticationScheme); 
                     var principal = new ClaimsPrincipal(identity);
                     var stdlogin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                     string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.ApplicationNo, 1));
                     return RedirectToAction("stdprofile", "Student", new { tid = str });
                      
                    }
                    else
                    {
                        login.Status = -1;
                        login.Msg = "You are inactive user, please contact with admin";
                    }

                }
                else
                {
                    login.Status = -1;
                    login.Msg = "Invalid Username or password";
                }


            }

            return View(login);

        }

        public async Task<IActionResult> forgotPassword()
        {
            ValidatePreOTPModel vl = new ValidatePreOTPModel();
            return View(vl);
        }

        [HttpPost]
        public async Task<IActionResult> forgotPassword(ValidatePreOTPModel vl)
        {
            try
            {
                if (!string.IsNullOrEmpty(vl.InputOTP))
                {
                    var std = await _iuow.studentPreRepo.GetByMobileNoAsync(vl.InputOTP);

                    if (!string.IsNullOrEmpty(std.ApplicationNo))
                    {
                        string rnd = General.GenerateRandomOTP(4);
                        string msg = rnd + " is the OTP to validate your mobile at Dr. Bhimrao Ambedkar University. Kindly do not share the OTP with anyone ";
                        string tempid = _iconfig.GetSection("SMS:OTPTEMPID").Value;
                        var t = await _iuow.iSMS.sendSMS(std.Mobile, msg, tempid);
                        var e = await _iuow.iSMS.sendMail(std.Email, "DBRAU OTP Verification ", msg);

                        string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", std.ApplicationNo, rnd));
                        return RedirectToAction("forgotPwdOTP", new { tid = str });
                    }
                    else
                    {
                        vl.Status = -1;
                        vl.Msg = "Registered Mobile No. is not valid ";
                        return View(vl);
                    }

                }
                else
                {
                    vl.Status = -1;
                    vl.Msg = "Please enter registered Mobile No.";
                    return View(vl);

                }
            }
            catch (Exception ex)
            {
                vl.Status = -1;
                vl.Msg = " Enter Mobile No. is not valid ";
                return View(vl);

            }

        }


        public async Task<IActionResult> forgotPwdOTP(string? tid)
        {


            ValidatePreOTPModel validatePreOTP = new ValidatePreOTPModel();
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
                var std = await _iuow.studentPreRepo.GetByIdAsync(Memcode);

                validatePreOTP.OTP = SMS;
                validatePreOTP.EncriptData = tid;
                validatePreOTP.Mobile = std.Mobile.Substring(0, 2) + "XXXX" + std.Mobile.Substring(6, std.Mobile.Length - 6);
            }

            return View(validatePreOTP);

        }

        [HttpPost]
        public async Task<IActionResult> forgotPwdOTP(ValidatePreOTPModel validatePreOTP)
        {
            string strReq = "";
            strReq = validatePreOTP.EncriptData.Replace("%2F", "/");
            //strReq = strReq.Substring(strReq.IndexOf('?') + 1);

            if (!strReq.Equals(""))
            {
                strReq = DecryptQueryString(strReq);
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string Memcode = "", OTPs = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                Memcode = arrIndMsg[1].ToString().Trim();
                arrIndMsg = arrMsgs[1].Split('='); //Get the Age
                OTPs = arrIndMsg[1].ToString().Trim();
                if (OTPs == validatePreOTP.InputOTP.Trim())
                {
                    //Responsemessage Contain ApplicationNO
                    string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", Memcode, Memcode));
                    return RedirectToAction("createpassword", new { tid = str });

                }
                else
                {
                    validatePreOTP.Status = -1;
                    validatePreOTP.Msg = "OTP is Invalid !";
                }

            }
            return View(validatePreOTP);

        }

       public async Task<IActionResult> stdlogout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("stdlogin");
        
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
//return RedirectToAction("ACTION", "CONTROLLER", new
//{
//    id = 99,
//    otherParam = "Something",
//    anotherParam = "OtherStuff"
//});