using AdmissionModel.Entity;

namespace AdmissionRepo 
{
    public interface IStudentQualificationRepo:IGenericRepository<StudentQualification>
    {
        public Task<IEnumerable<StudentQualification>> GetAllByAppNoAsync(string appno);
        
    }
}