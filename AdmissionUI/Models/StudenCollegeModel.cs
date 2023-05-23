using AdmissionModel;

namespace AdmissionUI.Models
{
    public class StudenCollegeModel:ResponseModel
    {
        
        public int CourseID { get; set; }
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public int IsNewData { get; set; } = 1;
        public List<StudentAppliedColleges> studentapplycollegeList { get; set; }
    }
}
