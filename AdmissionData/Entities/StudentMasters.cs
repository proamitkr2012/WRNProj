using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AdmissionData.Entities
{
    public class StudentMasters
    {
        [Key]
        public long EntryID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Aadhar { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? AltMobile { get; set; }

        public string? Category { get; set; }
        public string? CollegeCode { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? CurrentAddress { get; set; }
        public string? CState { get; set; }
        public string? CDistrict { get; set; }
        public string? CPin { get; set; }
        public string? ParmanentAddress { get; set; }
        public string? PState { get; set; }
        public string? PDistrict { get; set; }
        public string? PPin { get; set; }
        public bool? Ews { get; set; }
        public string? ApplicationNo { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentReferenceNo { get; set; }
        public string? Session { get; set; }
        public string? CourseName { get; set; }
        public string? WRN { get; set; }

        //raj 15/05/2023
        public string? PWD { get; set; }
        public int IsNewadm { get; set; } = 0;

        [NotMapped]
        public string? SubCategory { get; set; }
        [NotMapped]
        public string? Domicile { get; set; }
        [NotMapped]
        public string? Religion { get; set; }
        [NotMapped]
        public string? Nationalty { get; set; }
        [NotMapped]
        public int CourseTypeID { get; set; } = 0;
        [NotMapped]
        public string? Std_PHOTO { get; set; }
        
        [NotMapped]
        public string? CreatedBy { get; set; }

        [NotMapped]
        public string? Roles { get; set; }


        //19062023
        [NotMapped]
        public int? CourseId { get; set; } 
        [NotMapped]
        public int? AdmitFlg { get; set; } 
        [NotMapped]
        public DateTime? AdmitFlgDate { get; set; }
       

    }
}
