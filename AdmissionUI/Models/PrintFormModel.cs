using AdmissionData.Entities;
using AdmissionModel;
using AdmissionModel.Entity;

namespace AdmissionUI.Models
{
    public class PrintFormModel: StudentMasters
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public List<StudentQualification> QualifationList { get; set; }
        public List<StudentWeightage> stdweightageList { get; set; }
        public List<StudentAppliedCourse> studentapplyList { get; set; }
        public List<Subjects> subjectList { get; set; }

    }
}
