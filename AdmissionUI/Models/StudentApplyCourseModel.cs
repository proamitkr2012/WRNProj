using AdmissionModel;

namespace AdmissionUI.Models
{
    public class StudentApplyCourseModel:ResponseModel
    {
        [InitialValue(ErrorMessage = "Please select Course")]
        public int CourseID { get; set; }
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public List<StudentAppliedCourse>studentapplyList { get; set; }
    }
}
