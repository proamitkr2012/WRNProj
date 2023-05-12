using AdmissionData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AdmissionModel.DTO
{
    public class StudentMastersDTO
    {
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
        public string? DOBStr { get; set; }
        
        public string? AltMobile { get; set; }
       
        public string? Category { get; set; }
        public string? CollegeCode { get; set; }
        public string? CollegeName { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
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
        public string EncrptedRoll { get; set; }
        public List<DocSetting> DocRequired { get; set; }
        public List<QualifationMasters> QualifationList { get; set; }
       public List<StudentDocUploaded> GetDocUoloadList { get; set; }


        public int DegreeID { get; set; }
        public byte[] qcore { get; set; }

        public string? ApplicationNo { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentReferenceNo { get; set; }
        public string? Session { get; set; }
        public string? CourseName { get; set; }
        public string? WRN { get; set; }
    }
}
