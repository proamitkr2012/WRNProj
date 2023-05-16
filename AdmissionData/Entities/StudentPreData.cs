using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class StudentPreData
    {
        [Key]
        public long EntryID { get; set; }
        public string Roll { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Category { get; set; }

        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
       
        public string? CollegeCode { get; set; }
       
        public bool IsActive { get; set; }
        public int EntryBy { get; set; }
        public DateTime? Entrydate { get; set; }
        public string? SystemIP { get; set; }
    }
}
