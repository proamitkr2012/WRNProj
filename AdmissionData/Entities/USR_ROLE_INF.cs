using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class USR_ROLE_INF
    {
        [Key]
        public int USR_ID { get; set; }
        public int ROLE_ID { get; set; }        
        public bool USR_ROLE_STATUS { get; set; }

    }
}
