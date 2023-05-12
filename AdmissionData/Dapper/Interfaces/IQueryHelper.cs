
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdmissionData.Dapper.Interfaces
{
    public interface IQueryHelper
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}
