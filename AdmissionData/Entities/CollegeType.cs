using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class CollegeType
    {
        [Key]
        public int CollegeTypeID { get; set; }
        public string Type { get; set; }
       
       
    }


}
