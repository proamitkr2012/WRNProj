namespace AdmissionUI.Models
{
    public class StudentQualificationModel
    {
        public string ApplicationNo { get; set; }
        public int EntryID { get; set; }
        public int DegreeID { get; set; }
        public int DocID { get; set; }
        public string DocName { get; set; }
        public string DocDisplayName { get; set; }
        public string ExamStatus { get; set; }
        public string Board_Universty_Name { get; set; }
        public string Subject { get; set; }
        public string PassingYear { get; set; }
        public string PreRollNo { get; set; }
        public double MarkObt { get; set; }
        public double TotalMarks { get; set; }
        public double Percentage { get; set; }
        public bool IsCGPA { get; set; }
        public string Board_Other { get; set; }
    }
}
