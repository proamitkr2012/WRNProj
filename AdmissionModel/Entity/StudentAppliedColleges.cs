using AdmissionData.Entities;

namespace AdmissionModel
{
    public  class StudentAppliedColleges:Colleges
    {
      public long SRNO { get;set; }
      public string  CCode { get; set; }
      public int   CourseId { get; set; }
      public string  ApplicationNo { get; set; }
      public string CourseName { get; set; } 
      public bool IsSelect { get; set; }
      public int IsNEP { get; set; }
      public int IsSubSelect { get; set; }
      public string City  { get; set; }
      public string CollegeType { get; set; }
      public int IsRWCollege { get; set; }
        

    }

    public class StudentAppliedCollegesSubject : StudentAppliedColleges
    {
        public long SRNO { get; set; }
        public int MajorSubjectID { get; set; }
        public int CoSubjectID { get; set; }
        public int SkillSubjectID { get; set; }
        

    }

    public class StudentSubjects : StudentAppliedCollegesSubject
    {

        public string Subject { get; set; }
        public string CoSubject { get; set; }
        public string SkillSubject { get; set; }

    }


}
