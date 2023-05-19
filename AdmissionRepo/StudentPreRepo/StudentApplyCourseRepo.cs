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
using static System.Net.Mime.MediaTypeNames;

namespace AdmissionRepo 
{
    public class StudentApplyCourseRepo : IStudentApplyCourseRepo
    {


        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<StudentApplyCourseRepo> _logger;

        public StudentApplyCourseRepo(IConnectionFactory connectionFactory, ILogger<StudentApplyCourseRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async  Task<int> AddAsync(StudentAppliedCourse entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertUniversities";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", entity.ApllicationNo);
                    param.Add("@CourseId", entity.CourseId);
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


        public async  Task<IEnumerable<StudentAppliedCourse>> GetAllAsyncByAppNo(string appno)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectStudentAppliedCourse";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", appno);
                    var list = await SqlMapper.QueryAsync<StudentAppliedCourse>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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









        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentAppliedCourse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       
        public Task<StudentAppliedCourse> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(StudentAppliedCourse entity)
        {
            throw new NotImplementedException();
        }
    }
}
