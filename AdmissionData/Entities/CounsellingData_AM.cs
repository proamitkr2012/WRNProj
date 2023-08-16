using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class CounsellingData_AM
    {
        [Key]
        public int EntryID { get; set; }
        public string? CounsellingNo { get; set; }
        public decimal Amount { get; set; }
        public string? Mobile { get; set; }
        public int CourseId { get; set; }

    }


}
