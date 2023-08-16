using AdmissionModel.Entity;

namespace AdmissionUI.Models
{
    public class EligibiltyModel:ResponseModel
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public int Newdata { get; set; } = 1;

      
        public List<StudentQualificationModel> QualifationList { get; set; }
    }
}
