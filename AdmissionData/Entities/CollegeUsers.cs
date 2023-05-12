using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionData.Entities
{
    public class CollegeUsers
    {
        //public Student()
        //{
        //    Roles = new HashSet<Role>();
        //}

        [Key]
        public Int64 UserID { get; set; }
        public string Password { get; set; }
        public string Clogin { get; set; }
        public int CollegeID { get; set; }
        
        public string UsersName { get; set; }

        public string Email { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DOB { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public string? AddressLocation { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GenderId { get; set; }
        public Nullable<DateTime> LastActivityDate { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
        public Nullable<DateTime> LastPasswordChangedDate { get; set; }
        public bool IsLockedOut { get; set; }


    }
}