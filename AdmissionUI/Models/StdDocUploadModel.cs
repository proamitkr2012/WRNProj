using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace AdmissionUI.Models
{
    public class StdDocUploadModel
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public string Std_Photo { get; set; }

        [NotMapped]
        [DisplayName("Upload Photo")]
        public IFormFile ImageFile { get; set; }
       
    }
    }
