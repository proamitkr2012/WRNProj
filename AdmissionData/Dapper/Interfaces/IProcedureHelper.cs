
using AdmissionData.Dapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdmissionData.Dapper.Interfaces
{
    public interface IProcedureHelper
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Task<dynamic> QueryMultipleAsync(string storedProcedure, IEnumerable<MapItem> mapItems = null, object parameters = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Task<int> ExecuteCommandAsync(string storedProcedure, object parameters);
    }
}
