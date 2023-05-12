using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmissionData.Entities
{
    public class USR_INF
    {
        [Key]
        public int USR_ID { get; set; }
        public string USR_LOGIN_ID { get; set; }
        public string USR_NAME { get; set; }
        public string USR_PHN_MOB { get; set; }
        public string USR_MAIL { get; set; }
        public string MAIL_DOM_VALUE { get; set; }
        public int USR_DES_ID { get; set; }
        public int USR_SEN_ID { get; set; }
        public int MARK_OFF_ID { get; set; }
        public bool USR_STATUS { get; set; }
        

    }
}
