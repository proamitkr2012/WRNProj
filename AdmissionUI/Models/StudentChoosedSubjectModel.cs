using AdmissionModel;

namespace AdmissionUI.Models
{
    public class StudentChoosedSubjectModel:ResponseModel
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        
        public int IsNewData { get; set; } = 1;
        public List<StudentSubjects> studentSubjects { get; set; } 
    }
}
