namespace AdmissionModel
{
    public class SubjectMaster : Colleges_Course
    {
        public int SubjectID { get; set; }
        public string SubCode { get; set; }
        public string SubjectName { get; set; }
        public string Session { get; set; }
        public int SEM { get; set; }
        public int Status { get; set; }
        public int SubjectType { get; set; }
        public string? SubCategory { get; set; }

        List<SubjectMaster> SubjectList { get; set; }
    }




}
