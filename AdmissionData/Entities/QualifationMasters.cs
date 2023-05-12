using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdmissionData.Entities
{
    public class QualifationMasters
    {
        [Key]
        public long EntryID { get; set; }
        public string Roll { get; set; }
        public int DocID { get; set; }
        public string? ExamStatus { get; set; }
        public string? Board_Universty_Name { get; set; }
        public string? Subject { get; set; }
        public string? PassingYear { get; set; }
        public string? PreRollNo { get; set; }
        public decimal MarkObt { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal Percentage { get; set; }
        public bool IsCGPA { get; set; }

        [NotMapped]
        public string DocName { get; set; }
        [NotMapped]
        public string DocDisplayName { get; set; }

    }
}
