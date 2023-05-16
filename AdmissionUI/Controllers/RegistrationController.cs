﻿using AdmissionData.Entities;
using AdmissionRepo;
using AdmissionUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdmissionUI.Controllers
{
    public class RegistrationController : Controller
    {
        

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _iuow;

        public RegistrationController(ILogger<HomeController> logger, IUnitOfWork iuow)
        {
            _logger = logger;
            _iuow = iuow;

        }

        public IActionResult Index()
        {
            return View();
        }

         
        public IActionResult preRegistration()
        {
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
                    if (studentMasters.Mobile.Length == 10)
                    {
                        int ismobexists = await _iuow.studentPreRepo.IsMobileExistsAsync(studentPreReg.MobileNo);
                        if (ismobexists > 0)
                        {
                            var stddata  = await _iuow.studentPreRepo.GetByMobileAsync(studentPreReg.MobileNo);
                            string rnd = General.GenerateRandomOTP(4);
                            if (ismobexists == 1)//Mobile No exists but not verified
                            {

                                //Integrate Sms gateway Here 
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.PreRegID_PK, rnd));
                                return RedirectToAction("preVaildateOTP", new { id = str });
                            }
                            else if (ismobexists == 2)//Mobile No exists and verified  but Password is Not Create
                            {
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.ApplicationNo, stddata.PreRegID_PK));
                                return RedirectToAction("createpassword", new { id = str });
                            }
                            else if (ismobexists == 3)
                            {
                                string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}", stddata.ApplicationNo, stddata.PreRegID_PK));
                                return RedirectToAction("createpassword", new { id = str });
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
                                return RedirectToAction("preVaildateOTP", new { id = str });

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


        public async Task<IActionResult> preVaildateOTP(string? id)
        {
            ValidatePreOTPModel validatePreOTP=new ValidatePreOTPModel();
            string strReq = "";
            strReq =id.Replace("%2F", "/");
            id = strReq;
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
                validatePreOTP.EncriptData = id;
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
                    studentMasters.IsNewadm = 1;
                    var response= await _iuow.studentPreRepo.VerifyOTP_InsertStudentData(studentMasters);
                    if (response.ResponseCode > 0)
                    {
                        //Responsemessage Contain ApplicationNO
                        string str = EncryptQueryString(string.Format("MEMCODE={0}&SMS={1}",response.ResponseMessage, Memcode));
                        return RedirectToAction("createpassword", new { id = str });
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


        public async Task<IActionResult> createpassword(string? id)
        {
            ValidatePreOTPModel validatePreOTP = new ValidatePreOTPModel();
            string strReq = "";
            strReq = id.Replace("%2F", "/");
            id = strReq;
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
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var stdlogin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("dashboard", "Student");
                        //return RedirectToAction("ACTION", "CONTROLLER", new
                        //{
                        //    id = 99,
                        //    otherParam = "Something",
                        //    anotherParam = "OtherStuff"
                        //});
                    }
                    else
                    {
                        login.Status = -1;
                        login.Msg = "In valid Username or password ";
                    }

                }
                else
                {
                    login.Status = -1;
                    login.Msg = "In valid Username or password ";
                }


            }

            return View(login);

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
