using AdmissionModel;

namespace AdmissionRepo
{
    public interface IStudentApplyCourseRepo:IGenericRepository<StudentAppliedCourse>
    {
        public Task<IEnumerable<StudentAppliedCourse>> GetAllAsyncByAppNo(string appno);
        Task<int> DeleteAsync(string id, int courseid);
        Task<int> updateCourseFeesPaymentStatus(StudentAppliedCourse entity);
        Task<string> genrateGrpFeesforPayment(string appno,int courseID,double amt);
        Task<int> updateCourseFeesGrnStatus(StudentAppliedCourse entity);
        Task<int> IsAnyCoursePaidByStd(string appno);
    }
}