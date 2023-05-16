using AdmissionData;
using AdmissionData.Dapper;
using AdmissionData.Entities;
using AdmissionModel;
using AdmissionModel.Enum;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdmissionModel.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AdmissionData.Dapper.Models;
using System.Security.Policy;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace AdmissionRepo
{
    public class AdminRepository : Repository<USR_INF>, IAdminRepository
    {
        public string _baseUrl = "";
        public string ImgCloudPath = "";
        IDapperContext _dapperContext;
        public DataContext _dbContext
        {
            get
            {
                return db as DataContext;
            }
        }

        public AdminRepository(DataContext _db, IDapperContext dapperContext) : base(_db)
        {
            //_baseUrl = WebConfigSetting.BaseURL;
            //  ImgCloudPath = WebConfigSetting.ImgCloudPath;
            _dapperContext = dapperContext;
        }

        //admin-login
        public StudentMastersDTO CheckStudentPreData(LoginDTO data)
        {
            try
            {
                StudentMastersDTO n = new StudentMastersDTO();
                var model = _dbContext.StudentPreData.Where(x => x.Roll.ToLower().Trim() == data.Enrollment.ToLower().Trim() && x.DOB.Value.Date == data.DobDATE.Value.Date).FirstOrDefault();
                if (model != null)
                {

                    n.Roll = model.Roll;
                    n.Name = model.Name;
                    n.FatherName = model.FatherName;
                    n.MotherName = model.MotherName;
                    n.Email = model.Email;
                    n.Mobile = model.Mobile;
                    n.Gender = model.Gender;
                    n.DOB = model.DOB;
                    n.CollegeCode = model.CollegeCode;
                    // n.CollegeName = model.CollegeName;
                    return n;

                }
            }
            catch (Exception ex) { }
            return null;
        }

        public StudentMastersDTO GetStudentMasterData(string Enrollment)
        {
            try
            {
                StudentMastersDTO n = new StudentMastersDTO();
                var model = _dbContext.StudentMasters.Where(x => x.Roll.ToLower().Trim() == Enrollment.ToLower().Trim()).FirstOrDefault();

                if (model != null)
                {

                    n.Roll = model.Roll;
                    n.Name = model.Name;
                    n.FatherName = model.FatherName;
                    n.MotherName = model.MotherName;
                    n.Email = model.Email;
                    n.Mobile = model.Mobile;
                    n.Gender = model.Gender;
                    n.DOB = model.DOB;
                    n.CollegeCode = model.CollegeCode;
                    n.CollegeName = _dbContext.CollegeMasters.Where(x => x.InstCode.ToLower().Trim() == model.CollegeCode.ToLower().Trim()).Select(x => x.Cname).FirstOrDefault();
                    return n;

                }
                else
                {
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public StudentMastersDTO GetStudentPreData(string Enrollment)
        {
            try
            {
                StudentMastersDTO n = new StudentMastersDTO();
                var model = _dbContext.StudentMasters.Where(x => x.Roll.ToLower().Trim() == Enrollment.ToLower().Trim()).FirstOrDefault();

                if (model != null)
                {

                    n.Roll = model.Roll;
                    n.Name = model.Name;
                    n.FatherName = model.FatherName;
                    n.MotherName = model.MotherName;
                    n.Email = model.Email;
                    n.Mobile = model.Mobile;
                    n.Gender = model.Gender;
                    n.DOB = model.DOB;
                    n.CollegeCode = model.CollegeCode;
                    n.CollegeName = _dbContext.CollegeMasters.Where(x => x.InstCode.ToLower().Trim() == model.CollegeCode.ToLower().Trim()).Select(x => x.Cname).FirstOrDefault();
                    return n;

                }
                else
                {
                    var model1 = _dbContext.StudentPreData.Where(x => x.Roll.ToLower().Trim() == Enrollment.ToLower().Trim()).FirstOrDefault();

                    if (model1 != null)
                    {

                        n.Roll = model1.Roll;
                        n.Name = model1.Name;
                        n.FatherName = model1.FatherName;
                        n.MotherName = model1.MotherName;
                        n.Email = model1.Email;
                        n.Mobile = model1.Mobile;
                        n.Gender = model1.Gender;
                        n.DOB = model1.DOB;
                        n.CollegeCode = model1.CollegeCode;
                        var c = _dbContext.CollegeMasters.Where(x => x.Code.ToLower().Trim() == model1.CollegeCode.ToLower().Trim()).Select(x => x).FirstOrDefault();
                        n.CollegeName = c.Cname;
                        n.CollegeCode = c.InstCode;
                        return n;

                    }
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public StudentMastersDTO CheckBedStudentLogin(LoginDTO model)
        {
            try
            {
                StudentMastersDTO data = new StudentMastersDTO();
                var admin = _dbContext.StudentMasters.Where(x => x.Roll.ToLower().Trim() == model.Enrollment.ToLower().Trim() && x.DOB.Value.Date == model.DobDATE.Value.Date).FirstOrDefault();
                if (admin != null)
                {

                    data.Roll = admin.Roll;
                    data.Name = admin.Name;

                    return data;

                }
            }
            catch (Exception ex) { }
            return null;
        }

        public StudentMastersDTO CheckBedStudentDATA(string Enrollment)
        {
            try
            {
                // StudentPreDataDTO model = new StudentPreDataDTO();
                // var admin = _dbContext.StudentMasters.Where(x => x.Roll.ToLower().Trim() == Enrollment.ToLower().Trim()).FirstOrDefault();
                StudentMastersDTO model = new StudentMastersDTO();
                string sqlComand = "select * from StudentMasters where Roll='" + Enrollment.Trim() + "'";

                StudentMastersDTO stud = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;
                if (stud != null)
                {
                    var c = _dbContext.CollegeMasters.Where(x => x.InstCode.ToLower().Trim() == stud.CollegeCode.ToLower().Trim()).Select(x => x).FirstOrDefault();
                    stud.CollegeName = c.Cname;
                    stud.CollegeCode = c.InstCode;
                    // stud.CollegeName = _dbContext.CollegeMasters.Where(x => x.InstCode.ToLower().Trim() == stud.CollegeCode.ToLower().Trim()).Select(x => x.Cname).FirstOrDefault();
                    return stud;

                }
                else
                {
                    string sqlComand1 = "select * from StudentPreData where Roll='" + Enrollment.Trim() + "'";

                    StudentMastersDTO stud1 = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand1).Result;
                    if (stud1 != null)
                    {
                        var c = _dbContext.CollegeMasters.Where(x => x.Code.ToLower().Trim() == stud1.CollegeCode.ToLower().Trim()).Select(x => x).FirstOrDefault();
                        stud1.CollegeName = c.Cname;
                        stud1.CollegeCode = c.InstCode;
                        return stud1;

                    }
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<tblState> GetStateList()
        {
            try
            {
                List<tblState> Stateslist = _dbContext.tblState.ToList();
                return Stateslist;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public List<tblDistrict> GetDistrictList(int ID)
        {
            try
            {
                List<tblDistrict> c = _dbContext.tblDistrict.Where(x => x.StateId == ID).Select(x => x).OrderBy(c => c.Name).ToList();
                foreach (var item in c)
                {
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    item.Name = textInfo.ToTitleCase(item.Name);

                }
                return c;
            }
            catch (Exception e)
            {
            }
            return null;
        }
        public async Task<int> AddRegistration(StudentMastersDTO model)
        {
            try
            {
                //StudentMastersDTO check = new StudentMastersDTO();
                //string sqlComand = "select * from StudentMasters where Roll='" + Enrollment.Trim() + "'";

                //StudentMastersDTO stud = _dapperContext.QueryHelper.QueryFirstOrDefaultAsync<StudentMastersDTO>(sqlComand).Result;
                model.Roll = AESEncription.Base64Decode(model.EncrptedRoll);
                var stud = await _dbContext.StudentMasters.Where(x => x.Roll.ToLower().Trim() == model.Roll.ToLower().Trim()).FirstOrDefaultAsync();
                var checemail = await _dbContext.StudentMasters.Where(x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim() && x.Roll.ToLower().Trim() != model.Roll.ToLower().Trim()).FirstOrDefaultAsync();
                if (checemail != null)
                {
                    return 1;
                }
                var checmobile = await _dbContext.StudentMasters.Where(x => x.Mobile.ToLower().Trim() == model.Mobile.ToLower().Trim() && x.Roll.ToLower().Trim() != model.Roll.ToLower().Trim()).FirstOrDefaultAsync();
                if (checmobile != null)
                {
                    return 2;
                }
                var preda = await _dbContext.StudentPreData.Where(x => x.Roll.ToLower().Trim() == model.Roll.ToLower().Trim()).FirstOrDefaultAsync();

                var c = await _dbContext.CollegeMasters.Where(x => x.Code.ToLower().Trim() == preda.CollegeCode.ToLower().Trim()).FirstOrDefaultAsync();
                if (c == null)
                {

                    return 3;
                }
                if (stud == null)
                {
                    StudentMasters n = new StudentMasters();
                    n.Roll = model.Roll;
                    n.Name = preda.Name;
                    n.FatherName = preda.FatherName;
                    n.MotherName = preda.MotherName;
                    n.Email = model.Email;
                    n.Mobile = model.Mobile;
                    n.Aadhar = model.Aadhar;
                    n.Gender = preda.Gender;
                    n.DOB = preda.DOB;
                    n.AltMobile = model.AltMobile;

                    n.Category = model.Category;
                    n.CollegeCode = c.InstCode;
                    //n.CollegeName = preda.CollegeName;
                    n.CreatedDate = DateTime.Now;

                    n.CurrentAddress = model.CurrentAddress;
                    n.CState = model.CState;
                    n.CDistrict = model.CDistrict;
                    n.CPin = model.CPin;
                    n.ParmanentAddress = model.ParmanentAddress;
                    n.PState = model.PState;
                    n.PDistrict = model.PDistrict;
                    n.PPin = model.PPin;
                    n.Ews = model.Ews;
                    //  _dbContext.StudentMasters.Add(n);
                    //await _dbContext.SaveChangesAsync();

                    var parameters = new
                    {

                        @Roll = n.Roll,
                        @Name = n.Name,
                        @FatherName = n.FatherName,
                        @MotherName = n.MotherName,
                        @Email = n.Email,
                        @Mobile = n.Mobile,
                        @Aadhar = n.Aadhar,
                        @Gender = n.Gender,
                        @DOB = n.DOB,
                        @AltMobile = n.AltMobile,
                        @Category = n.Category,
                        @CollegeCode = n.CollegeCode,

                        @CreatedDate = n.CreatedDate,
                        @CurrentAddress = n.CurrentAddress,
                        @CState = n.CState,
                        @CDistrict = n.CDistrict,
                        @CPin = n.CPin,
                        @ParmanentAddress = n.ParmanentAddress,
                        @PState = n.PState,
                        @PDistrict = n.PDistrict,
                        @PPin = n.PPin,
                        @Ews = n.Ews,



                    };
                    var response = _dapperContext.ProcedureHelper.QueryAsync<string>("SaveStudentMaster", parameters).Result;


                    QualificationAutoFeed(model.DegreeID, n.Roll);
                    DocAutoFeed(model.DegreeID, n.Roll);

                    return 3;

                }
                else
                {

                    //stud.Roll = model.Roll;
                    //stud.Name = preda.Name;
                    // stud.FatherName = model.FatherName;
                    // stud.MotherName = model.MotherName;
                    stud.Email = model.Email;
                    stud.Mobile = model.Mobile;
                    stud.Aadhar = model.Aadhar;
                    //stud.Gender = model.Gender;
                    //stud.DOB = model.DOB;
                    stud.AltMobile = model.AltMobile;
                    stud.Category = model.Category;
                    //stud.CollegeCode = model.CollegeCode;
                    // stud.CollegeName = preda.CollegeName;
                    stud.CreatedDate = DateTime.Now;
                    stud.CurrentAddress = model.CurrentAddress;
                    stud.CState = model.CState;
                    stud.CDistrict = model.CDistrict;
                    stud.CPin = model.CPin;
                    stud.ParmanentAddress = model.ParmanentAddress;
                    stud.PState = model.PState;
                    stud.PDistrict = model.PDistrict;
                    stud.PPin = model.PPin; stud.Ews = model.Ews;

                    await _dbContext.SaveChangesAsync();

                    QualificationAutoFeed(model.DegreeID, stud.Roll);
                    DocAutoFeed(model.DegreeID, stud.Roll);
                    return 3;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return 0;
        }

        public List<DocSetting> GetDocRequiredList(int DegreeID)
        {
            try
            {
                var preda = _dbContext.DocSetting.Where(x => x.DegreeID == DegreeID).ToList();
                return preda;

            }
            catch (Exception r)
            {

            }
            return null;
        }
        public List<StudentDocUploaded> GetDocUoloadList(string Roll)
        {
            try
            {
                List<StudentDocUploaded> model = new List<StudentDocUploaded>();
                model = (from q in _dbContext.StudentDocUploaded
                         join d in _dbContext.DocSetting on q.DocID equals d.EntryID

                         where q.Roll == Roll
                         select new StudentDocUploaded
                         {
                             EntryID = q.EntryID,
                             Roll = Roll,
                             DocID = q.DocID,
                             DocName = d.DocName,
                             DocDisplayName = d.DocDisplayName,
                             FilePath = q.FilePath,
                             VirtualFilePath = q.VirtualFilePath,
                             IsActive = q.IsActive,
                             CreateDate = q.CreateDate,
                         }).ToList();
                return model;

            }
            catch (Exception r)
            {

            }
            return null;
        }
        public bool QualificationAutoFeed(int DegreeID, string Roll)
        {
            try
            {
                var totdocreq = _dbContext.DocSetting.Where(x => x.DegreeID == DegreeID && x.IsOnlyUpload == false && x.IsActive == true).ToList();


                foreach (var item in totdocreq)
                {
                    var check = _dbContext.QualifationMasters.Where(x => x.DocID == item.EntryID && x.Roll == Roll).FirstOrDefault();
                    if (check == null)
                    {
                        QualifationMasters q = new QualifationMasters();
                        q.DocID = item.EntryID;
                        q.Roll = Roll;
                        q.PreRollNo = "";
                        q.PassingYear = null;
                        _dbContext.QualifationMasters.Add(q);
                        _dbContext.SaveChanges();
                    }

                }
                return true;

            }
            catch (Exception r)
            {

            }
            return false;
        }
        public bool DocAutoFeed(int DegreeID, string Roll)
        {
            try
            {
                var totdocreq = _dbContext.DocSetting.Where(x => x.DegreeID == DegreeID && x.IsActive == true).ToList();


                foreach (var item in totdocreq)
                {
                    var check = _dbContext.StudentDocUploaded.Where(x => x.DocID == item.EntryID && x.Roll == Roll).FirstOrDefault();
                    if (check == null)
                    {
                        StudentDocUploaded q = new StudentDocUploaded();
                        q.DocID = item.EntryID;
                        q.Roll = Roll;
                        _dbContext.StudentDocUploaded.Add(q);
                        _dbContext.SaveChanges();
                    }

                }
                return true;

            }
            catch (Exception r)
            {

            }
            return false;
        }
        public List<QualifationMasters> GetQualificationList(string Roll)
        {
            try
            {
                List<QualifationMasters> model = new List<QualifationMasters>();
                model = (from q in _dbContext.QualifationMasters
                         join d in _dbContext.DocSetting on q.DocID equals d.EntryID

                         where q.Roll == Roll
                         select new QualifationMasters
                         {
                             EntryID = q.EntryID,
                             Roll = Roll,
                             DocID = q.DocID,
                             ExamStatus = q.ExamStatus,
                             Board_Universty_Name = q.Board_Universty_Name,
                             Subject = q.Subject,
                             PassingYear = q.PassingYear,
                             PreRollNo = q.PreRollNo,
                             MarkObt = q.MarkObt,
                             TotalMarks = q.TotalMarks,
                             Percentage = q.Percentage,
                             IsCGPA = q.IsCGPA,
                             DocName = d.DocName,
                             DocDisplayName = d.DocDisplayName
                         }).ToList();
                return model;
            }
            catch (Exception r)
            {

            }
            return null;
        }
        public async Task<bool> UpdateQualification(QualifationMasters model)
        {
            try
            {
                QualifationMasters q = _dbContext.QualifationMasters.Where(x => x.EntryID == model.EntryID).FirstOrDefault();


                if (q != null)
                {

                    q.ExamStatus = model.ExamStatus;
                    q.Board_Universty_Name = model.Board_Universty_Name;
                    q.MarkObt = model.MarkObt;
                    q.TotalMarks = model.TotalMarks;
                    if (model.IsCGPA == true)
                    {
                        q.Percentage = model.Percentage;
                    }
                    else
                    {
                        q.Percentage = (int)Math.Round((double)(100 * (int)model.MarkObt) / (int)model.TotalMarks);
                    }
                    q.IsCGPA = model.IsCGPA;
                    q.Subject = model.Subject;

                    q.PreRollNo = model.PreRollNo;
                    q.PassingYear = model.PassingYear;

                    _dbContext.SaveChanges();
                    return true;

                }

            }
            catch (Exception e)
            {

            }
            return false;
        }
        public bool UploadDocData(StudentDocUploaded data)
        {
            try
            {
                StudentDocUploaded q = _dbContext.StudentDocUploaded.Where(x => x.EntryID == data.EntryID).FirstOrDefault();
                if (q != null)
                {

                    q.FilePath = data.FilePath;
                    q.VirtualFilePath = data.VirtualFilePath;
                    q.CreateDate = DateTime.Now;
                    _dbContext.SaveChanges();
                    return true;

                }

            }
            catch (Exception e)
            {

            }
            return false;
        }
        public List<CollegeMasters> GetCollegeList()
        {
            try
            {
                List<CollegeMasters> CollegeList = _dbContext.CollegeMasters.Where(x => x.IsActive == true && !string.IsNullOrEmpty(x.InstCode)).ToList();
                return CollegeList;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}
