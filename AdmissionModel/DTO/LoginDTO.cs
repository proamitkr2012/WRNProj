using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace AdmissionModel.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please Enter Enrollment")]
        [Display(Name= "Enrollment")]
        public string? Enrollment { get; set; }
        public string? Dob { get; set; }
        public DateTime? DobDATE { get; set; }


    }

    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Please Enter Enrollment Number / First Year Registration Number")]
        [Display(Name = "Enrollment")]
        public string? Enrollment { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string? Name { get; set; }
        public string? OTP { get; set; }


    }
   
}