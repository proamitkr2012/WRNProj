using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdmissionData.Entities
{
    public class Colleges
    {
        [Key]
        public int CollegeID { get; set; }

        //public string? CollegeNameUnicode { get; set; }

        //public string? CollegeDistrict { get; set; }
        //public int? PrevCollegeID { get; set; }
        //public string? Remarks { get; set; }
        //public bool? BlockVerify { get; set; }


        //  //[NotMapped]
        ////  public string? CollegeType { get; set; }

        public string? CLogin { get; set; }

        //[NotMapped]
        public string? CollegeName { get; set; }
        // [NotMapped]
        public string? CollegeCode { get; set; }
        //[NotMapped]
        public byte? CollegeType { get; set; }


        [NotMapped]
        public string? College { get; set; }

        [NotMapped]
        public string? Code { get; set; }

    }


}
