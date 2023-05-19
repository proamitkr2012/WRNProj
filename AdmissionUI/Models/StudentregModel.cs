using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AdmissionUI.Models
{
    public class StudentRegModel:ResponseModel
    {
        [Key]
        public string  ApplicationNo { get; set; }

        public string? EncrptedData { get; set; } 

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Name")]
        [StringLength(550)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Father's name")]
        [Display(Name = "Father's Name")]
        [StringLength(500)]
        public string FatherName { get; set; }


        [Required(ErrorMessage = "Please enter Mother's name")]
        [Display(Name = "Father's Name")]
        [StringLength(500)]
        public string MotherName { get; set; }


        [Required(ErrorMessage = "Please enter EmailID")]
        [Display(Name = "Email-ID")]
        [StringLength(500)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email-ID")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter Mobile No.")]
        [Display(Name = "Mobile No.")]
        [StringLength(11)]
        public string Mobile { get; set; }


        public string? AltMobile { get; set; } 

        [Required(ErrorMessage = "Please enter Aadhar Number.")]
        [Display(Name = "Aadhar Number.")]
        [AAdharValidation(ErrorMessage ="Invalid Aadhar")]
        [StringLength(16)]
        public string Aadhar { get; set; }

        [InitialValue(ErrorMessage ="Please Select Gender")]
         public string Gender { get; set; }


        [Required(ErrorMessage = "Please enter Date of Birth")]
        [CustomDateOfBirth]
        public string DOB{ get; set; }

        [InitialValue(ErrorMessage = "Please Select Category")]
        public string Category { get; set; }

        [InitialValue(ErrorMessage = "Please Select Subcategory")]
        public string SubCategory { get; set; }

        [Required(ErrorMessage = "Please enter Current address")]
        public string CurrentAddress { get; set; }

        [InitialValue(ErrorMessage = "Please Select Current address State")]
        public string CState { get; set; }

        [InitialValue(ErrorMessage = "Please Select Current address District")]
        public string CDistrict { get; set; }

        [Required(ErrorMessage = "Please enter Current address Pin")]
        public string CPin { get; set; }

        public string ParmanentAddress { get; set; }

        [InitialValue(ErrorMessage = "Please Select Permanent address State")]
        public string PState { get; set; }

        [InitialValue(ErrorMessage = "Please Select Permanent address District")]
        public string PDistrict { get; set; }

        [Required(ErrorMessage = "Please enter Permanent address Pin")]
        public string PPin { get; set; }

        public bool Ews { get; set; }

        [Required(ErrorMessage = "Please enter Domicile")]
        public string Domicile { get; set; }

        [InitialValue(ErrorMessage = "Please select Religion")]
        public string Religion { get; set; }

        [Required(ErrorMessage = "Please enter Nationalty")]
        public string Nationalty { get; set; }

        public int Newdata { get; set; } = 0;

       
    }
}
