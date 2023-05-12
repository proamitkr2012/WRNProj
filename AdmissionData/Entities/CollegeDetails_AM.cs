using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class CollegeDetails_AM
    {
        [Key]
        public int EntryID { get; set; }
        public int CollegeID { get; set; }
        public int CourseID { get; set; }        
        public int AffiliationType { get; set; }        
        public int Seat { get; set; }        
        public int Subject { get; set; }        
        public int TeacherCount { get; set; }
        public int CollegeTypeID { get; set; }

    }
}
