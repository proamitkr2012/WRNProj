using System.ComponentModel.DataAnnotations;

namespace AdmissionUI.Models
{
    public class LoginModel:ResponseModel
    {
        [Required(ErrorMessage = "Please enter Application No")]
        [Display(Name = "Application No")]
        public string UserName  { get; set; }

        [Required(ErrorMessage = "Please enter Pasword")]
        public string PWd { get; set; }
    }
}
