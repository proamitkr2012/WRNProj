using AdmissionModel.Entity;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AdmissionRepo
{
    public class StudentQualificationRepo : IStudentQualificationRepo
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<StudentQualificationRepo> _logger;

        public StudentQualificationRepo(IConnectionFactory connectionFactory, ILogger<StudentQualificationRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }




        public async  Task<int> AddAsync(StudentQualification entity)
        {

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertUpdateStudentQualification";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", entity.ApplicationNo);
                    param.Add("@DocID", entity.DocID);
                    param.Add("@ExamStatus", entity.ExamStatus);
                    param.Add("@Board_Universty_Name", entity.Board_Universty_Name);
                    param.Add("@Subject", entity.Subject);
                    param.Add("@PassingYear", entity.PassingYear);
                    param.Add("@PreRollNo", entity.PreRollNo);
                    param.Add("@MarkObt", entity.MarkObt );  
                    param.Add("@TotalMarks", entity.TotalMarks);  
                    param.Add("@Percentage", entity.Percentage); 
                    param.Add("@IsCGPA", entity.IsCGPA);
                    param.Add("@EnteryID", entity.EntryID);
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

        public Task<IEnumerable<StudentQualification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<StudentQualification>> GetAllByAppNoAsync(string appno)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "getstudentQualification";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", appno);
                    var list = await SqlMapper.QueryAsync<StudentQualification>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public Task<StudentQualification> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(StudentQualification entity)
        {
            throw new NotImplementedException();
        }
    }
}
