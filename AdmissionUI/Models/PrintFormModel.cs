using AdmissionData.Entities;
using AdmissionModel;
using AdmissionModel.Entity;

namespace AdmissionUI.Models
{
    public class PrintFormModel: StudentMasters
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public DateTime? ReportFlgDate { get; set; }
        public DateTime? AdmitFlgDate { get; set; }
        public DateTime? FeeDepositDate { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string? CollegeName { get; set; }
        public string? CRN { get; set; }
        public  int? seatAvilable { get; set; }

        public int? Status { get; set; }
        public string ? Msg { get; set; }
        public List<StudentQualification> QualifationList { get; set; }
        public List<StudentWeightage> stdweightageList { get; set; }
        public List<StudentAppliedCourse> studentapplyList { get; set; }
        public List<Subjects> subjectList { get; set; }

    }
}
