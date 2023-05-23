using AdmissionModel;

namespace AdmissionRepo
{
    public interface IstudentApplyCollegeRepo:IGenericRepository<StudentAppliedColleges>
    {
        public Task<IEnumerable<StudentAppliedColleges>> GetAllStudentselectCollegeByCourseIdAsync(StudentAppliedColleges entity);

        public Task<int> AddStudentAppliedCollegesSubject(StudentAppliedCollegesSubject entity);

        public Task<StudentAppliedCollegesSubject> getStudentAppliedCollegesSubject(StudentAppliedCollegesSubject entity);
        public Task<IEnumerable<StudentSubjects>> getStudentChoosensSubject(string appno);

    }
}