using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;
using static Dapper.SqlMapper;

namespace AdmissionRepo
{
    public class studentApplyCollegeRepo : IstudentApplyCollegeRepo
    {

        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<studentApplyCollegeRepo> _logger;

        public studentApplyCollegeRepo(IConnectionFactory connectionFactory, ILogger<studentApplyCollegeRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }


        public async  Task<int> AddAsync(StudentAppliedColleges entity)
        {
            

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertUpdateStudentCollege";
                    var param = new DynamicParameters();
                    param.Add("@CCode", entity.CCode);
                    param.Add("@CourseId", entity.CourseId);
                    param.Add("@ApplicationNo", entity.ApplicationNo);
                    param.Add("@IsSelect", entity.IsSelect);
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

        public async Task<int> AddStudentAppliedCollegesSubject(StudentAppliedCollegesSubject entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "insertUpdateStudentSubject";
                    var param = new DynamicParameters();
                    param.Add("@CCode", entity.CCode);
                    param.Add("@CourseId", entity.CourseId);
                    param.Add("@ApplicationNo", entity.ApplicationNo);
                    param.Add("@MajorSubjectID", entity.MajorSubjectID);
                    param.Add("@CoSubjectID", entity.CoSubjectID);
                    param.Add("@SkillSubjectID", entity.SkillSubjectID);
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
        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentAppliedColleges>> GetAllAsync()
        {
            throw new NotImplementedException();
        }




        public async  Task<IEnumerable<StudentAppliedColleges>> GetAllStudentselectCollegeByCourseIdAsync(StudentAppliedColleges entity)
        {
           
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectStudenCourseWiseColleges";
                    var param = new DynamicParameters();
                    param.Add("@CourseID", entity.CourseId);
                    param.Add("@ApplicationNo", entity.ApplicationNo);
                    var list = await SqlMapper.QueryAsync<StudentAppliedColleges>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

                return null;
            }
        }

        public Task<StudentAppliedColleges> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentAppliedCollegesSubject> getStudentAppliedCollegesSubject(StudentAppliedCollegesSubject entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectStudentSubjets";
                    var param = new DynamicParameters();
                    param.Add("@CourseID", entity.CourseId);
                    param.Add("@ApplicationNo", entity.ApplicationNo);
                    param.Add("@CCode", entity.CCode);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<StudentAppliedCollegesSubject>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

                return null;
            }
        }

        public async  Task<IEnumerable<StudentSubjects>> getStudentChoosensSubject(string appno)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectStudentSubjetsDetails";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", appno);
                    var list = await SqlMapper.QueryAsync<StudentSubjects>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

                return null;
            }
        }

        public Task<int> UpdateAsync(StudentAppliedColleges entity)
        {
            throw new NotImplementedException();
        }
    }
}
