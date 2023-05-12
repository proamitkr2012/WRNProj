using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using AdmissionData.Dapper.Interfaces;
using AdmissionData.Dapper.Models;

namespace AdmissionData.Dapper.Implementations
{
    public class ProcedureHelper : IProcedureHelper
    {
        public ProcedureHelper(ISqlConnectionHelper sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }
        private readonly ISqlConnectionHelper _sqlConnectionProvider;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<dynamic> QueryMultipleAsync(string storedProcedure, IEnumerable<MapItem> mapItems = null, object parameters = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            var data = new ExpandoObject();
            if (mapItems == null) return data;

            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                if (parameters != null)
                {
                    var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    foreach (var item in mapItems)
                    {
                        if (item.DataRetriveType == EnumDataRetriveType.FirstOrDefault)
                        {
                            var singleItem = multi.Read(item.Type).FirstOrDefault();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, singleItem);
                        }
                        else if (item.DataRetriveType == EnumDataRetriveType.List)
                        {
                            var listItem = multi.Read(item.Type).ToList();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, listItem);
                        }
                    }
                    return data;
                }
                else
                {
                    var multi = await connection.QueryMultipleAsync(storedProcedure, commandType: CommandType.StoredProcedure);
                    foreach (var item in mapItems)
                    {
                        if (item.DataRetriveType == EnumDataRetriveType.FirstOrDefault)
                        {
                            var singleItem = multi.Read(item.Type).FirstOrDefault();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, singleItem);
                        }
                        else if (item.DataRetriveType == EnumDataRetriveType.List)
                        {
                            var listItem = multi.Read(item.Type).ToList();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, listItem);
                        }
                    }
                    return data;
                }
            }
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                if (parameters != null)
                {
                    return await connection.QueryAsync<T>(storedProcedure, parameters,
                          commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return await connection.QueryAsync<T>(storedProcedure,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                if (parameters != null)
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters,
                          commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<int> ExecuteCommandAsync(string storedProcedure, object parameters = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                return await connection.ExecuteAsync(storedProcedure, parameters,
                      commandType: CommandType.StoredProcedure);
            }
        }
    }
}