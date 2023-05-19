namespace AdmissionUI.Models
{
    public class StudentCollegesModel:ResponseModel
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        
        [InitialValue(ErrorMessage ="Please select Course")]
        public int CourseID { get; set; }
        List<StudentCourseCollegesModel>list { get; set; }
    }

    public class StudentCourseCollegesModel
    {
        public string CCode { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Seat { get; set; }
        public long Srno { get; set; }
        public string ApplicationNo { get; set; }

    }
}
