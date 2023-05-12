
using System.Data;

namespace AdmissionData.Dapper.Interfaces
{
    public interface ISqlConnectionHelper
    {
        IDbConnection GetDbConnection();
    }
}
