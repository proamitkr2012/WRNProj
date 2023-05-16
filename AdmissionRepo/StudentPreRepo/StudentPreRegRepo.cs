using AdmissionData.Entities;
using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

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

        public Task<StudentMasters> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(StudentMasters entity)
        {
            throw new NotImplementedException();
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
                    param.Add("@PWD", studentMasters.Mobile);
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
    }
}