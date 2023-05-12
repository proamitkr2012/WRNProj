using AdmissionData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdmissionModel.DTO
{
    public class StudentDTO
    {
       
        public Int64 StudId { get; set; }
        public string? RollNo { get; set; }
        public string EnrollNo { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime? DOB { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string AddressLocation { get; set; }        
        public int? GenderId { get; set; }
        public int CourseID { get; set; }
        public string CandidateName { get; set; }

        
        public string[] Roles { get; set; }

        public string DOBDate { get; set; }
       
        //
        public string ProfilePic { get; set; }
        public string SignaturePic { get; set; }
        public string FeesName { get; set; }

        ///

        public long TRAN_ID { get; set; }
        public string? Remarks { get; set; }
        public bool ISVERIFIED { get; set; }
        public bool IsCompleted { get; set; }
    }
}