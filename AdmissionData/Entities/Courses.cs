using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? CourseNameUnicode { get; set; }
        public byte? SubjectCount { get; set; }
        public string? RollCode { get; set; }
        public string? QualifyingExam { get; set; }
        public byte? CourseYear { get; set; }
        public byte? CourseSemester { get; set; }
        public byte? CourseSerial { get; set; }
        public int? PrevCourseID { get; set; }
        public int? NextCourseID { get; set; }
        public bool? HideRGES { get; set; }
        public int? CourseGroupID { get; set; }
        public string? CLCode { get; set; }
        public string? AdmissionDegree { get; set; }
        public bool? IsEnabled { get; set; }
        public int? AdmissionDegreeID { get; set; }
        public int? OldCourseID { get; set; }
        public int? BackPaper { get; set; }
        public byte? BackType { get; set; }
        public string? CourseCode { get; set; }
    }


}
