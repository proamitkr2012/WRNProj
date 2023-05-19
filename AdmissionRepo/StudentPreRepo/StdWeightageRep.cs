using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AdmissionRepo
{

    public class StdWeightageRep : IStdWeightageRep
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<StdWeightageRep> _logger;

        public StdWeightageRep(IConnectionFactory connectionFactory, ILogger<StdWeightageRep> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async  Task<int> AddAsync(StudentWeightage entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertStudentWeightage";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", entity.ApplicationNo);
                    param.Add("@ID", entity.ID);
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

        public async  Task<int> DeleteAsync(string id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "DeleteStudentWeightagesByAppno";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", id);
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

        public Task<IEnumerable<StudentWeightage>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentWeightage> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<StudentWeightage>> GetStudentWeightagesAsync(string applicationno)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectStudentWeightage";
                    var param = new DynamicParameters();
                    param.Add("@ApplicationNo", applicationno);
                    var list = await SqlMapper.QueryAsync<StudentWeightage>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public Task<int> UpdateAsync(StudentWeightage entity)
        {
            throw new NotImplementedException();
        }
    }
}
