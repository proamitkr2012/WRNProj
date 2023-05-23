using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace AdmissionRepo 
{
    public class MasterRepo : IMasterRepo
    {

        IConnectionFactory _connectionFactory;
        private readonly ILogger<MasterRepo> _logger;
        
        public MasterRepo(IConnectionFactory connectionFactory, ILogger<MasterRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        
        public async  Task<IEnumerable<StudentCategory>> GetCategory()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selecgtAllstudentCategory";
                    var param = new DynamicParameters();
                    //param.Add("@COUNTRYID_FK", countryid);
                    var list = await SqlMapper.QueryAsync<StudentCategory>(connection, query, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async Task <IEnumerable<StudentSubcategory>> GetSubCategory(int cateid)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "getAllstudentsubcategoryByCatId";
                    var param = new DynamicParameters();
                    param.Add("@cateid", cateid);
                    var list = await SqlMapper.QueryAsync<StudentSubcategory>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;

        }

        public async  Task<IEnumerable<CourseType>> GetCourseType()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectAllCourseType";
                    var param = new DynamicParameters();
                    var list = await SqlMapper.QueryAsync<CourseType>(connection, query, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async  Task<IEnumerable<EducationalBoard>> GetEducationalBoard()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "getallEducationalBoard";
                    var param = new DynamicParameters();
                    //param.Add("@COUNTRYID_FK", countryid);
                    var list = await SqlMapper.QueryAsync<EducationalBoard>(connection, query, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async  Task<IEnumerable<Universty>> GetAlllUniversity()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "SelectAllUniversity";
                    var param = new DynamicParameters();
                    //param.Add("@COUNTRYID_FK", countryid);
                    var list = await SqlMapper.QueryAsync<Universty>(connection, query, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async  Task<int> SaveUniversity(Universty entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertUniversities";
                    var param = new DynamicParameters();
                    param.Add("@University", entity.University);
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

        public async  Task<int> SaveEducationalBoard(EducationalBoard entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertUpdateEducationalBoards";
                    var param = new DynamicParameters();
                    param.Add("@Board", entity.Board);
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

        public async  Task<IEnumerable<Course>> GetAllCoursebyCourseType(int coursetypeId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectCourseByCourseType";
                    var param = new DynamicParameters();
                    param.Add("@CourseTypeId", coursetypeId);
                    var list = await SqlMapper.QueryAsync<Course>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async  Task<IEnumerable<Colleges_Course>> GetAllCourseWiseColleges(int CourseID)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectCourseWiseColleges";
                    var param = new DynamicParameters();
                    param.Add("@CourseID", CourseID);
                    var list = await SqlMapper.QueryAsync<Colleges_Course>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

       
        public async Task<IEnumerable<Subjects>> GetAllCollegeCOSubjects(string cCode, int courseId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectAllCosubject";
                    var param = new DynamicParameters();
                    param.Add("@Ccode", cCode);
                    param.Add("@CourseId", courseId);
                    var list = await SqlMapper.QueryAsync<Subjects>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async  Task<IEnumerable<Subjects>> GetAllCollegeSkillSubjects(string cCode, int courseId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectAllSkillsubject";
                    var param = new DynamicParameters();
                    param.Add("@Ccode", cCode);
                    param.Add("@CourseId", courseId);
                    var list = await SqlMapper.QueryAsync<Subjects>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

        public async Task<IEnumerable<Subjects>> GetAllCollegeMajorSubjects(string cCode, int courseId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "selectAllMainsubject";
                    var param = new DynamicParameters();
                    param.Add("@Ccode", cCode);
                    param.Add("@CourseId", courseId);
                    var list = await SqlMapper.QueryAsync<Subjects>(connection, query, param, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return list.ToList();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    _logger.LogError(ex.Message);
                }
            }
            return null;
        }

         
    }
}
