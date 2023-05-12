using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class DocSetting
    {
        [Key]
        public int EntryID { get; set; }
        public int DegreeID { get; set; }        
        public string DegreeCode { get; set; }
        public string Degree { get; set; }
        public string DocName { get; set; }
        public string DocDisplayName { get; set; }
        public bool IsOnlyUpload { get; set; }
        public bool IsActive { get; set; }

    }
}
