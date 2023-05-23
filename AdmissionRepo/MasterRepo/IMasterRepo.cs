using AdmissionModel;

namespace AdmissionRepo
{
    public interface IMasterRepo
    {
        public Task<IEnumerable<StudentCategory>> GetCategory();
        public Task<IEnumerable<StudentSubcategory>> GetSubCategory(int cateid);
        public Task<IEnumerable<CourseType>> GetCourseType();
        public Task<IEnumerable<EducationalBoard>> GetEducationalBoard();
        Task<IEnumerable<Universty>> GetAlllUniversity();
        Task<int> SaveUniversity(Universty universty);
        Task<int> SaveEducationalBoard(EducationalBoard educationalBoard);
        Task<IEnumerable<Course>> GetAllCoursebyCourseType(int coursetype);
        Task<IEnumerable<Colleges_Course>> GetAllCourseWiseColleges (int CourseID);
         
        Task<IEnumerable<Subjects>> GetAllCollegeCOSubjects(string cCode, int courseId);
        Task<IEnumerable<Subjects>> GetAllCollegeSkillSubjects(string cCode, int courseId);
        Task<IEnumerable<Subjects>> GetAllCollegeMajorSubjects(string cCode, int courseId); 
    }
}