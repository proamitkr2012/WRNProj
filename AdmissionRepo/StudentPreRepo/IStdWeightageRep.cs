using AdmissionModel;

namespace AdmissionRepo
{
    public interface IStdWeightageRep:IGenericRepository<StudentWeightage>
    {
        Task<IEnumerable<StudentWeightage>>GetStudentWeightagesAsync(string applicationno);
    }
}