
using AdmissionData.Dapper.Interfaces;

namespace AdmissionData.Dapper
{
    public interface IDapperContext
    {
        IQueryHelper QueryHelper { get; }
        IProcedureHelper ProcedureHelper { get; }
    }
}
