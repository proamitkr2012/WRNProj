using AdmissionData.Entities;
using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AdmissionRepo
{
    public class AdminDashBoardRepo : IAdminDashBoardRepo
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<AdminDashBoardRepo> _logger;

        public AdminDashBoardRepo(IConnectionFactory connectionFactory, ILogger<AdminDashBoardRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async  Task<DashBoardEntityCount> BedRegisterationDataCountReport()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "Dash_BEDCount";
                    var param = new DynamicParameters();
                    // param.Add("@ApplicationNo", mobileNo);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<DashBoardEntityCount>(connection, query, commandType: CommandType.StoredProcedure);
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

        public async Task<DashBoardEntityCount>RegisterationDataCountReport()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "Dash_TotalRecords";
                    var param = new DynamicParameters();
                   // param.Add("@ApplicationNo", mobileNo);
                    var list = await SqlMapper.QuerySingleOrDefaultAsync<DashBoardEntityCount>(connection, query,commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<StudentAllData>> RegisteredStudentList(SearchStudent searchStudent)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectAllNewRegisteredStudents";
                    var param = new DynamicParameters();
                    param.Add("@CourseTypeID", searchStudent.CourseTypeId);
                    param.Add("@CourseID", searchStudent.CourseID);
                    param.Add("@SearchText", searchStudent.SearchText);
                    param.Add("@PgIndex", searchStudent.PgIndex);
                    param.Add("@PgSize", searchStudent.PgSize);
                    var list = await SqlMapper.QueryAsync<StudentAllData>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

		public async Task<IEnumerable<StudentAllData>> Student_Course_Fees_List(SearchStudent searchStudent)
		{
			using (IDbConnection connection = _connectionFactory.GetConnection)
			{

				try
				{
					var query = "selectNewStudents_Courses_FeesDetails";
					var param = new DynamicParameters();
					param.Add("@CourseTypeID", searchStudent.CourseTypeId);
					param.Add("@CourseID", searchStudent.CourseID);
					param.Add("@SearchText", searchStudent.SearchText);
					param.Add("@PgIndex", searchStudent.PgIndex);
					param.Add("@PgSize", searchStudent.PgSize);
					var list = await SqlMapper.QueryAsync<StudentAllData>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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

		public async Task<IEnumerable<StudentAllData>> TotalStudentList(SearchStudent searchStudent  )
		{
			using (IDbConnection connection = _connectionFactory.GetConnection)
			{

				try
				{
                    var query = "selectAllNewStudents";
					var param = new DynamicParameters();
					param.Add("@CourseTypeID", searchStudent.CourseTypeId);
					param.Add("@SearchText", searchStudent.SearchText);
					param.Add("@PgIndex", searchStudent.PgIndex);
					param.Add("@PgSize", searchStudent.PgSize);
					var list = await SqlMapper.QueryAsync<StudentAllData>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
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
	}
}
