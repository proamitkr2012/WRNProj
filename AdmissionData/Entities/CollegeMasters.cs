using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class CollegeMasters
    {
        [Key]
        public int CollegeID { get; set; }
        public string? Code { get; set; }
        public string? Cname { get; set; }
        public string? CfullName { get; set; }
        public string? CType { get; set; }
        public string? CCategory { get; set; }
       
        public string? InstCode { get; set; }
        public bool? IsActive { get; set; }
    }


}
