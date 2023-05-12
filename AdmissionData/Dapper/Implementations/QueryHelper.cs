
using Dapper;
using AdmissionData.Dapper.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdmissionData.Dapper.Implementations
{
    public class QueryHelper : IQueryHelper
    {
        private readonly ISqlConnectionHelper _sqlConnectionProvider;

        public QueryHelper(ISqlConnectionHelper sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }
    }
}
