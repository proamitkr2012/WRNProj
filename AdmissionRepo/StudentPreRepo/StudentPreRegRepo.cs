﻿using AdmissionData.Entities;
using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace AdmissionRepo
{
    public class StudentPreRegRepo : IstudentPreRepo
    {

        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<StudentPreRegRepo> _logger;

        public StudentPreRegRepo(IConnectionFactory connectionFactory, ILogger<StudentPreRegRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }


        public async Task<int> AddAsync(StudentMasters entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertStudentPreResult";
                    var param = new DynamicParameters();
                    param.Add("@PreRegID_PK", entity.EntryID);
                    param.Add("@StudentName", entity.Name.Trim());
                    param.Add("@FatherName", entity.FatherName);
                    param.Add("@MobileNo", entity.Mobile);
                    param.Add("@EmailID", entity.Email);
                    param.Add("@CourseTypeID", entity.CourseTypeID);
                    param.Add("@IsOTPVerified", entity.IsVerified == true ? 1 : 0);
                    var rowsInserted = await SqlMapper.QuerySingleOrDefaultAsync<int>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentMasters>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task<StudentMasters> GetByIdAsync(string id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "select_newstudentmasterByAppNo";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo",id );
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<StudentMasters>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return null;
        }


        public async Task<StudentAllData> AppliedStudentDetailByID(string collegecode, string courseid, string appno)
        {

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var paramList = new
                    {

                        ccode = collegecode,
                        courseId = courseid,
                        ApplicationNo = appno

                    };
                
                    var data = await connection.QuerySingleOrDefaultAsync<StudentAllData>("Select_College_StudentByAppNo", paramList, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return data;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

            }

            return null;
        }





        public async Task<StudentMasters> GetByMobileNoAsync(string mobileNo)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "select_newstudentmasterByMobile";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", mobileNo);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<StudentMasters>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return null;
        }

        public async  Task<int> UpdateAsync(StudentMasters entity)
        {

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "UpdatenewstudetProfile";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", entity.ApplicationNo );
                    param.Add("@Name", entity.Name );
                    param.Add("@FatherName", entity.FatherName );
                    param.Add("@MotherName", entity.MotherName );
                    param.Add("@Email", entity.Email );
                    param.Add("@Mobile", entity.Mobile );
                    param.Add("@Aadhar", entity.Aadhar );
                    param.Add("@Gender", entity.Gender );
                    param.Add("@DOB", entity.DOB );
                    param.Add("@AltMobile",!string.IsNullOrEmpty(entity.AltMobile)? entity.AltMobile:"" );
                    param.Add("@Category", entity.Category );
                    param.Add("@SubCategory", entity.SubCategory );
                    param.Add("@CurrentAddress", entity.CurrentAddress );
                    param.Add("@CState", entity.CState );
                    param.Add("@CDistrict", entity.CDistrict );
                    param.Add("@CPin", entity.CPin );
                    param.Add("@ParmanentAddress", entity.ParmanentAddress );
                    param.Add("@PState", entity.PState );
                    param.Add("@PDistrict", entity.PDistrict );
                    param.Add("@PPin", entity.PPin );  
                    param.Add("@Domicile", entity.Domicile );
                    param.Add("@Religion", entity. Religion);
                    param.Add("@Nationalty", entity.Nationalty );
                    param.Add("@EWS", entity.Ews);
                    param.Add("@CreatedBy", entity.CreatedBy);
                    param.Add("@Roles", entity.Roles);
                    var rowsInserted = await SqlMapper.ExecuteAsync(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                   
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }

                return -1;
            }

          }

       public  async  Task<StudentPreRegistratiuon> GetByPreIdAsync(string id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectpreregStudentbyID";
                    var param = new DynamicParameters();
                    param.Add("@PreRegID", id);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<StudentPreRegistratiuon>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return null;
        }

        public async  Task<StudentPreRegistratiuon> GetByMobileAsync(string mobileNo)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectpreregStudentbyMobile";
                    var param = new DynamicParameters();
                    param.Add("@MObile", mobileNo);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<StudentPreRegistratiuon>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return null;
        }

        public async Task<int> IsMobileExistsAsync(string mobileNo)
        {
            int result = -1;
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "IsvalidateMobile";
                    var param = new DynamicParameters();
                    param.Add("@mobile", mobileNo);
                    result = await SqlMapper.QuerySingleOrDefaultAsync<int>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public async Task<sqlResponse> VerifyOTP_InsertStudentData(StudentMasters studentMasters)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "SaveNewStudentMaster";
                    var param = new DynamicParameters();
                    param.Add("@PreRegID", studentMasters.EntryID);
                    param.Add("@Roll", studentMasters.Roll);
                    param.Add("@Name", studentMasters.Name);
                    param.Add("@FatherName", studentMasters.FatherName);
                    param.Add("@Email", studentMasters.Email);
                    param.Add("@Mobile", studentMasters.Mobile);
                    param.Add("@CourseTypeID", studentMasters.CourseTypeID);
                    param.Add("@isnewadm", studentMasters.IsNewadm);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<sqlResponse>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return null;
        }

        public async  Task<int> ChangePassword(StudentMasters studentMasters)
        {
           int result = -1;
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "ChangePassword";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", studentMasters.ApplicationNo);
                    param.Add("@PWD", studentMasters.PWD);
                    result = await SqlMapper.ExecuteAsync(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public async  Task<StudentMasters> CheckAuthuntication(StudentMasters studentMasters)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "IsnewstudentisValid";
                    var param = new DynamicParameters();
                    param.Add("@USERNAME", studentMasters.ApplicationNo);
                    param.Add("@PWD", studentMasters.PWD);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<StudentMasters>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return null;
        }

        public async Task<int> IsAadharExists(string aadhar)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "IsAadharExists";
                    var param = new DynamicParameters();
                    param.Add("@aadhar", aadhar);
                    var rowsInserted = await SqlMapper.QuerySingleOrDefaultAsync<int>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }

        public async  Task<int> UploadDocData(string appno, string path)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "UploadPhoto";
                    var param = new DynamicParameters();
                    param.Add("@Applicationno", appno);
                    param.Add("@Std_PHOTO", path);
                    var rowsInserted = await SqlMapper.ExecuteAsync(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }

        /*---------------------------------Admission -------------------------------*/

        public async Task<int> IsStudentAdmitted(string applicationNo)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "IsStudentAdmitted";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", applicationNo);
                    var rowsInserted = await SqlMapper.QuerySingleOrDefaultAsync<int>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }



        }


        public async Task<AdmissionPayment> getAdmissionRefrenceNoForPay(string applicationNo,string ccode,int courseId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "CreateReferenceforAdmissionPay";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", applicationNo);
                    param.Add("@ccode", ccode);
                    param.Add("@CourseID", courseId);
                    var rowsInserted = await SqlMapper.QuerySingleOrDefaultAsync<AdmissionPayment>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return null;
            }



        }



        public async Task<AdmissionPayment> getAdmissionfeesPaymentDetails(string applicationNo, string ccode, int courseId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "getRefrenceAdmissionPay";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", applicationNo);
                    param.Add("@ccode", ccode);
                    param.Add("@CourseID", courseId);
                    var rowsInserted = await SqlMapper.QuerySingleOrDefaultAsync<AdmissionPayment>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return null;
            }



        }




        public async Task<int> GetcollegeCourseAvilabeSeat(string code,int courseId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "IsSeatAvilable";
                    var param = new DynamicParameters();
                    param.Add("@CCode", code);
                    param.Add("@CourseID", courseId);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<int>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return 0;
        }



        public async Task<int> candidatCancelAdmissionSeat(string appno,string code, int courseId,string createdby,string remarks)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "CancelCandidatePreAdmission";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", appno);
                    param.Add("@CourseId", courseId);
                    param.Add("@Ccode", code);
                    param.Add("@CreatedBy", createdby);
                    param.Add("@Remarks", remarks);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<int>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return 0;
        }

        public async Task<int> cancelForm(string id,int Isconfirm)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "Cancel_WRN_Registration";
                    var param = new DynamicParameters();
                    param.Add("@Appno", id);
                    param.Add("@Isconfirm", Isconfirm);
                    var list = await SqlMapper.ExecuteAsync(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return 0;
        }


    }
}

