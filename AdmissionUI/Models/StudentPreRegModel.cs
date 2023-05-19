using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AdmissionUI.Models
{
    public class StudentPreRegModel:ResponseModel
    {

        public int PreRegID { get; set; } = 0;

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Name")]
        [StringLength(550)]
        public string StudentName { get; set; }
        
        [Required(ErrorMessage = "Please enter Father's name")]
        [Display(Name = "Father's Name")]
        [StringLength(500)]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please enter Mobile No.")]
        [Display(Name = "Mobile No.")]
        [StringLength(11)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter EmailID")]
        [Display(Name = "Email-ID")]
        [StringLength(500)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email-ID")]
        public string EmailID { get; set; }
        public int? IsOTPVerified { get; set; } = 0;

        [InitialValue(ErrorMessage ="Please select Course Type")]
        public int CourseTypeID { get; set; }


    }
}
