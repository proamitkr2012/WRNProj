using AdmissionModel;

namespace AdmissionRepo
{
    public interface IStudentApplyCourseRepo:IGenericRepository<StudentAppliedCourse>
    {
        public Task<IEnumerable<StudentAppliedCourse>> GetAllAsyncByAppNo(string appno);
    }
}