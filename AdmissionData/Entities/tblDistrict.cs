using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class tblDistrict
    {
        [Key]
        public int Id { get; set; }
        public int StateId { get; set; }        
        public string Name { get; set; }

    }
}
